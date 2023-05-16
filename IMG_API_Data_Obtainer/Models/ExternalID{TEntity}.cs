namespace IMG_API_Data_Obtainer.Models;

/// <summary>
/// Определяет идентификатор внешней системы.
/// </summary>
public readonly record struct ExternalID<TEntity>
{
    /// <summary>
    /// Идентификатор поставщика.
    /// </summary>
    public short ProviderId => IMG_PROVIDER_ID;

    /// <summary>
    /// Значение идентификатора.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создает экземпляр структуры <see cref="ExternalID{TEntity}"/>
    /// </summary>
    /// <param name="value">
    /// Значение идентификатора.
    /// </param>
    public ExternalID(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "The identifier's value cannot be null, empty or whitespances.",
                nameof(value));
        }

        Value = value;
    }

    private const int IMG_PROVIDER_ID = 4; // IMG Provider
}
