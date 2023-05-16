using ADF.Library.Common.Logic.Serialization;

using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.TransportModels.Containers;

using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

using IMG_API_Data_Obtainer.TransportModels;

using Team = IMG_API_Data_Obtainer.EntitiesModels.Team;

namespace IMG_API_Data_Obtainer.Logic.Deserializers;

public sealed class TeamsDeserializer
    : IDeserializer<string, IReadOnlyList<Team>>
{
    public IReadOnlyList<Team> Deserialize(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            throw new ArgumentException(
                "Source cannot be empty, or null, or has only white-space characters.",
                nameof(source));
        }

        using var reader = new JsonTextReader(new StringReader(source));

        var serializer = JsonSerializer.CreateDefault();

        var fixturesList = serializer.Deserialize<CompetitionsFixturesContainer>(reader);

        Debug.Assert(fixturesList != null);

        var teamsList = new List<Team>();

        foreach (var fixture in fixturesList.CompetitionFixtures)
        {
            var teamType = fixture.MatchType == RawMatchType.MS || fixture.MatchType == RawMatchType.LS
                || fixture.MatchType == RawMatchType.QMS || fixture.MatchType == RawMatchType.LS
                    ? TeamType.Solo
                    : fixture.MatchType == RawMatchType.MD || fixture.MatchType == RawMatchType.LD
                        || fixture.MatchType == RawMatchType.QMD || fixture.MatchType == RawMatchType.QLD
                            ? TeamType.DuoSimilar
                            : TeamType.DuoMixed;

            var teamAName = new StringBuilder();
            var teamAId = new StringBuilder();

            var teamBName = new StringBuilder();
            var teamBId = new StringBuilder();

            var teamAPlayer1Name = $"{fixture.TeamA.Team.Player1.LastName} {fixture.TeamA.Team.Player1.LastName}";
            teamAName.Append(teamAPlayer1Name);
            teamAId.Append(fixture.TeamA.Team.Player1.Id);

            var teamBPlayer1Name = $"{fixture.TeamB.Team.Player1.LastName} {fixture.TeamB.Team.Player1.LastName}";
            teamBName.Append(teamBPlayer1Name);
            teamBId.Append(fixture.TeamB.Team.Player1.Id);

            var teamAPlayer2Name = new StringBuilder();
            var teamBPlayer2Name = new StringBuilder();

            if (teamType != TeamType.Solo)
            {
                teamAPlayer2Name.Append($"{fixture.TeamA.Team.Player2!.LastName} {fixture.TeamA.Team.Player2.FirstName}");
                teamAName.Append($" / {teamAPlayer2Name}");
                teamAId.Append(fixture.TeamA.Team.Player2.Id);

                teamBPlayer2Name.Append($"{fixture.TeamB.Team.Player2!.LastName} {fixture.TeamB.Team.Player2.FirstName}");
                teamBName.Append($" / {teamBPlayer2Name}");
                teamBId.Append(fixture.TeamB.Team.Player2.Id);
            }

            teamsList.Add(
                new(
                    new(teamAId.ToString()),
                    new(teamAName.ToString()),
                    teamType,
                    fixture.TeamA.KnownTeamStatus));

            teamsList.Add(
                new(
                    new(teamBId.ToString()),
                    new(teamBName.ToString()),
                    teamType,
                    fixture.TeamB.KnownTeamStatus));
        }

        return teamsList;
    }
}
