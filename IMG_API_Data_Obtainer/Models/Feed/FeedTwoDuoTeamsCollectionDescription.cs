namespace IMG_API_Data_Obtainer.Models.Feed;

using IMG_API_Data_Obtainer.Models.Internal;
using IMG_API_Data_Obtainer.TransportModels;

using GenderMixType = IMG_API_Data_Obtainer.TransportModels.GenderMixType;

public sealed class FeedTwoDuoTeamsCollectionDescription : FeedTwoTeamsCollectionDescription
{
    /// <summary>
    /// Тип команды в зависимости от пола участников.
    /// </summary>
    public GenderMixType GenderMixType { get; }

    /// <summary>
    /// Дуо-игроки команды A.
    /// </summary>
    public List<ExternalID<IntPlayerDescription>> TeamAPlayers { get; } 

    /// <summary>
    /// Дуо-игроки команды B.
    /// </summary>
    public List<ExternalID<IntPlayerDescription>> TeamBPlayers { get; }
    
    public FeedTwoDuoTeamsCollectionDescription(
        EntryType teamAEntryType,
        EntryType teamBEntryType,
        KnownTeamStatus teamAKnownStatus,
        KnownTeamStatus teamBKnownStatus,
        GenderMixType genderMixType,
        List<ExternalID<IntPlayerDescription>> teamAPlayers,
        List<ExternalID<IntPlayerDescription>> teamBPlayers)
        : base(teamAEntryType, teamBEntryType, teamAKnownStatus, teamBKnownStatus)
    {
        if (teamAPlayers == null)
        {
            throw new ArgumentNullException(nameof(teamAPlayers));
        }

        if (teamBPlayers == null)
        {
            throw new ArgumentNullException(nameof(teamBPlayers));
        }

        if (teamAPlayers.Count != 2)
        {
            throw new ArgumentException($"Collection {nameof(teamAPlayers)} does not contain 2 required elements");
        }

        if (teamBPlayers.Count != 2)
        {
            throw new ArgumentException($"Collection {nameof(teamBPlayers)} does not contain 2 required elements");
        }

        foreach(var entryA in teamAPlayers)
        {
            foreach(var entryB in teamBPlayers)
            { 
                if (entryA == entryB)
                {
                    throw new ArgumentException($"One of team A player {entryA} is the same as one of team B player {entryB}");
                }
            }
        }

        TeamAPlayers = teamAPlayers;
        TeamBPlayers = teamBPlayers;
        GenderMixType = genderMixType;
    }
}
