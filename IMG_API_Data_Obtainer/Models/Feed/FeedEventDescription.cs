using IMG_API_Data_Obtainer.Models.Internal;

using MatchType = IMG_API_Data_Obtainer.TransportModels.MatchType;

namespace IMG_API_Data_Obtainer.Models.Feed;

/// <summary>
/// Представляет описание события от поставщика (предоставляется внешней системой).
/// </summary>
public sealed class FeedEventDescription : MappableEntity<IntEventDescription>
{
    /// <summary>
    /// Идентификатор чемпионата.
    /// </summary>
    public ExternalID<IntChampionshipDescription> ChampionshipExternalKey { get; }

    /// <summary>
    /// Тип матча.
    /// </summary>
    public MatchType MatchType { get; }
 
    /// <summary>
    /// Запланированное время старта матча.
    /// </summary>
    public DateTimeOffset ScheduledStart { get; }

    /// <summary>
    /// Флаг, обозначающий отмену матча.
    /// </summary>
    public bool IsCancelled { get; }

    /// <summary>
    /// Команды - участники события.
    /// </summary>
    public FeedTwoTeamsCollectionDescription TeamParticipants { get; }

    /// <summary>
    /// Создаёт экземпляр типа <see cref="FeedEventDescription"/>.
    /// </summary>
    /// <param name="externalKey">
    /// Идентификатор события.
    /// </param>
    /// <param name="championshipExternalKey">
    /// Идентификатор чемпионата.
    /// </param>
    /// <param name="matchType">
    /// Тип матча.
    /// </param>
    /// <param name="scheduledStart">
    /// Запланированное время старта матча.
    /// </param>
    /// <param name="teamParticipants">
    /// Участники события.
    /// </param>
    /// <param name="isCancelled">
    /// Флаг, обозначающий отмену матча.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Если идентификатор чемпионата и идентификатор
    /// события из разных источников.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="participants"/> является <see langword="null"/>.
    /// </exception>
    public FeedEventDescription(
        ExternalID<IntEventDescription> externalKey,
        MatchType matchType,
        ExternalID<IntChampionshipDescription> championshipExternalKey,
        DateTimeOffset scheduledStart,
        FeedTwoTeamsCollectionDescription teamParticipants,
        bool isCancelled)
        : base(externalKey)
    {
        ValidateDate(scheduledStart);

        if (teamParticipants is null)
        {
            throw new ArgumentNullException(
                nameof(teamParticipants),
                "The collection of the participants is missing.");
        }
        if (teamParticipants is not FeedTwoTeamsCollectionDescription feedTwoTeams)
        {
            throw new ArgumentException($"Received unsupported feed participant collection ({teamParticipants.GetType().FullName})");
        }

        IsCancelled = isCancelled;
        ChampionshipExternalKey = championshipExternalKey;
        MatchType = matchType;
        ScheduledStart = scheduledStart;
        TeamParticipants = feedTwoTeams;
    }

    private static void ValidateDate(DateTimeOffset scheduledStart)
    {
        if (scheduledStart.Offset != TimeSpan.Zero)
        {
            throw new ArgumentException(
                "The starting time of the event is not in UTC.",
                nameof(scheduledStart));
        }
    }
}
