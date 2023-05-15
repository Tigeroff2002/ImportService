namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель соревнования.
/// </summary>
/// <param name="Id">
/// Идентификатор соревнования.
/// </param>
/// <param name="Name">
/// Название соревнования.
/// </param>
public sealed record class Competition(
    Name<Competition> Id,
    Name<Competition> Name);
