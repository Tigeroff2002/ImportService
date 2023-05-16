namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель спорта.
/// </summary>
/// <param name="Id">
/// Идентификатор спорта.
/// </param>
/// <param name="Name">
/// Название спорта.
/// </param>
public sealed record class Sport(
    Id<Sport> Id,
    Name<Sport> Name);
