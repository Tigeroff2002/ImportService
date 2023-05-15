using IMG_API_Data_Obtainer.Models.Internal;
using IMG_API_Data_Obtainer.TransportModels;
using Newtonsoft.Json;

namespace IMG_API_Data_Obtainer.Models.Feed;

/// <summary>
/// Общее описание теннисной команды (предоставляется внешней системой).
/// </summary>
public sealed class FeedTwoTeamsCollectionDescription
{
    /// <summary>
    /// Внешний идентификатор команды А.
    /// </summary>
    public ExternalID<IntTeamDescription> TeamA { get; }

    /// <summary>
    /// Внешний идентификатор команды B.
    /// </summary>
    public ExternalID<IntTeamDescription> TeamB { get; }
}
