using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels.Feed;

/// <summary>
/// Представляет описание внешнего игрока.
/// </summary>
public sealed class FeedPlayerDescription : FeedDescriptionBase
{
    /// <summary>
    /// Внешний идентификатор спорта.
    /// </summary>
    [JsonProperty("sport_external_key", Required = Required.Always)]
    public ExternalID SportExternalKey { get; init; } = default!;

    /// <summary>
    /// Внешний идентификатор категории.
    /// </summary>
    [JsonProperty("category_external_key", Required = Required.Default)]
    public ExternalID CategoryExternalKey { get; init; } = default!;

    /// <summary>
    /// Название игрока.
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; init; } = default!;

    /// <summary>
    /// Пол игрока.
    /// </summary>
    [JsonProperty("gender", Required = Required.Always)]
    public PlayerGender Gender { get; init; }
}