using IMG_API_Data_Obtainer.TransportModels;
using MatchType = IMG_API_Data_Obtainer.TransportModels.MatchType;

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
    /// Идентификаторы игрока (ов) команды A.
    /// </summary>
    public List<Id<Competitor>> TeamAPlayersIds { get; }

    /// <summary>
    /// Идентификаторы игрока (ов) команды B.
    /// </summary>
    public List<Id<Competitor>> TeamBPlayersIds { get; }

    /// <summary>
    /// Статус вхождения теннисной команды A в реестр.
    /// </summary>
    public EntryType TeamAEntry { get; }

    /// <summary>
    /// Статус вхождения теннисной команды B в реестр.
    /// </summary>
    public EntryType TeamBEntry { get; }

    /// <summary>
    /// Статус известности теннисной команды A в реестре.
    /// </summary>
    public KnownTeamStatus TeamAKnownStatus { get; }

    /// <summary>
    /// Статус известности теннисной команды B в реестре.
    /// </summary>
    public KnownTeamStatus TeamBKnownStatus { get; }

    /// <summary>
    /// Флаг, обозначающий отменен ли матч.
    /// </summary>
    public bool IsCancelled { get; }

    /// <summary>
    /// Тип матча.
    /// </summary>
    public MatchType MatchType { get;  }

    /// <summary>
    /// Создаёт экземпляр типа <see cref="Match"/>.
    /// </summary>
    /// <param name="id">
    /// Идентификатор матча.
    /// </param>
    /// <param name="scheduledStart">
    /// Время начала матча.
    /// </param>
    /// <param name="championshipId">
    /// Идентификатор чемпионата.
    /// </param>
    /// <param name="matchType">
    /// Тип матча.
    /// </param>
    /// <param name="teamAPlayersIds">
    /// Идентификаторы игрока (ов) команды A.
    /// </param>
    /// <param name="teamBPlayersIds">
    /// Идентификаторы игрока (ов) команды B.
    /// </param>
    /// <param name="isCancelled">
    /// Флаг, обозначающий отменен ли матч.
    /// </param>
    public Match(
        Id<Match> id,
        DateTimeOffset scheduledStart,
        Id<Championship> championshipId,
        MatchType matchType,
        EntryType teamAEntry,
        EntryType teamBEntry,
        KnownTeamStatus teamAKnownStatus,
        KnownTeamStatus teamBKnownStatus,
        List<Id<Competitor>> teamAPlayersIds,
        List<Id<Competitor>> teamBPlayersIds,
        bool isCancelled)
    {
        if (teamAPlayersIds == null)
        {
            throw new ArgumentNullException(nameof(teamAPlayersIds));
        }
        if (teamBPlayersIds == null)
        {
            throw new ArgumentNullException(nameof(teamBPlayersIds));
        }

        if (teamAPlayersIds.Count == 0 || teamAPlayersIds.Count > 2)
        {
            throw new ArgumentException($"Collection {nameof(teamAPlayersIds)} has incorrect number of players in tennis team");
        }

        if (teamBPlayersIds.Count == 0 || teamBPlayersIds.Count > 2)
        {
            throw new ArgumentException($"Collection {nameof(teamBPlayersIds)} has incorrect number of players in tennis team");
        }

        foreach(var entryA in teamAPlayersIds)
        {
            foreach(var entryB in teamBPlayersIds)
            {
                if (entryA == entryB)
                {
                    throw new InvalidOperationException(
                        $"Match with id = {id} cannot be created " +
                        "with equivalent players ids in different teams.");
                }
            }
        }

        Id = id;
        ScheduledStart = scheduledStart;
        ChampionshipId = championshipId;
        MatchType = matchType;
        TeamAEntry = teamAEntry;
        TeamBEntry = teamBEntry;
        TeamAKnownStatus = teamAKnownStatus;
        TeamBKnownStatus = teamBKnownStatus;
        TeamAPlayersIds = teamAPlayersIds;
        TeamBPlayersIds = teamBPlayersIds;
        IsCancelled = isCancelled;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as Match);

    /// <inheritdoc/>
    public bool Equals(Match? other) => other is Match match && match.Id == Id;

    /// <inheritdoc/>
    public override int GetHashCode() => Id.GetHashCode();
}
