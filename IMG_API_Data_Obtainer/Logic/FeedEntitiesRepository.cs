using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Logic.Abstractions;
using IMG_API_Data_Obtainer.Models;
using IMG_API_Data_Obtainer.Models.Feed;
using IMG_API_Data_Obtainer.Models.Internal;

using MatchType = IMG_API_Data_Obtainer.TransportModels.RawMatchType;
using TeamType = IMG_API_Data_Obtainer.TransportModels.TeamType;
using EntryType = IMG_API_Data_Obtainer.TransportModels.MatchEntryType;

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
    public FeedEntitiesRepository(IEntitiesStructureFetcher entitiesStructureDeserializer)
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

            _ when typeof(FeedTeamDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>) await GetTeamsAsync(cancellationToken),

            _ when typeof(FeedEventDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>) await GetMatchesAsync(cancellationToken),

            _ => throw new InvalidOperationException($"An unknown feed entity type is received {typeof(TFeed).Name}."),
        };
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntSportDescription>, FeedSportDescription>> GetSportsAsync(CancellationToken cancellationToken)
    {
        var sports = 
            await _entitiesStructureDeserializer.FetchSportsAsync(cancellationToken)
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

        var tennisCategories = 
            await _entitiesStructureDeserializer.FetchCountriesAsync(cancellationToken)
                .ConfigureAwait(false);

        return tennisCategories.ToDictionary(
            category => new ExternalID<IntCategoryDescription>(
                $"{category.Id}"),
            category
                => new FeedCategoryDescription(
                    new($"{category.Id.Value}"),
                    new($"{SPORT_TENNIS_ID}")));
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntChampionshipDescription>, FeedChampionshipDescription>> GetChampionshipsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var championships = 
            await _entitiesStructureDeserializer.FetchTournamentsAsync(cancellationToken)
                .ConfigureAwait(false);

        return championships.ToDictionary(
            championship => new ExternalID<IntChampionshipDescription>(
                $"{championship.Id}"),
            championship =>
                new FeedChampionshipDescription(
                    new($"{championship.Id.Value}"),
                    new($"{championship.SportId.Value}"),
                    new($"{championship.CountryId.Value}"),
                    new($"{championship.TournamentName.Value}"),
                    championship.Year,
                    championship.MatchType));
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntTeamDescription>, FeedTeamDescription>> GetTeamsAsync(CancellationToken cancellationToken)
    {
        var teams = await _entitiesStructureDeserializer.FetchTeamsAsync(cancellationToken)
            .ConfigureAwait(false);

        return teams.ToDictionary(
                team => new ExternalID<IntTeamDescription>(
                            $"{team.Id.Value}"),
                team => new FeedTeamDescription(
                            new($"{team.Id.Value}"),
                            new($"{team.SportId.Value}"),
                            new($"{team.FullName.Value}"),
                            team.TeamType));
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntEventDescription>, FeedEventDescription>> GetMatchesAsync(CancellationToken cancellationToken)
    {
        var matches = await _entitiesStructureDeserializer.FetchMatchesAsync(cancellationToken)
            .ConfigureAwait(false);

        var deletedMatches = await _entitiesStructureDeserializer.FetchDeletedMatchesAsync(cancellationToken)
            .ConfigureAwait(false);

        var tournaments = await _entitiesStructureDeserializer.FetchTournamentsAsync(cancellationToken)
            .ConfigureAwait(false);

        return matches.Aggregate(
            new Dictionary<ExternalID<IntEventDescription>, FeedEventDescription>(),
            (matches, match) =>
            {
                var externalId = new ExternalID<IntEventDescription>(
                    $"{match.Id.Value}");

                var mustBeMarkedAsCancelled =
                    deletedMatches.Contains(match.Id)
                    || match.IsCancelled;

                var tournament = tournaments.FirstOrDefault(x => x.CompetitionIds.Contains(match.CompetitionId.Value));

                if (tournament != null)
                {
                    var feedEvent = new FeedEventDescription(
                        externalId,
                        match.MatchType == MatchType.MS || match.MatchType == MatchType.LS
                            || match.MatchType == MatchType.QMS || match.MatchType == MatchType.QLS
                                ? TeamType.Solo
                                : match.MatchType == MatchType.MD || match.MatchType == MatchType.LD
                                    || match.MatchType == MatchType.QMD || match.MatchType == MatchType.QLD
                                        ? TeamType.DuoSimilar
                                        : TeamType.DuoMixed,
                        match.MatchType == MatchType.MS || match.MatchType == MatchType.LS
                            || match.MatchType == MatchType.MD || match.MatchType == MatchType.LD
                                || match.MatchType == MatchType.XD
                                    ? EntryType.Standart
                                    : EntryType.Qualifier,
                        new ExternalID<IntChampionshipDescription>(
                            $"{tournament.Id}"),
                        match.ScheduledStart,
                        new(
                            new ExternalID<IntTeamDescription>(
                                $"{match.TeamA.Value}"),
                            new ExternalID<IntTeamDescription>("" +
                            $"{match.TeamB.Value}")),
                        mustBeMarkedAsCancelled);

                    matches.Add(feedEvent.ExternalKey, feedEvent);
                }

             return matches;
        });
    }

    private readonly IEntitiesStructureFetcher _entitiesStructureDeserializer;

    private const long SPORT_TENNIS_ID = 5L;
}
