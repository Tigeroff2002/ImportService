namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель страны.
/// </summary>
/// <param name="Id">
/// Идентификатор (название) страны.
/// </param>
public sealed record class Country(
    Name<Country> Id);