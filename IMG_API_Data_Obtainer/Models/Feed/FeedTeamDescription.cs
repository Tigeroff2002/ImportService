using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Models.Internal;
using IMG_API_Data_Obtainer.TransportModels;

namespace IMG_API_Data_Obtainer.Models.Feed;

/// <summary>
/// Этот класс описывает команду (предоставлятся внешней системой).
/// </summary>
public sealed class FeedTeamDescription : MappableEntity<IntTeamDescription>
{
    public ExternalID<IntSportDescription> SportExternalKey { get; }

    public Name<FeedTeamDescription> Name { get; }

    public TeamType TeamType { get; }

    /// <summary>
    /// Создает экземпляр <see cref="FeedTeamDescription"/>.
    /// </summary>
    /// <param name="externalKey"></param>
    /// <param name="sportExternalKey"></param>
    /// <param name="name"></param>
    /// <param name="teamType"></param>
    public FeedTeamDescription(
        ExternalID<IntTeamDescription> externalKey,
        ExternalID<IntSportDescription> sportExternalKey,
        Name<FeedTeamDescription> name,
        TeamType teamType)
        : base(externalKey)
    {
        SportExternalKey = sportExternalKey;
        Name = name;
        TeamType = teamType;
    }
}
