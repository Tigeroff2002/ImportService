using IMG_API_Data_Obtainer.TransportModels;

namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет команду.
/// </summary>
public sealed class Team : IEquatable<Team>
{
    public Name<Team> Id { get; }

    public Name<Team> FullName { get; }

    public TeamType TeamType { get; }

    public KnownTeamStatus Status { get; }

    public Id<Team> SportId { get; }

    /// <summary>
    /// Создает экземпляр <see cref="Team"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fullName"></param>
    /// <param name="teamType"></param>
    /// <param name="status"></param>
    public Team(
        Name<Team> id,
        Name<Team> fullName,
        TeamType teamType,
        KnownTeamStatus status)
    {
        Id = id;
        FullName = fullName;
        TeamType = teamType;
        Status = status;
        SportId = new(SPORT_TENNIS_ID);
    }

    private const long SPORT_TENNIS_ID = 5L;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as Team);

    /// <inheritdoc/>
    public bool Equals(Team? other) => other is Team team && team.Id == Id;

    /// <inheritdoc/>
    public override int GetHashCode() => Id.GetHashCode();
}
