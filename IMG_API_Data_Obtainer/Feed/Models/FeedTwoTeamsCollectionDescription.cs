using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Feed.Models;

/// <summary>
/// Представляет коллекцию из двух внешних команд.
/// </summary>
public sealed class FeedTwoTeamsCollectionDescription
{
    /// <summary>
    /// Идентификатор команды хозяев.
    /// </summary>
    [JsonProperty("teamA", Required = Required.Always)]
    public ExternalID TeamA { get; init; } = default!;

    /// <summary>
    /// Идентификатор команды гостей.
    /// </summary>
    [JsonProperty("teamB", Required = Required.Always)]
    public ExternalID TeamB { get; init; } = default!;
}
