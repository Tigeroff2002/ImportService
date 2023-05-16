using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels.Feed;

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
    [JsonProperty("tournament_name", Required = Required.Always)]
    public string Name { get; init; } = default!;

    /// <summary>
    /// Год проведения чемпионата.
    /// </summary>
    [JsonProperty("year", Required = Required.Default)]
    public int Year { get; init; }

    [JsonProperty("match_type", Required = Required.Always)]
    public RawMatchType MatchType { get; init; }
}
