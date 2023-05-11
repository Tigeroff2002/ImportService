﻿using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Feed.Models;

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
    /// Внешний идентификатор команды.
    /// </summary>
    [JsonProperty("team_external_key", NullValueHandling = NullValueHandling.Ignore)]
    public ExternalID? TeamExternalKey { get; init; } = default!;

    /// <summary>
    /// Название игрока.
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; init; } = default!;
}