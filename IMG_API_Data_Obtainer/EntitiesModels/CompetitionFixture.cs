namespace IMG_API_Data_Obtainer.EntitiesModels;

public sealed record class CompetitionFixture(
    Id<Competition> CompetitionId,
    DateTimeOffset ScheduledStart,
    Id<Match> MatchId);
