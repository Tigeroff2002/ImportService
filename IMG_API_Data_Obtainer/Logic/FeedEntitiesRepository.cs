using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Logic.Abstractions;
using IMG_API_Data_Obtainer.Models;
using IMG_API_Data_Obtainer.Models.Feed;
using IMG_API_Data_Obtainer.Models.Internal;
using IMG_API_Data_Obtainer.TransportModels;
using System.IO;
using MatchType = IMG_API_Data_Obtainer.TransportModels.MatchType;

namespace IMG_API_Data_Obtainer.Logic;

/// <summary>
/// Представляет репозиторий внешних сущностей.
/// </summary>
public sealed class FeedEntitiesRepository : IFeedEntitiesRepository
{
    /// <summary>
    /// Создаёт экземпляр типа <see cref="FeedEntitiesRepository"/>.
    /// </summary>
    /// <param name="entitiesStructureDeserializer">
    /// Извлекатель данных по структуре матчей из внешних систем.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="entitiesStructureDeserializer"/> является <see langword="null"/>.
    /// </exception>
    public FeedEntitiesRepository(IEntitiesStructureDeserializer entitiesStructureDeserializer)
    {
        _entitiesStructureDeserializer = entitiesStructureDeserializer ??
            throw new ArgumentNullException(nameof(entitiesStructureDeserializer));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyDictionary<ExternalID<TInternal>, TFeed>> GetAllAsync<TFeed, TInternal>(CancellationToken cancellationToken)
        where TFeed : MappableEntity<TInternal>
        where TInternal : IInternalEntityMarker
    {
        cancellationToken.ThrowIfCancellationRequested();

        return typeof(TFeed) switch
        {
            _ when typeof(FeedSportDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>) await GetSportsAsync(cancellationToken),

            _ when typeof(FeedCategoryDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>) await GetCategoriesAsync(cancellationToken),

            _ when typeof(FeedChampionshipDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>) await GetChampionshipsAsync(cancellationToken),

            _ when typeof(FeedPlayerDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>) await GetPlayersAsync(cancellationToken),

            _ when typeof(FeedEventDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>) await GetMatchesAsync(cancellationToken),

            _ => throw new InvalidOperationException($"An unknown feed entity type is received {typeof(TFeed).Name}."),
        };
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntSportDescription>, FeedSportDescription>> GetSportsAsync(CancellationToken cancellationToken)
    {
        var sports = await _entitiesStructureDeserializer.FetchSportsAsync(cancellationToken)
            .ConfigureAwait(false);

        return sports.ToDictionary(
            sport => new ExternalID<IntSportDescription>(
                        $"{sport.Key.Value}"),
            sport => new FeedSportDescription(
                        new($"{sport.Key.Value}"),
                        new($"{sport.Value.Name}")));
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntCategoryDescription>, FeedCategoryDescription>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tennisCategories = await LoadTennisCategoriesAsync();

        return tennisCategories.ToDictionary(v => v.ExternalKey);

        async Task<IEnumerable<FeedCategoryDescription>> LoadTennisCategoriesAsync()
        {
            var tennisCompetitions =
                await _entitiesStructureDeserializer.FetchCompetitionsAsync(
                    _tennisId,
                    cancellationToken);

            return tennisCompetitions.Values.Select(
                    competition
                        => new FeedCategoryDescription(
                            new($"{competition.Id.Value}"),
                            new($"{competition.Name.Value}")));
        }
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntChampionshipDescription>, FeedChampionshipDescription>> GetChampionshipsAsync(CancellationToken cancellationToken)
    {
        var championships = await Task.WhenAll(
            _supportedSportIds.Select(
                spid => _entitiesStructureDeserializer.FetchChampionshipsAsync(
                    spid,
                    cancellationToken)));

        return championships.SelectMany(v => v.Values).Select(
            championship =>
                new FeedChampionshipDescription(
                    new($"{championship.Id.Value}"),
                    new($"{championship.SportId.Value}"),
                    championship.SportId == _tennisId
                        ? new ExternalID<IntCategoryDescription>($"{championship.CompetitionId.Value}")
                        : new($"{championship.CountryId.Value}"),
                    new($"{championship.FullName.Value}"),
                    championship.Year))
            .ToDictionary(v => v.ExternalKey);
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntPlayerDescription>, FeedPlayerDescription>> GetPlayersAsync(CancellationToken cancellationToken)
    {
        var players = await Task.WhenAll(
            _supportedSportIds.Select(
                spid => _entitiesStructureDeserializer.FetchCompetitorsAsync(
                    spid,
                    cancellationToken)));

        return players.SelectMany(v => v.Values)
            .ToDictionary(
                player => new ExternalID<IntPlayerDescription>(
                            $"{player.Id.Value}"),
                player => new FeedPlayerDescription(
                            new($"{player.Id.Value}"),
                            new($"{player.SportId.Value}"),
                            new($"{player.CountryId.Value}"),
                            new($"{player.Name.Value}"),
                            player.Gender));
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntEventDescription>, FeedEventDescription>> GetMatchesAsync(CancellationToken cancellationToken)
    {
        var matches = await Task.WhenAll(
            _supportedSportIds.Select(
                spid => _entitiesStructureDeserializer.FetchMatchesAsync(
                    spid,
                    cancellationToken)));

        var deletedMatches = await _entitiesStructureDeserializer.FetchDeletedMatchesAsync(cancellationToken)
            .ConfigureAwait(false);

        return matches.SelectMany(v => v.Values)
            .Aggregate(
            new Dictionary<ExternalID<IntEventDescription>, FeedEventDescription>(),
            (matches, match) =>
            {
                var externalId = new ExternalID<IntEventDescription>(
                    $"{match.Id.Value}");

                var mustBeMarkedAsCancelled =
                    deletedMatches.Contains(match.Id)
                    || match.IsCancelled;

                var feedEvent = new FeedEventDescription(
                    externalId,
                    match.MatchType,
                    new($"{match.ChampionshipId.Value}"),
                    match.ScheduledStart,
                    GetTeamsCollectionDescriptionForCurrentMatch(match),
                    mustBeMarkedAsCancelled);

                matches.Add(feedEvent.ExternalKey, feedEvent);

                return matches;
            });

        FeedTwoTeamsCollectionDescription GetTeamsCollectionDescriptionForCurrentMatch(Match match)
        {
            if (match == null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return match.MatchType switch
            {
                MatchType.MaleSolo
                    or MatchType.FemaleSolo
                    or MatchType.QualifierMaleSolo
                    or MatchType.QualifierFemaleSolo =>
                        new FeedTwoSoloTeamsCollectionDescription(
                            match.TeamAEntry,
                            match.TeamBEntry,
                            match.TeamAKnownStatus,
                            match.TeamBKnownStatus,
                            match.TeamAPlayersIds.Count == 1
                                ? new($"{match.TeamAPlayersIds.First()}")
                                : throw new ArgumentException(
                                    $"Collection {nameof(match.TeamAPlayersIds)}" +
                                    $" should contains 1 player for solo tennis match"),
                            match.TeamBPlayersIds.Count == 1
                                ? new($"{match.TeamBPlayersIds.First()}")
                                : throw new ArgumentException(
                                    $"Collection {nameof(match.TeamBPlayersIds)}" +
                                    $" should contains 1 player for solol tennis match")),

                MatchType.MaleDuo
                    or MatchType.FemaleDuo
                    or MatchType.QualifierMaleDuo
                    or MatchType.QualifierFemaleDuo
                    or MatchType.MixedDuo
                    or MatchType.QualifierMixedDuo =>
                        match.TeamAPlayersIds.Count == 2 && match.TeamBPlayersIds.Count == 2
                            ? new FeedTwoDuoTeamsCollectionDescription(
                                match.TeamAEntry,
                                match.TeamBEntry,
                                match.TeamAKnownStatus,
                                match.TeamBKnownStatus,
                                match.MatchType == MatchType.MixedDuo
                                    || match.MatchType == MatchType.QualifierMixedDuo
                                        ? GenderMixType.Mixed
                                        : GenderMixType.Similar,
                                new List<ExternalID<IntPlayerDescription>>
                                {
                                    new($"{match.TeamAPlayersIds.First()}"),
                                    new($"{match.TeamAPlayersIds.Last()}"),
                                },
                                new List<ExternalID<IntPlayerDescription>>
                                {
                                    new($"{match.TeamAPlayersIds.First()}"),
                                    new($"{match.TeamAPlayersIds.Last()}"),
                                })
                            : throw new ArgumentException(
                                $"Each of 2 collections: " +
                                $"{nameof(match.TeamAPlayersIds)}" +
                                $" and {nameof(match.TeamBPlayersIds)}" +
                                $" should contain exactly 2 players for duo tennis match"),

                _ => throw new NotSupportedException($"{match.MatchType} is not supported now")
            };
        }
    }

    private readonly IEntitiesStructureDeserializer _entitiesStructureDeserializer;

    private static readonly Id<Sport> _tennisId = new(5);

    private readonly IReadOnlySet<Id<Sport>> _supportedSportIds = new HashSet<Id<Sport>>
    {
        _tennisId,
    };
}
