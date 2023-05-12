using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Feed.Models;

/// <summary>
/// Представляет описание внешней команды (участника).
/// </summary>
public sealed class FeedTeamDescription : FeedDescriptionBase
{
    /// <summary>
    /// Внешний идентификатор спорта.
    /// </summary>
    [JsonProperty("sport_external_key", Required = Required.Always)]
    public ExternalID SportExternalKey { get; init; } = default!;

    /// <summary>
    /// Название команды (участника).
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; init; } = default!;
}
