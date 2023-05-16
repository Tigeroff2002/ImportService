using ADF.Library.Common.Logic.Serialization;
using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.TransportModels.Containers;
using IMG_API_Data_Obtainer.TransportModels;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace IMG_API_Data_Obtainer.Logic.Deserializers;

public sealed class DeletedMatchesDeserializer
    : IDeserializer<string, IReadOnlyList<Name<Match>>>
{
    public IReadOnlyList<Name<Match>> Deserialize(string source)
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

        var deletedMatchesList = new List<Name<Match>>();

        foreach (var fixture in fixturesList.CompetitionFixtures)
        {
            if (fixture.Status == ResultStatus.Cancelled)
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
                var matchName = new StringBuilder();

                var teamAPlayer1Name = $"{fixture.TeamA.Team.Player1.LastName} {fixture.TeamA.Team.Player1.FirstName}";
                teamAName.Append(teamAPlayer1Name);

                var teamBPlayer1Name = $"{fixture.TeamB.Team.Player1.LastName} {fixture.TeamB.Team.Player1.FirstName}";
                teamBName.Append(teamBPlayer1Name);

                var teamAPlayer2Name = new StringBuilder();
                var teamBPlayer2Name = new StringBuilder();

                if (teamType != TeamType.Solo)
                {
                    teamAPlayer2Name.Append($"{fixture.TeamA.Team.Player2!.LastName} {fixture.TeamA.Team.Player2.FirstName}");
                    teamAName.Append($" / {teamAPlayer2Name}");

                    teamBPlayer2Name.Append($"{fixture.TeamB.Team.Player2!.LastName} {fixture.TeamB.Team.Player2.FirstName}");
                    teamBName.Append($" / {teamBPlayer2Name}");
                }

                matchName.Append($"{teamAName} vs {teamBName} - {fixture.StartTime.Time}");

                deletedMatchesList.Add(new(matchName.ToString()));
            }
        }

        return deletedMatchesList;
    }
}
