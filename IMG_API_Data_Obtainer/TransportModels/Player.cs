using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels;

public sealed class Player
{
    [JsonProperty("id", Required = Required.Always)]
    public string Id { get; init; } = default!;

    [JsonProperty("firstName", Required = Required.Always)]
    public string FirstName { get; init; } = default!;

    [JsonProperty("lastName", Required = Required.Always)]
    public string LastName { get; init; } = default!;

    [JsonProperty("country", Required = Required.Always)]
    public string Country { get; init; } = default!;
}
