using IMG_API_Data_Obtainer.Models;

namespace IMG_API_Data_Obtainer.Logic.Abstractions;

/// <summary>
/// Контракт репозитория внешних сущностей.
/// </summary>
public interface IFeedEntitiesRepository
{
    /// <summary>
    /// Считывает внешние сущности типа <typeparamref name="TFeed"/>.
    /// </summary>
    /// <typeparam name="TFeed">
    /// Тип внешней сущности.
    /// </typeparam>
    /// <typeparam name="TInternal">
    /// Тип внутренней сущности.
    /// </typeparam>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// Словарь внешних сущностей.
    /// </returns>
    Task<IReadOnlyDictionary<ExternalID<TInternal>, TFeed>>
        GetAllAsync<TFeed, TInternal>(CancellationToken cancellationToken)
        where TFeed : MappableEntity<TInternal>
        where TInternal : IInternalEntityMarker;
}

