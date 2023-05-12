using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels.Feed;

/// <summary>
/// Описание теннисной команды.
/// </summary>
public abstract class FeedTwoTeamsCollectionDescription 
{
    /// <summary>
    /// Статус внесения в реестр матча команды А.
    /// </summary>
    [JsonProperty("teamA_entry_type", Required = Required.Default)]
    public EntryType TeamAEntryType { get; init; }

    /// <summary>
    /// Статус внесения в реестр матча команды B.
    /// </summary>
    [JsonProperty("teamB_entry_type", Required = Required.Default)]
    public EntryType TeamBEntryType { get; init; }

    /// <summary>
    /// Статус известности команды А в реестре.
    /// </summary>
    [JsonProperty("teamA_known_status", Required = Required.Always)]
    public KnownTeamStatus TeamAKnownStatus { get; init; }

    /// <summary>
    /// Статус известности команды B в реестре.
    /// </summary>
    [JsonProperty("teamB_known_status", Required = Required.Always)]
    public KnownTeamStatus TeamBKnownStatus { get; init; }
}
