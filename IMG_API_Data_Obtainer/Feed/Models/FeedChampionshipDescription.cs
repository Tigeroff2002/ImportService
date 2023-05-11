using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Feed.Models;

/// <summary>
/// Представляет внешний чемпионат.
/// </summary>
public sealed class FeedChampionshipDescription : FeedDescriptionBase
{
    /// <summary>
    /// Внешний идентификатор спорта.
    /// </summary>
    [JsonProperty("sport_external_key", Required = Required.Always)]
    public ExternalID SportExternalKey { get; init; } = default!;

    /// <summary>
    /// Внешний идентификатор категории.
    /// </summary>
    [JsonProperty("category_external_key", Required = Required.Always)]
    public ExternalID CategoryExternalKey { get; init; } = default!;

    /// <summary>
    /// Название чемпионата.
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; init; } = default!;
}