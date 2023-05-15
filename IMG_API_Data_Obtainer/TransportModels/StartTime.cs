using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels;

public sealed class StartTime
{
    [JsonProperty("status", Required = Required.Always)]
    public string Status { get; init; } = default!;

    [JsonProperty("time", Required = Required.Always)]
    public DateTimeOffset Time { get; init; } = default!;   
}
