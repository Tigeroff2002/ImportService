using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels;

public sealed class CompetitionFixture
{
    [JsonProperty("startTime", Required = Required.Always)]
    public StartTime StartTime { get; init; } = default!;

    [JsonProperty("matchType", Required = Required.Always)]
    public RawMatchType MatchType { get; init; }

    [JsonProperty("eventId", Required = Required.Always)]
    public string EventId { get; init; } = default!;

    [JsonProperty("status", Required = Required.Always)]
    public ResultStatus Status { get; init; }

    [JsonProperty("competitionId", Required = Required.Always)]
    public string CompetitionId { get; init; } = default!;

    [JsonProperty("teamA", Required = Required.Always)]
    public TeamContainer TeamA { get; init; } = default!;

    [JsonProperty("teamB", Required = Required.Always)]
    public TeamContainer TeamB { get; init; } = default!;  
}
