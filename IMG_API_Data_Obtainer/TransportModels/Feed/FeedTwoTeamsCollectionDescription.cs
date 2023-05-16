using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels.Feed;

/// <summary>
/// Общее описание теннисной команды.
/// </summary>
public sealed class FeedTwoTeamsCollectionDescription 
{
    /// <summary>
    /// Идентификатор команды А.
    /// </summary>
    [JsonProperty("teamA", Required = Required.Always)]
    public ExternalID TeamA { get; init; } = default!;

    /// <summary>
    /// Идентификатор команды B.
    /// </summary>
    [JsonProperty("teamB", Required = Required.Always)]
    public ExternalID TeamB { get; init; } = default!;
}
