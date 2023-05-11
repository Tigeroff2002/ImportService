using ADF.Library.Models.Mapping;
using ADF.Library.Models.Mapping.Feed.Description;
using ADF.Library.Models.Mapping.Internal.Description;
using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Logic.Abstractions;

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
    {
        cancellationToken.ThrowIfCancellationRequested();

        return typeof(TFeed) switch
        {
            _ when typeof(FeedSportDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>)await GetSportsAsync(cancellationToken),

            _ when typeof(FeedCategoryDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>)await GetCategoriesAsync(cancellationToken),

            _ when typeof(FeedChampionshipDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>)await GetChampionshipsAsync(cancellationToken),

            _ when typeof(FeedTeamDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>)await GetTeamsAsync(cancellationToken),

            _ when typeof(FeedEventDescription) == typeof(TFeed)
                => (IReadOnlyDictionary<ExternalID<TInternal>, TFeed>)await GetMatchesAsync(cancellationToken),

            _ => throw new InvalidOperationException($"An unknown feed entity type is received {typeof(TFeed).Name}."),
        };
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntSportDescription>, FeedSportDescription>> GetSportsAsync(CancellationToken cancellationToken)
    {
        var sports = await _entitiesStructureDeserializer.FetchSportsAsync(cancellationToken)
            .ConfigureAwait(false);

        return sports.ToDictionary(
            sport => new ExternalID<IntSportDescription>(
                        $"{sport.Key.Value}",
                        ExternalSystem.TxOdds),
            sport => new FeedSportDescription(
                        new($"{sport.Key.Value}",
                            ExternalSystem.TxOdds),
                        sport.Value.Name.Value)
            {
                IsChangedByImport = true
            });
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
                            new($"{competition.Id.Value}", ExternalSystem.TxOdds),
                            competition.Name.Value)
                        {
                            IsChangedByImport = true
                        });
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
                    new($"{championship.Id.Value}", ExternalSystem.TxOdds),
                    new($"{championship.SportId.Value}", ExternalSystem.TxOdds),
                    championship.SportId == _tennisId
                        ? new ExternalID<IntCategoryDescription>($"{championship.CompetitionId.Value}", ExternalSystem.TxOdds)
                        : new($"{championship.CountryId.Value}", ExternalSystem.TxOdds),
                    championship.FullName.Value,
                    false)
                {
                    IsChangedByImport = true,
                })
            .ToDictionary(v => v.ExternalKey);
    }

    private async Task<IReadOnlyDictionary<ExternalID<IntTeamDescription>, FeedTeamDescription>> GetTeamsAsync(CancellationToken cancellationToken)
    {
        var teams = await Task.WhenAll(
            _supportedSportIds.Select(
                spid => _entitiesStructureDeserializer.FetchCompetitorsAsync(
                    spid,
                    cancellationToken)));

        return teams.SelectMany(v => v.Values)
            .ToDictionary(
                team => new ExternalID<IntTeamDescription>(
                            $"{team.Id.Value}",
                            ExternalSystem.TxOdds),
                team => new FeedTeamDescription(
                            new($"{team.Id.Value}",
                                ExternalSystem.TxOdds),
                            new($"{team.SportId.Value}",
                                ExternalSystem.TxOdds),
                            $"{team.Name.Value} ({"some_team_name"})")
                {
                    IsChangedByImport = true
                });
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
                    $"{match.Id.Value}",
                    ExternalSystem.TxOdds);

                var mustBeMarkedAsCancelled =
                    deletedMatches.Contains(match.Id)
                    || match.IsCancelled;

                var feedEvent = new FeedEventDescription(
                    externalId,
                    new($"{match.ChampionshipId.Value}",
                        ExternalSystem.TxOdds),
                    match.ScheduledStart,
                    new FeedTwoTeamsCollectionDescription(
                        new($"{match.HomeId.Value}",
                            ExternalSystem.TxOdds),
                        new($"{match.AwayId.Value}",
                            ExternalSystem.TxOdds)),
                    mustBeMarkedAsCancelled)
                {
                    IsChangedByImport = true
                };

                matches.Add(feedEvent.ExternalKey, feedEvent);

                return matches;
            });
    }

    private readonly IEntitiesStructureDeserializer _entitiesStructureDeserializer;

    private static readonly Id<Sport> _tennisId = new(5);

    private readonly IReadOnlySet<Id<Sport>> _supportedSportIds = new HashSet<Id<Sport>>
    {
        _tennisId,
    };
}
