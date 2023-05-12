using ADF.Library.Models.Mapping;
using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Models.Internal;

namespace IMG_API_Data_Obtainer.Models.Feed;

/// <summary>
/// Этот класс описывает вид спорта (предоставляется внешней системой).
/// </summary>
public class FeedSportDescription : MappableEntity<IntSportDescription>
{
    /// <summary>
    /// Название вида спорта.
    /// </summary>
    public Name<FeedSportDescription> Name { get; }

    /// <summary>
    /// Создаёт экземпляр <see cref="FeedSportDescription"/>.
    /// </summary>
    /// <param name="externalKey">
    /// Внешний идентификатор вида спорта.
    /// </param>
    /// <param name="name">
    /// Название вида спорта.
    /// </param>
    public FeedSportDescription(
        ExternalID<IntSportDescription> externalKey,
        Name<FeedSportDescription> name)
        : base(externalKey)
    {
        Name = name;
    }
}
