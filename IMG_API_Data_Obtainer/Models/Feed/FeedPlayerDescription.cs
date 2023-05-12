using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Models.Internal;

using Gender = IMG_API_Data_Obtainer.TransportModels.PlayerGender;

namespace IMG_API_Data_Obtainer.Models.Feed;

/// <summary>
/// Представляет описание игрока от поставщика (предоставляется внешней системой).
/// </summary>
public sealed class FeedPlayerDescription : MappableEntity<IntPlayerDescription>
{
    /// <summary>
    /// Внешний ключ к виду спорта, к которому прикреплен этот игрок.
    /// </summary>
    public ExternalID<IntSportDescription> SportExternalKey { get; }

    /// <summary>
    /// Внешний ключ категории, к которой прикреплен этот игрок.
    /// </summary>
    public ExternalID<IntCategoryDescription> CategoryExternalKey { get; }

    /// <summary>
    /// Имя игрока.
    /// </summary>
    public Name<FeedPlayerDescription> Name { get; }

    /// <summary>
    /// Пол игрока.
    /// </summary>
    public Gender Gender { get; }

    /// <summary>
    /// Создает экземпляр <see cref="FeedPlayerDescription"/>.
    /// </summary>
    /// <param name="externalKey">
    /// Внешний ключ игрока.
    /// </param>
    /// <param name="sportExternalKey">
    /// Внешний ключ спорта.
    /// </param>
    /// <param name="categoryExternalKey">
    /// Внешний ключ категории.
    /// </param>
    /// <param name="name">
    /// Имя игрока.
    /// </param>
    /// <param name="gender">
    /// Пол игрока.
    /// </param>
    public FeedPlayerDescription(
        ExternalID<IntPlayerDescription> externalKey, 
        ExternalID<IntSportDescription> sportExternalKey,
        ExternalID<IntCategoryDescription> categoryExternalKey,
        Name<FeedPlayerDescription> name, 
        Gender gender)
        : base(externalKey)
    {
        SportExternalKey = sportExternalKey;
        CategoryExternalKey = categoryExternalKey;
        Name = name;
        Gender = gender;
    }
}
