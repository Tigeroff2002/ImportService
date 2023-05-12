using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Feed.Models;

/// <summary>
/// Представляет внешнюю категорию.
/// </summary>
public sealed class FeedCategoryDescription : FeedDescriptionBase
{
    /// <summary>
    /// Внешний идентификатор спорта.
    /// </summary>
    [JsonProperty("sport_external_key", NullValueHandling = NullValueHandling.Ignore)]
    public ExternalID? SportExternalKey { get; init; } = default!;

    /// <summary>
    /// Название категории.
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; init; } = default!;
}
