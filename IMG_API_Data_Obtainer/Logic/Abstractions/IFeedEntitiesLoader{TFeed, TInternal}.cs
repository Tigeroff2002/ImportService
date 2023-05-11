using ADF.Library.Models.Mapping;

namespace IMG_API_Data_Obtainer.Logic.Abstractions;

/// <summary xml:lang="ru">
/// Контракт загрузчика внешних сущностей.
/// </summary>
/// <typeparam xml:lang="ru" name="TFeed">
/// Тип внешней сущности.
/// </typeparam>
/// <typeparam xml:lang="ru" name="TInternal">
/// Тип внутренней сущности.
/// </typeparam>
public interface IFeedEntitiesLoader<TFeed, TInternal>
    where TFeed : MappableEntity<TInternal>
{
    /// <summary xml:lang="ru">
    /// Загружает внешние сущности.
    /// </summary>
    /// <param xml:lang="ru" name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// <see cref="Task"/>.
    /// </returns>
    Task LoadAsync(CancellationToken cancellationToken);
}