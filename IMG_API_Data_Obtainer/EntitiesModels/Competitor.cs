namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель участника.
/// </summary>
/// <param name="Id">
/// Идентификатор участника.
/// </param>
/// <param name="Name">
/// Название участника.
/// </param>
/// <param name="SportId">
/// Идентификатор спорта.
/// </param>
public sealed record class Competitor(
    Id<Competitor> Id,
    Name<Competitor> Name,
    Id<Sport> SportId);