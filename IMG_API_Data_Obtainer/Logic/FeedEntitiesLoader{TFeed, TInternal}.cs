using ADF.Library.Models.Mapping;
using IMG_API_Data_Obtainer.Logic.Abstractions;

namespace IMG_API_Data_Obtainer.Logic;

/// <summary>
/// Представляет загрузчика внешних сущностей.
/// </summary>
/// <typeparam name="TFeed">
/// Тип внешней сущности.
/// </typeparam>
/// <typeparam name="TInternal">
/// Тип внутренней сущности.
/// </typeparam>
public sealed class FeedEntitiesLoader<TFeed, TInternal> : IFeedEntitiesLoader<TFeed, TInternal>
    where TFeed : MappableEntity<TInternal>
{
    /// <summary>
    /// Создаёт экземпляр типа <see cref="FeedEntitiesLoader{TFeed, TInternal}"/>.
    /// </summary>
    /// <param name="state">
    /// Объект, управляющий обновлениями внешних сущностей.
    /// </param>
    /// <param name="repository">
    /// Репозиторий внешних сущностей.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public FeedEntitiesLoader(
        IFeedEntitiesRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <inheritdoc/>
    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        var readEntities = await _repository.GetAllAsync<TFeed, TInternal>(cancellationToken)
            .ConfigureAwait(false);

        Console.WriteLine($"{nameof(TFeed)} - название фида");

        foreach (var entity in readEntities.Values)
        {
            Console.WriteLine($"Entity with external key = {entity.ExternalKey}," +
                $" internal key = {entity.InternalKey}," +
                $" is_changedBy_import = {entity.IsChangedByImport}," +
                $" is_mapped = {entity.IsMapped}," +
                $" is_rejected = {entity.IsRejected}");
        }
    }

    private readonly IFeedEntitiesRepository _repository;
}
