using IMG_API_Data_Obtainer.TransportModels;

namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель матча.
/// </summary>
public sealed class Match : IEquatable<Match>
{
    /// <summary>
    /// Идентификатор матча.
    /// </summary>
    public Name<Match> Id { get; }

    /// <summary>
    /// Название матча.
    /// </summary>
    public Name<Match> Name { get; }

    /// <summary>
    /// Время начала матча.
    /// </summary>
    public DateTimeOffset ScheduledStart { get; }

    /// <summary>
    /// Идентификатор соревнования.
    /// </summary>
    public Name<Competition> CompetitionId { get; }

    /// <summary>
    /// Идентификатор команды A.
    /// </summary>
    public Name<Team> TeamA { get; }

    /// <summary>
    /// Идентификатор команды B.
    /// </summary>
    public Name<Team> TeamB { get; }

    /// <summary>
    /// Флаг, обозначающий отменен ли матч.
    /// </summary>
    public bool IsCancelled { get; }

    /// <summary>
    /// Тип команды, принимающей участие в матче.
    /// </summary>
    public TeamType TeamType { get; }

    /// <summary>
    /// Тип матча.
    /// </summary>
    public RawMatchType MatchType { get; }

    /// <summary>
    /// Создает экземпляр <see cref="Match"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="matchName">
    /// </param>
    /// <param name="competitionId">
    /// </param>
    /// <param name="scheduledStart"></param>
    /// <param name="teamA"></param>
    /// <param name="teamB"></param>
    /// <param name="isCancelled"></param>
    /// <param name="matchType"></param>
    /// <param name="teamType"></param>
    public Match(
        Name<Match> id,
        Name<Match> matchName,
        DateTimeOffset scheduledStart,
        Name<Competition> competitionId,
        Name<Team> teamA,
        Name<Team> teamB,
        bool isCancelled,
        RawMatchType matchType,
        TeamType teamType)
    {
        Id = id;
        Name = matchName;
        ScheduledStart = scheduledStart;
        CompetitionId = competitionId;
        TeamA = teamA;
        TeamB = teamB;
        IsCancelled = isCancelled;
        MatchType = matchType;
        TeamType = teamType;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as Match);

    /// <inheritdoc/>
    public bool Equals(Match? other) => other is Match match && match.Id == Id;

    /// <inheritdoc/>
    public override int GetHashCode() => Id.GetHashCode();
}
