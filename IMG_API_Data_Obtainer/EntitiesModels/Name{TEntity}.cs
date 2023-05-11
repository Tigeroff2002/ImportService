namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель названия.
/// </summary>
/// <typeparam name="T">
/// Маркерный тип, для которого создаётся название.
/// </typeparam>
public readonly record struct Name<T>
{
    /// <summary>
    /// Значение названия.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт экземпляр типа <see cref="Name{T}"/>.
    /// </summary>
    /// <param name="value">
    /// Значение названия.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Если <paramref name="value"/> состоит из специальных символов разделителей,
    /// пуста или является <see langword="null"/>.
    /// </exception>
    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Name value cannot be empty, or null, or has only white-space characters.",
                nameof(value));
        }

        Value = value;
    }
}
