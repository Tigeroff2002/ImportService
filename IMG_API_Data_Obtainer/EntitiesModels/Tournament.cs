using System.Diagnostics.Metrics;

namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель чемпионата.
/// </summary>
/// <param name="Id">
/// Идентификатор чемпионата.
/// </param>
/// <param name="FullName">
/// Полное название чемпионата
/// включающее название лиги или события и информацию о сезоне или годе.
/// </param>
/// <param name="SportId">
/// Идентификатор спорта.
/// </param>
/// <param name="CountryId">
/// Идентификатор страны.
/// </param>
/// <param name="CompetitionId">
/// Идентификатор соревнования.
/// </param>
/// <param name="Year">
/// Год проведения.
/// </param>
public sealed record class Championship(
    Id<Championship> Id,
    Name<Championship> FullName,
    Id<Sport> SportId,
    Id<Country> CountryId,
    Id<Competition> CompetitionId,
    int Year);