namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель идентификатора.
/// </summary>
/// <typeparam name="T">
/// Маркерный тип, для которого создаётся идентификатор.
/// </typeparam>
/// <param name="Value">
/// Значение идентификатора.
/// </param>
public record struct Id<T>(long Value);
