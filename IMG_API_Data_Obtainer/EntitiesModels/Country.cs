namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель страны.
/// </summary>
/// <param name="Id">
/// Идентификатор страны.
/// </param>
/// <param name="Name">
/// Название страны.
/// </param>
public sealed record class Country(
    Name<Country> Name);