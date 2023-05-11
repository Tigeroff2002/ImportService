using IMG_API_Data_Obtainer.Feed.Models;
using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Feed;

/// <summary>
/// Представляет внешний идентификатор.
/// </summary>
public sealed class ExternalID
{
    /// <summary>
    /// Значение внешнего идентификатора.
    /// </summary>
    [JsonProperty("value", Required = Required.Always)]
    public string Value { get; init; } = default!;

    /// <summary>
    /// Идентификатор внешнего источника.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Выбрасывается в случае, если значение
    /// не равно <see cref="IMG_PROVIDER_ID"/>.
    /// </exception>
    [JsonProperty("source", Required = Required.Always)]
    public ExternalSystem SourceId
    {
        get => _sourceId;
        init => _sourceId = value == IMG_PROVIDER_ID
            ? value
            : throw new ArgumentException($"Invalid source id: {value}. " +
                $"Expected source id: {IMG_PROVIDER_ID}");
    }

    private ExternalSystem _sourceId;
    private const ExternalSystem IMG_PROVIDER_ID = ExternalSystem.IMG;
}
