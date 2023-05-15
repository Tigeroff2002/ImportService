namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Создает экземпляр <see cref="Tournament"/>.
/// </summary>
/// <param name="ChampionsipId"></param>
/// <param name="CountryId"></param>
/// <param name="SportId"></param>
/// <param name="CompetitionIds"></param>
/// <param name="TournamentName"></param>
/// <param name="Year"></param>
public sealed record class Tournament(
    Id<Championship> ChampionsipId,
    Id<Country> CountryId,
    Id<Sport> SportId,
    List<Id<Competition>> CompetitionIds,
    Name<Championship> TournamentName,
    int Year);
