using IMG_API_Data_Obtainer.TransportModels;
using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Models.Feed;

/// Общее описание теннисной команды (предоставляется внешней системой).
public abstract class FeedTwoTeamsCollectionDescription
{
    /// <summary>
    /// Статус внесения в реестр матча команды А.
    /// </summary>
    public EntryType TeamAEntryType { get; }

    /// <summary>
    /// Статус внесения в реестр матча команды B.
    /// </summary>
    public EntryType TeamBEntryType { get; }

    /// <summary>
    /// Статус известности команды А в реестре.
    /// </summary>
    public KnownTeamStatus TeamAKnownStatus { get; init; }

    /// <summary>
    /// Статус известности команды B в реестре.
    /// </summary>
    public KnownTeamStatus TeamBKnownStatus { get; }

    /// <summary>
    /// Создает экземпляр <see cref="FeedTwoTeamsCollectionDescription"/>.
    /// </summary>
    /// <param name="teamAEntryType">
    /// Статус внесения в реестр матча команды А.
    /// </param>
    /// <param name="teamBEntryType">
    /// Статус внесения в реестр матча команды B.
    /// </param>
    /// <param name="teamAKnownStatus">
    /// Статус известности команды А в реестре.
    /// </param>
    /// <param name="teamBKnownStatus">
    /// Статус внесения в реестр матча команды B.
    /// </param>
    protected FeedTwoTeamsCollectionDescription(
        EntryType teamAEntryType,
        EntryType teamBEntryType,
        KnownTeamStatus teamAKnownStatus,
        KnownTeamStatus teamBKnownStatus)
    {
        TeamAEntryType = teamAEntryType;
        TeamBEntryType = teamBEntryType;
        TeamAKnownStatus = teamAKnownStatus;
        TeamBKnownStatus = teamBKnownStatus;
    }
}
