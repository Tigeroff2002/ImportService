using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Logic.Abstractions;

using ADF.Library.Common.Logic.Serialization;
using System.Diagnostics.Metrics;
using System.Threading;

namespace IMG_API_Data_Obtainer.Logic;

public sealed class EntitiesStructureDeserializer : IEntitiesStructureDeserializer
{
    public EntitiesStructureDeserializer(
        IDeserializer<string, IReadOnlyDictionary<Id<Sport>, Sport>> sportsDeserializer,
        IDeserializer<string, IReadOnlyDictionary<Id<Championship>, Championship>> championshipsDeserializer,
        IDeserializer<string, IReadOnlyDictionary<Id<Competitor>, Competitor>> competitorsDeserializer,
        IDeserializer<string, IReadOnlyDictionary<Id<Match>, Match>> matchesDeserializer,
        IDeserializer<string, IReadOnlyCollection<Id<Match>>> deletedMatchesDeserializer,
        IDeserializer<string, IReadOnlyDictionary<Id<Competition>, Competition>> competitionsDeserializer)
    {
        _sportsDeserializer = sportsDeserializer ?? throw new ArgumentNullException(nameof(sportsDeserializer));
        _championshipsDeserializer = championshipsDeserializer ?? throw new ArgumentNullException(nameof(championshipsDeserializer));
        _competitorsDeserializer = competitorsDeserializer ?? throw new ArgumentNullException(nameof(competitorsDeserializer));
        _matchesDeserializer = matchesDeserializer ?? throw new ArgumentNullException(nameof(matchesDeserializer));
        _deletedMatchesDeserializer = deletedMatchesDeserializer ?? throw new ArgumentNullException(nameof(deletedMatchesDeserializer));
        _competitionsDeserializer = competitionsDeserializer ?? throw new ArgumentNullException(nameof(competitionsDeserializer));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyDictionary<Id<Championship>, Championship>> FetchChampionshipsAsync(Id<Sport> sportId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _championshipsDeserializer.Deserialize(
            await FileReader.ReadMatchesData("tournaments", cancellationToken)
                .ConfigureAwait(false));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyDictionary<Id<Competition>, Competition>> FetchCompetitionsAsync(Id<Sport> sportId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _competitionsDeserializer.Deserialize(
            await FileReader.ReadMatchesData("competitions", cancellationToken)
                .ConfigureAwait(false));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyDictionary<Id<Competitor>, Competitor>> FetchCompetitorsAsync(Id<Sport> sportId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _competitorsDeserializer.Deserialize(
            await FileReader.ReadMatchesData("competitions", cancellationToken)
                .ConfigureAwait(false));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyDictionary<Id<Match>, Match>> FetchMatchesAsync(Id<Sport> sportId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _matchesDeserializer.Deserialize(
            await FileReader.ReadMatchesData("matches", cancellationToken)
                .ConfigureAwait(false));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<Id<Match>>> FetchDeletedMatchesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _deletedMatchesDeserializer.Deserialize(
            await FileReader.ReadMatchesData("matches", cancellationToken)
                .ConfigureAwait(false));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyDictionary<Id<Sport>, Sport>> FetchSportsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _sportsDeserializer.Deserialize(
            await FileReader.ReadMatchesData("sports", cancellationToken)
                .ConfigureAwait(false));
    }

    private readonly IDeserializer<string, IReadOnlyDictionary<Id<Sport>, Sport>> _sportsDeserializer;
    private readonly IDeserializer<string, IReadOnlyDictionary<Id<Championship>, Championship>> _championshipsDeserializer;
    private readonly IDeserializer<string, IReadOnlyDictionary<Id<Team>, Team>> _teamsDeserializer;
    private readonly IDeserializer<string, IReadOnlyDictionary<Id<Match>, Match>> _matchesDeserializer;
    private readonly IDeserializer<string, IReadOnlyCollection<Id<Match>>> _deletedMatchesDeserializer;
    private readonly IDeserializer<string, IReadOnlyDictionary<Id<Competition>, Competition>> _competitionsDeserializer;
}
