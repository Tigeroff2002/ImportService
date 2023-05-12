using ADF.Library.Models.Mapping;

namespace IMG_API_Data_Obtainer.Models;

/// <summary>
/// Определяет класс для сопоставляемой сущности.
/// </summary>
/// <typeparam name="TEntity">
/// Некоторая сущность.
/// </typeparam>
public abstract class MappableEntity<TEntity> where TEntity : IInternalEntityMarker
{
    /// <summary>
    /// Внешний ключ к данной сопоставляемой сущности.
    /// </summary>
    public ExternalID<TEntity> ExternalKey { get; }

    /// <summary>
    /// Внутренний ключ, получаемый от данной сущности.
    /// </summary>
    public InternalID<TEntity> InternalKey
        => _internal.IsKeySet
            ? _internal.Key
            : throw new InvalidOperationException(
                $"The {typeof(TEntity).Name} entity has not been mapped yet.");

    /// <summary>
    /// Сопоставляется с внутренним объектом.
    /// </summary>
    public bool IsMapped
        => _internal.IsKeySet;

    /// <summary>
    /// Создаёт экземпляр типа <see cref="MappableEntity{TEntity}"/>.
    /// </summary>
    /// <param name="externalKey">
    /// Внешний идентификатор данной сущности.
    /// </param>
    protected MappableEntity(ExternalID<TEntity> externalKey)
    {
        ExternalKey = externalKey;
        _internal = (default, false);
    }

    /// <summary>
    /// Связывает текущую сущность с внутренней сущностью.
    /// </summary>
    /// <param name="internalKey">
    /// Идентификатор внутренней сущности.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается, если сущность уже имеет связь с внутренней сущностью.
    /// </exception>
    public void Map(InternalID<TEntity> internalKey)
    {
        if (_internal.IsKeySet)
        {
            throw new InvalidOperationException(
                $"The {typeof(TEntity).Name} entity has been already mapped.");
        }

        _internal.Key = internalKey;
        _internal.IsKeySet = true;
    }

    private (InternalID<TEntity> Key, bool IsKeySet) _internal;
}
