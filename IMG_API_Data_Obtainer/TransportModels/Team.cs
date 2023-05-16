using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.TransportModels;

public sealed class Team
{
    [JsonProperty("player1", Required = Required.Always)]
    public Player Player1 { get; init; } = default!;

    [JsonProperty("entryType", Required = Required.Always)]
    public MatchEntryType EntryType { get; init; }

    [JsonProperty("player2", NullValueHandling = NullValueHandling.Ignore)]
    public Player? Player2 { get; init; } = default!;
}
