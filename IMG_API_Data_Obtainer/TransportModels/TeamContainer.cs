using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels;

public sealed class TeamContainer
{
    [JsonProperty("status", Required = Required.Always)]
    public KnownTeamStatus KnownTeamStatus { get; init; }

    [JsonProperty("team", Required = Required.Always)]
    public Team Team { get; init; } = default!;
}
