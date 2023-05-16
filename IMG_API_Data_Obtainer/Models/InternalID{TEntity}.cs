namespace IMG_API_Data_Obtainer.Models;

/// <summary>
/// Эта структура определяет идентификатор внутренней сущности,
/// которая сопоставляется с внешней системой.
/// </summary>
public readonly record struct InternalID<TEntity> where TEntity : IInternalEntityMarker
{
    /// <summary>
    /// Значение идентификатора.
    /// </summary>
    public long Value { get; }

    /// <summary>
    /// Создает экземпляр <see cref="InternalID{TEntity}"/>
    /// </summary>
    /// <param name="value">
    /// Значение идентификатора.
    /// </param>
    public InternalID(long value)
    {
        Value = value;
    }
}
