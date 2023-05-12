using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels.Feed;

/// <summary>
/// Описание соло-команды матча в теннис.
/// </summary>
public sealed class FeedTwoSoloTeamsCollectionDescription : FeedTwoTeamsCollectionDescription
{
    /// <summary>
    /// Соло игрок команды А.
    /// </summary>
    [JsonProperty("teamA_player", Required = Required.Always)]
    public ExternalID TeamAPlayer { get; init; } = default!;

    /// <summary>
    /// Соло игрок команды B.
    /// </summary>
    [JsonProperty("teamB_player", Required = Required.Always)]
    public ExternalID TeamBPlayer { get; init; } = default!;
}
