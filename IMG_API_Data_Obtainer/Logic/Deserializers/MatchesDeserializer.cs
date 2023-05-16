using ADF.Library.Common.Logic.Serialization;

using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.TransportModels.Containers;
using IMG_API_Data_Obtainer.TransportModels;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

using Match = IMG_API_Data_Obtainer.EntitiesModels.Match;

namespace IMG_API_Data_Obtainer.Logic.Deserializers;

public sealed class MatchesDeserializer
    : IDeserializer<string, IReadOnlyList<Match>>
{
    public IReadOnlyList<Match> Deserialize(string source)
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

        var matchesList = new List<Match>();

        foreach (var fixture in fixturesList.CompetitionFixtures)
        {
            var teamType = fixture.MatchType == RawMatchType.MS || fixture.MatchType == RawMatchType.LS
                || fixture.MatchType == RawMatchType.QMS || fixture.MatchType == RawMatchType.QLS
                    ? TeamType.Solo
                    : fixture.MatchType == RawMatchType.MD || fixture.MatchType == RawMatchType.LD
                        || fixture.MatchType == RawMatchType.QMD || fixture.MatchType == RawMatchType.QLD
                            ? TeamType.DuoSimilar
                            : TeamType.DuoMixed;

            var teamAName = new StringBuilder();
            var teamBName = new StringBuilder();

            var teamAId = new StringBuilder();
            var teamBId = new StringBuilder();

            var matchName = new StringBuilder();

            var teamAPlayer1Name = $"{fixture.TeamA.Team.Player1.LastName} {fixture.TeamA.Team.Player1.FirstName}";
            teamAName.Append(teamAPlayer1Name);
            teamAId.Append(fixture.TeamA.Team.Player1.Id);

            var teamBPlayer1Name = $"{fixture.TeamB.Team.Player1.LastName} {fixture.TeamB.Team.Player1.FirstName}";
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

            matchName.Append($"{teamAName} vs {teamBName} - {fixture.StartTime.Time}");


            matchesList.Add(
                new(
                    new(fixture.EventId),
                    new(matchName.ToString()),
                    fixture.StartTime.Time,
                    new(fixture.CompetitionId),
                    new(teamAId.ToString()),
                    new(teamBId.ToString()),
                    fixture.Status == ResultStatus.Cancelled,
                    fixture.MatchType,
                    teamType));
        }

        return matchesList;
    }
}
