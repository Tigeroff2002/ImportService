using IMG_API_Data_Obtainer.Models.Internal;
using IMG_API_Data_Obtainer.TransportModels;

namespace IMG_API_Data_Obtainer.Models.Feed;

/// <summary>
/// Описание соло-команды матча в теннис.
/// </summary>
public sealed class FeedTwoSoloTeamsCollectionDescription : FeedTwoTeamsCollectionDescription
{
    /// <summary>
    /// Соло игрок команды А.
    /// </summary>
    public ExternalID<IntPlayerDescription> TeamAPlayer { get; }

    /// <summary>
    /// Соло игрок команды B.
    /// </summary>
    public ExternalID<IntPlayerDescription> TeamBPlayer { get; }

    public FeedTwoSoloTeamsCollectionDescription(
        EntryType teamAEntryType,
        EntryType teamBEntryType,
        KnownTeamStatus teamAKnownStatus,
        KnownTeamStatus teamBKnownStatus,
        ExternalID<IntPlayerDescription> teamAPlayer,
        ExternalID<IntPlayerDescription> teamBPlayer)
        : base(teamAEntryType, teamBEntryType, teamAKnownStatus, teamBKnownStatus)
    {
        if (teamAPlayer == teamBPlayer)
        {
            throw new ArgumentException($"Team A player {teamAPlayer} is the same as team B player {teamBPlayer}");
        }

        TeamAPlayer = teamAPlayer;
        TeamBPlayer = teamBPlayer;
    }
}
