using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels.Feed;

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
}
