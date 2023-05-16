using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels;

public sealed class Competition
{
    [JsonProperty("competitionId", Required = Required.Always)]
    public string CompetitionID { get; init; } = default!;
}
