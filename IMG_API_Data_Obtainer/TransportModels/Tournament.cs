using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels;

public sealed class Tournament
{
    [JsonProperty("identifier", Required = Required.Always)]
    public long Identifier { get; init; }

    [JsonProperty("year", Required = Required.Always)]
    public int Year { get; init; }

    [JsonProperty("sport", Required = Required.Always)]
    public string Sport { get; init; } = default!;

    [JsonProperty("countryCode", Required = Required.Always)]
    public string CountryCode { get; init; } = default!;

    [JsonProperty("tournamentName", Required = Required.Always)]
    public string TournamentName { get; init; } = default!;

    [JsonProperty("competitions", Required = Required.Always)]
    public Competition[] Competitions { get; init; } = Array.Empty<Competition>();
}
