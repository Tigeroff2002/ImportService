using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Models.Internal;

namespace IMG_API_Data_Obtainer.Models.Feed;

/// <summary>
/// Этот класс описывает чемпионат (предоставляется внешней системой).
/// </summary>
public sealed class FeedChampionshipDescription : MappableEntity<IntChampionshipDescription>
{
    /// <summary>
    /// Внешний ключ к виду спорта, к которому относится этот чемпионат.
    /// </summary>
    public ExternalID<IntSportDescription> SportExternalKey { get; }

    /// <summary>
    /// Внешний ключ категории, к которой относится этот чемпионат.
    /// </summary>
    public ExternalID<IntCategoryDescription> CategoryExternalKey { get; }

    /// <summary>
    /// Название чемпионата.
    /// </summary>
    public Name<FeedChampionshipDescription> Name { get; }

    /// <summary>
    /// Год проведения чемпионата.
    /// </summary>
    public int Year { get;  }

    /// <summary>
    /// Создает экземпляр <see cref="FeedChampionshipDescription"/>.
    /// </summary>
    /// <param name="externalKey">
    /// Внешний ключ данного чемпионата.
    /// </param>
    /// <param name="sportExternalKey">
    /// Внешний ключ к виду спорта, к которому относится этот чемпионат.
    /// </param>
    /// <param name="categoryExternalKey">
    /// Внешний ключ категории, к которой относится этот чемпионат.
    /// </param>
    /// <param name="name">
    /// Название чемпионата.
    /// </param>
    /// <param name="year">
    /// Год проведения чемпионата.
    /// </param>
    public FeedChampionshipDescription(
        ExternalID<IntChampionshipDescription> externalKey,
        ExternalID<IntSportDescription> sportExternalKey,
        ExternalID<IntCategoryDescription> categoryExternalKey,
        Name<FeedChampionshipDescription> name,
        int year)
        : base(externalKey)
    {
        SportExternalKey = sportExternalKey;
        CategoryExternalKey = categoryExternalKey;
        Name = name;
        Year = year;
    }
}
