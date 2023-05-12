using Newtonsoft.Json;
using System.Collections;

namespace IMG_API_Data_Obtainer.TransportModels.Feed
{
    /// <summary>
    /// Описание дуо-команды теннисного матча.
    /// </summary>
    public sealed class FeedTwoDuoTeamsCollectionDescription : FeedTwoTeamsCollectionDescription
    {
        /// <summary>
        /// Тип команды в зависимости от пола участников.
        /// </summary>
        [JsonProperty("gender_mix_type", Required = Required.Always)]
        public GenderMixType GenderMixType { get; init; }

        /// <summary>
        /// Дуо-игроки команды A.
        /// </summary>
        [JsonProperty("teamA_players", Required = Required.Always)]
        public List<ExternalID> TeamAPlayers { get; init; } = new List<ExternalID>(TEAM_PLAYERS_COUNT);

        /// <summary>
        /// Дуо-игроки команды B.
        /// </summary>
        [JsonProperty("teamB_players", Required = Required.Always)]
        public List<ExternalID> TeamBPlayers { get; init; } = new List<ExternalID>(TEAM_PLAYERS_COUNT);

        private const int TEAM_PLAYERS_COUNT = 2;
    }
}
