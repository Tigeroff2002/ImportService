using IMG_API_Data_Obtainer.Models;

namespace IMG_API_Data_Obtainer.Logic.Abstractions;

/// <summary>
/// Контракт загрузчика внешних сущностей.
/// </summary>
/// <typeparam name="TFeed">
/// Тип внешней сущности.
/// </typeparam>
/// <typeparam name="TInternal">
/// Тип внутренней сущности.
/// </typeparam>
public interface IFeedEntitiesLoader<TFeed, TInternal>
    where TFeed : MappableEntity<TInternal>
    where TInternal : IInternalEntityMarker
{
    /// <summary>
    /// Загружает внешние сущности.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// <see cref="Task"/>.
    /// </returns>
    Task LoadAsync(CancellationToken cancellationToken);
}