using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Feed;

/// <summary>
/// Представляет внутренний идентификатор.
/// </summary>
public sealed class InternalID
{
    /// <summary>
    /// Значение внутреннего идентификатора.
    /// </summary>
    [JsonProperty("value", Required = Required.Always)]
    public long Value { get; set; }
}

