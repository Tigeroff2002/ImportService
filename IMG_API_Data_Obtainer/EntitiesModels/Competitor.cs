using IMG_API_Data_Obtainer.TransportModels;

namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель игрока.
/// </summary>
/// <param name="Id">
/// Идентификатор игрока.
/// </param>
/// <param name="Name">
/// Имя игрока.
/// </param>
/// <param name="SportId">
/// Идентификатор спорта игрока.
/// </param>
/// <param name="CountryId">
/// Идентификатор страны игрока.
/// </param>
/// <param name="Gender">
/// Пол игрока.
/// </param>
public sealed record class Competitor(
    Id<Competitor> Id,
    Name<Competitor> Name,
    Id<Sport> SportId,
    Id<Country> CountryId,
    PlayerGender Gender);