using IMG_API_Data_Obtainer.TransportModels;

namespace IMG_API_Data_Obtainer.EntitiesModels;

public sealed record class Team(
    Id<Player> Player1,
    Id<Player> Player2,
    Name<Team> Name,
    MatchEntryType Status);
