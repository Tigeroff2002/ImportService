using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Models.Internal;

namespace IMG_API_Data_Obtainer.Models.Feed;

/// <summary>
/// Этот класс описывает категорию (предоставлятся внешней системой).
/// </summary>
public sealed class FeedCategoryDescription : MappableEntity<IntCategoryDescription>
{
    /// <summary>
    /// Внешний ключ вида спорта (тенниса).
    /// </summary>
    public ExternalID<IntSportDescription> SportExternalKey { get; }

    /// <summary>
    /// Создает экземпляр <see cref="FeedCategoryDescription"/>.
    /// </summary>
    /// <param name="externalKey">
    /// Внешний ключ данной категории.
    /// </param>
    /// <param name="name">
    /// Название категории.
    /// </param>
    public FeedCategoryDescription(
        ExternalID<IntCategoryDescription> externalKey,
        ExternalID<IntSportDescription> sportExternalKey)
        : base(externalKey)
    {
        SportExternalKey = sportExternalKey;
    }
}

