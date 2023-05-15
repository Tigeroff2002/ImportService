using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Models.Internal;
using IMG_API_Data_Obtainer.Models;
using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels.Feed;

/// <summary>
/// 
/// </summary>
public sealed class FeedTeamDescription
{
    [JsonProperty("sport_external_key", Required = Required.Always)]
    public ExternalID SportExternalKey { get; init; } = default!;

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; } = default!;

    [JsonProperty("team_type", Required = Required.Always)]
    public TeamType TeamType { get; init; }

    [JsonProperty("known_team_status", Required = Required.Always)]
    public KnownTeamStatus KnownTeamStatus { get; }
}
