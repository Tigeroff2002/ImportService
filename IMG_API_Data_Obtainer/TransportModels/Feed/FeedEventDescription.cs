using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels.Feed;

/// <summary>
/// Представляет внешнее событие.
/// </summary>
public sealed class FeedEventDescription : FeedDescriptionBase
{
    /// <summary>
    /// Внешний идентификатор чемпионата.
    /// </summary>
    [JsonProperty("championship_external_key", Required = Required.Always)]
    public ExternalID ChampionshipExternalKey { get; init; } = default!;

    /// <summary>
    /// Тип теннисного матча.
    /// </summary>
    [JsonProperty("match_type", Required = Required.Always)]
    public MatchType MatchType { get; init; }

    /// <summary>
    /// Дата и время события.
    /// </summary>
    [JsonProperty("scheduled_start", Required = Required.Always)]
    public DateTimeOffset ScheduledStart { get; init; } = default!;

    /// <summary>
    /// Флаг, обозначающий отмену матча.
    /// </summary>
    [JsonProperty("is_cancelled", Required = Required.Always)]
    public bool IsCancelled { get; init; } = default!;

    /// <summary>
    /// Внешние команды события.
    /// </summary>
    [JsonProperty("two_teams", Required = Required.Always)]
    public FeedTwoTeamsCollectionDescription TwoTeamsCollection { get; init; } = default!;
}
