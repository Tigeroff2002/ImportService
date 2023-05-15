using IMG_API_Data_Obtainer.TransportModels;

namespace IMG_API_Data_Obtainer.EntitiesModels;

public sealed record class Team(
    Name<Team> FullName,
    RangeType RangeType,
    PlayersCollection PlayersCollection,
    KnownTeamStatus Status);
