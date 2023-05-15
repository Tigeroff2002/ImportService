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
    public Id<Match> Id { get; }

    /// <summary>
    /// Время начала матча.
    /// </summary>
    public DateTimeOffset ScheduledStart { get; }

    /// <summary>
    /// Идентификатор чемпионата.
    /// </summary>
    public Id<Championship> ChampionshipId { get; }

    /// <summary>
    /// Идентификатор команды A.
    /// </summary>
    public Id<Team> TeamA { get; }

    /// <summary>
    /// Идентификатор команды B.
    /// </summary>
    public Id<Team> TeamB { get; }

    /// <summary>
    /// Флаг, обозначающий отменен ли матч.
    /// </summary>
    public bool IsCancelled { get; }

    /// <summary>
    /// Тип матча.
    /// </summary>
    public TeamType MatchType { get;  }

    /// <summary>
    /// Создает экземпляр <see cref="Match"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="scheduledStart"></param>
    /// <param name="championshipId"></param>
    /// <param name="teamA"></param>
    /// <param name="teamB"></param>
    /// <param name="isCancelled"></param>
    /// <param name="matchType"></param>
    public Match(
        Id<Match> id,
        DateTimeOffset scheduledStart,
        Id<Championship> championshipId,
        Id<Team> teamA,
        Id<Team> teamB,
        bool isCancelled,
        TeamType matchType)
    {
        Id = id;
        ScheduledStart = scheduledStart;
        ChampionshipId = championshipId;
        TeamA = teamA;
        TeamB = teamB;
        IsCancelled = isCancelled;
        MatchType = matchType;
    }



    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as Match);

    /// <inheritdoc/>
    public bool Equals(Match? other) => other is Match match && match.Id == Id;

    /// <inheritdoc/>
    public override int GetHashCode() => Id.GetHashCode();
}
