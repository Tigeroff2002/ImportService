using IMG_API_Data_Obtainer.EntitiesModels;

namespace IMG_API_Data_Obtainer.Logic.Abstractions;

/// <summary>
/// Дессериализатор структуры сущностей из внешних систем.
/// </summary>
public interface IEntitiesStructureFetcher
{
    /// <summary>
    /// Извлекает спорты.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// Словарь спортов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Если операция отменена.
    /// </exception>
    Task<IReadOnlyDictionary<Id<Sport>, Sport>> FetchSportsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Извлекает 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<Country>> FetchCountriesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Извлекает чемпионаты.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// Словарь чемпионатов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Если операция отменена.
    /// </exception>
    Task<IReadOnlyList<Tournament>> FetchTournamentsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Извлекает соревнования.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// Словарь соревнований.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Если операция отменена.
    /// </exception>
    Task<IReadOnlyList<Competition>> FetchCompetitionsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Извлекает команды.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<Team>> FetchTeamsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Извлекает матчи.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// Словарь матчей.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Если операция отменена.
    /// </exception>
    Task<IReadOnlyList<Match>> FetchMatchesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Извлекает идентификаторы удаленных матчей.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// Идентификаторы матчей.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Если операция отменена.
    /// </exception>
    Task<IReadOnlyList<Name<Match>>> FetchDeletedMatchesAsync(CancellationToken cancellationToken);
}
