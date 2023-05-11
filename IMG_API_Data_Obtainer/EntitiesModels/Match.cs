namespace IMG_API_Data_Obtainer.EntitiesModels;

/// <summary>
/// Представляет модель матча.
/// </summary>
public sealed class Match : IEquatable<Match>
{
    /// <summary>
    /// Идентификатор матча.
    /// </summary>
    public Id<Match> Id { get; }

    /// <summary>
    /// Время начала матча.
    /// </summary>
    public DateTimeOffset ScheduledStart { get; }

    /// <summary>
    /// Идентификатор чемпионата.
    /// </summary>
    public Id<Championship> ChampionshipId { get; }

    /// <summary>
    /// Идентификатор команды "хозяев".
    /// </summary>
    public Id<Competitor> HomeId { get; }

    /// <summary>
    /// Идентификатор команды "гостей".
    /// </summary>
    public Id<Competitor> AwayId { get; }

    /// <summary>
    /// Флаг, обозначающий отменен ли матч.
    /// </summary>
    public bool IsCancelled { get; }

    /// <summary>
    /// Создаёт экземпляр типа <see cref="Match"/>.
    /// </summary>
    /// <param name="id">
    /// Идентификатор матча.
    /// </param>
    /// <param name="scheduledStart">
    /// Время начала матча.
    /// </param>
    /// <param name="championshipId">
    /// Идентификатор чемпионата.
    /// </param>
    /// <param name="home">
    /// Идентификатор команды "хозяев".
    /// </param>
    /// <param name="away">
    /// Идентификатор команды "гостей".
    /// </param>
    /// <param name="isCancelled">
    /// Флаг, обозначающий отменен ли матч.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Если <paramref name="home"/> равен <paramref name="away"/>.
    /// </exception>
    public Match(
        Id<Match> id,
        DateTimeOffset scheduledStart,
        Id<Championship> championshipId,
        Id<Competitor> home,
        Id<Competitor> away,
        bool isCancelled)
    {
        if (home == away)
        {
            throw new InvalidOperationException(
                "Match cannot be created " +
                "with equivalent team ids.");
        }

        Id = id;
        ScheduledStart = scheduledStart;
        ChampionshipId = championshipId;
        HomeId = home;
        AwayId = away;
        IsCancelled = isCancelled;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as Match);

    /// <inheritdoc/>
    public bool Equals(Match? other) => other is Match match && match.Id == Id;

    /// <inheritdoc/>
    public override int GetHashCode() => Id.GetHashCode();
}
