using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Feed.Models;

/// <summary>
/// Представляет описание внешнего спорта.
/// </summary>
public sealed class FeedSportDescription : FeedDescriptionBase
{
    /// <summary>
    /// Название спорта.
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; init; } = default!;
}