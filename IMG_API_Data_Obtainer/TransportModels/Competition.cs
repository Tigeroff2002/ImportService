using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels;

public sealed class Competition
{
    [JsonProperty("externalId", Required = Required.Always)]
    public string ExternalID { get; init; } = default!;

    [JsonProperty("competitionId", Required = Required.Always)]
    public string CompetitionID { get; init; } = default!;
}
