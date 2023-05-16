using IMG_API_Data_Obtainer.TransportModels;

namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет внешний чемпионат.
/// </summary>

public sealed class Tournament : IEquatable<Tournament>
{
    public Name<Tournament> Id { get; }

    public Name<Tournament> TournamentName { get; set; }

    public Id<Sport> SportId { get; }

    public Name<Country> CountryId { get; }

    public int Year { get; }

    public List<string> CompetitionIds { get; }

    public RawMatchType MatchType { get; set; }

    /// <summary>
    /// Создает экземпляр <see cref="Tournament"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tournamentName"></param>
    /// <param name="sportId"></param>
    /// <param name="countryId"></param>
    /// <param name="year"></param>
    /// <param name="competitionIds"></param>
    public Tournament(
        Name<Tournament> id,
        Name<Tournament> tournamentName,
        Id<Sport> sportId, 
        Name<Country> countryId,
        int year,
        List<string> competitionIds)
    {
        Id = id;
        TournamentName = tournamentName;
        SportId = sportId;
        CountryId = countryId;
        Year = year;
        CompetitionIds = competitionIds;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as Tournament);

    /// <inheritdoc/>
    public bool Equals(Tournament? other) => other is Tournament tournament && tournament.Id == Id;

    /// <inheritdoc/>
    public override int GetHashCode() => Id.GetHashCode();
}
