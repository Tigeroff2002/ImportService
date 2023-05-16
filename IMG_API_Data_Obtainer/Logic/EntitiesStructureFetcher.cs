using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Logic.Abstractions;

using ADF.Library.Common.Logic.Serialization;
using IMG_API_Data_Obtainer.TransportModels;

using Tournament = IMG_API_Data_Obtainer.EntitiesModels.Tournament;
using Competition = IMG_API_Data_Obtainer.EntitiesModels.Competition;
using Team = IMG_API_Data_Obtainer.EntitiesModels.Team;

namespace IMG_API_Data_Obtainer.Logic;

public sealed class EntitiesStructureFetcher : IEntitiesStructureFetcher
{
    public EntitiesStructureFetcher(
        IDeserializer<string, IReadOnlyDictionary<Id<Sport>, Sport>> sportsDeserializer,
        IDeserializer<string, IReadOnlyList<Country>> countriesDeserializer,
        IDeserializer<string, IReadOnlyList<Tournament>> tournamentsDeserializer,
        IDeserializer<string, IReadOnlyList<Competition>> competitionsDeserializer,
        IDeserializer<string, IReadOnlyList<Team>> teamsDeserializer,
        IDeserializer<string, IReadOnlyList<Match>> matchesDeserializer,
        IDeserializer<string, IReadOnlyList<Name<Match>>> deletedMatchesDeserializer)
    {
        _sportsDeserializer = sportsDeserializer ?? throw new ArgumentNullException(nameof(sportsDeserializer));
        _countriesDeserializer = countriesDeserializer ?? throw new ArgumentNullException(nameof(countriesDeserializer));
        _tournamentsDeserializer = tournamentsDeserializer ?? throw new ArgumentNullException(nameof(tournamentsDeserializer));
        _competitionsDeserializer = competitionsDeserializer ?? throw new ArgumentNullException(nameof(competitionsDeserializer));
        _teamsDeserializer = teamsDeserializer ?? throw new ArgumentNullException(nameof(teamsDeserializer));
        _matchesDeserializer = matchesDeserializer ?? throw new ArgumentNullException(nameof(matchesDeserializer));
        _deletedMatchesDeserializer = deletedMatchesDeserializer ?? throw new ArgumentNullException(nameof(deletedMatchesDeserializer));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyDictionary<Id<Sport>, Sport>> FetchSportsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var sports = _sportsDeserializer.Deserialize(
            await FileReader.ReadMatchesData(nameof(Sport), cancellationToken)
                .ConfigureAwait(false))
            .DistinctBy(sport => sport.Key);

        return sports.ToDictionary(x => x.Key, x => x.Value);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Country>> FetchCountriesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var countries = _countriesDeserializer.Deserialize(
            await FileReader.ReadMatchesData(nameof(Country), cancellationToken)
                .ConfigureAwait(false))
            .Where(country => country.Id.Value != SKIPPED_CATEGORY_NAME)
            .DistinctBy(country => country.Id);

        return countries.ToList();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Tournament>> FetchTournamentsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tournaments = _tournamentsDeserializer.Deserialize(
            await FileReader.ReadMatchesData(nameof(Tournament), cancellationToken)
                .ConfigureAwait(false));

        var championshipsList = new List<Tournament>(); 

        var matchesList = await FetchMatchesAsync(cancellationToken);

        foreach (var tournament in tournaments)
        {
            var competitionId = tournament.CompetitionIds.FirstOrDefault();

            var rawMatchType = matchesList.FirstOrDefault(match => match.CompetitionId.Value == competitionId)!.MatchType;

            var rawMatchTypeString = rawMatchType.ToString();

            var matchType = rawMatchTypeString[0] == 'Q'
                ? rawMatchTypeString[1..]
                : rawMatchTypeString;

            tournament.TournamentName = new($"{tournament.TournamentName} {matchType}");

            tournament.MatchType = rawMatchType;

            championshipsList.Add(tournament);
        }

        return championshipsList;
    }

    public async Task<IReadOnlyList<Competition>> FetchCompetitionsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _competitionsDeserializer.Deserialize(
            await FileReader.ReadMatchesData(nameof(Competition), cancellationToken)
                .ConfigureAwait(false));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Team>> FetchTeamsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _teamsDeserializer.Deserialize(
            await FileReader.ReadMatchesData(nameof(Team), cancellationToken)
                .ConfigureAwait(false))
            .Where(team => team.Status == KnownTeamStatus.KnownTennisTeam)
            .DistinctBy(team => team.Id)
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Match>> FetchMatchesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _matchesDeserializer.Deserialize(
            await FileReader.ReadMatchesData(nameof(Match), cancellationToken)
                .ConfigureAwait(false))
            .DistinctBy(matches => matches.Id)
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Name<Match>>> FetchDeletedMatchesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _deletedMatchesDeserializer.Deserialize(
            await FileReader.ReadMatchesData(nameof(Match), cancellationToken)
                .ConfigureAwait(false));
    }

    private readonly IDeserializer<string, IReadOnlyDictionary<Id<Sport>, Sport>> _sportsDeserializer;
    private readonly IDeserializer<string, IReadOnlyList<Country>> _countriesDeserializer;
    private readonly IDeserializer<string, IReadOnlyList<Tournament>> _tournamentsDeserializer;
    private readonly IDeserializer<string, IReadOnlyList<Competition>> _competitionsDeserializer;
    private readonly IDeserializer<string, IReadOnlyList<Team>> _teamsDeserializer;
    private readonly IDeserializer<string, IReadOnlyList<Match>> _matchesDeserializer;
    private readonly IDeserializer<string, IReadOnlyList<Name<Match>>> _deletedMatchesDeserializer;

    private const string SKIPPED_CATEGORY_NAME = "ZZZ";
}
