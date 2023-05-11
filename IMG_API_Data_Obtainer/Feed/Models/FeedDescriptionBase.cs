using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Feed.Models;

/// <summary>
/// Представляет сопоставляемую внешнюю сущность фида.
/// </summary>
public abstract class FeedDescriptionBase
{
    /// <summary>
    /// Внешний идентификатор.
    /// </summary>
    [JsonProperty("external_key", Required = Required.Always)]
    public ExternalID ExternalKey { get; init; } = default!;

    /// <summary>
    /// Внутренний идентификатор.
    /// </summary>
    [JsonProperty("internal_key", Required = Required.AllowNull)]
    public InternalID? InternalKey { get; init; } = default!;

    /// <summary>
    /// Флаг, который означает изменена ли данная сущность импортом.
    /// </summary>
    [JsonProperty("is_changed_by_import", Required = Required.Default)]
    public bool IsChangedByImport { get; set; }
}
