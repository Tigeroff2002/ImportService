using IMG_API_Data_Obtainer.EntitiesModels;
using System.Diagnostics.Metrics;

namespace IMG_API_Data_Obtainer.Logic.Abstractions;

/// <summary>
/// Дессериализатор структуры сущностей из внешних систем.
/// </summary>
public interface IEntitiesStructureDeserializer
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
    /// Извлекает чемпионаты.
    /// </summary>
    /// <param name="sportId">
    /// Идентификатор спорта.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// Словарь чемпионатов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Если операция отменена.
    /// </exception>
    Task<IReadOnlyDictionary<Id<Championship>, Championship>> FetchChampionshipsAsync(
        Id<Sport> sportId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Извлекает участников.
    /// </summary>
    /// <param name="sportId">
    /// Идентификатор спорта.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// Словарь участников.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Если операция отменена.
    /// </exception>
    Task<IReadOnlyDictionary<Id<Competitor>, Competitor>> FetchCompetitorsAsync(
        Id<Sport> sportId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Извлекает матчи.
    /// </summary>
    /// <param name="sportId">
    /// Идентификатор спорта.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// Словарь матчей.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Если операция отменена.
    /// </exception>
    Task<IReadOnlyDictionary<Id<Match>, Match>> FetchMatchesAsync(
        Id<Sport> sportId,
        CancellationToken cancellationToken);

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
    Task<IReadOnlyCollection<Id<Match>>> FetchDeletedMatchesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Извлекает соревнования.
    /// </summary>
    /// <param name="sportId">
    /// Идентификатор спорта.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены операции.
    /// </param>
    /// <returns>
    /// Словарь соревнований.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Если операция отменена.
    /// </exception>
    Task<IReadOnlyDictionary<Id<Competition>, Competition>> FetchCompetitionsAsync(
        Id<Sport> sportId,
        CancellationToken cancellationToken);
}
