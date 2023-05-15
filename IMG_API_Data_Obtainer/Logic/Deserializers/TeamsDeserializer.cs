using ADF.Library.Common.Logic.Serialization;

using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.TransportModels.Containers;
using Newtonsoft.Json;
using System.Diagnostics;

using IMG_API_Data_Obtainer.TransportModels;

using Team = IMG_API_Data_Obtainer.EntitiesModels.Team;

namespace IMG_API_Data_Obtainer.Logic.Deserializers;

public sealed class TeamsDeserializer
    : IDeserializer<string, IReadOnlyDictionary<Name<Team>, Team>>
{
    public IReadOnlyDictionary<Name<Team>, Team> Deserialize(string source)
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

        var teamsList = new Dictionary<Name<Team>, Team>();

        foreach(var fixture in fixturesList.CompetitionFixtures) 
        {
            var teamAPlayer1Name = $"{fixture.TeamA.Team.Player1.LastName} {fixture.TeamA.Team.Player1.LastName}";
            var teamBPlayer1Name = $"{fixture.TeamB.Team.Player1.LastName} {fixture.TeamB.Team.Player1.LastName}";

            if (fixture.MatchType == RawMatchType.LS
                || fixture.MatchType == RawMatchType.LS)
            {
                 teamsList.Add(
                    new(teamAPlayer1Name),
                    new(
                        new(teamAPlayer1Name),
                        RangeType.Solo,
                        new(
                            new(
                                new(
                                    fixture.TeamA.Team.Player1.Id),
                                    new(teamAPlayer1Name))),
                        fixture.TeamA.KnownTeamStatus));

                teamsList.Add(
                    new(teamBPlayer1Name),
                    new(
                        new(teamBPlayer1Name),
                        RangeType.Solo,
                        new(
                            new(
                                new(
                                    fixture.TeamA.Team.Player1.Id),
                                    new(teamBPlayer1Name))),
                        fixture.TeamA.KnownTeamStatus));
            }
            else
            {
                var teamAPlayer2Name = $"{fixture.TeamA.Team.Player2!.LastName} {fixture.TeamA.Team.Player2.LastName}";
                var teamBPlayer2Name = $"{fixture.TeamB.Team.Player2!.LastName} {fixture.TeamB.Team.Player2.LastName}";

                teamsList.Add(
                    new($"{teamAPlayer1Name} / {teamAPlayer2Name}"),
                    new(
                        new($"{teamAPlayer1Name} / {teamAPlayer2Name}"),
                        RangeType.Duo,
                        new(
                            new(
                                new(
                                    fixture.TeamA.Team.Player1.Id),
                                    new(teamAPlayer1Name)),
                            new(
                                new(
                                    fixture.TeamA.Team.Player2!.Id),
                                    new(teamAPlayer2Name))),
                        fixture.TeamA.KnownTeamStatus));

                teamsList.Add(
                    new($"{teamBPlayer1Name} / {teamBPlayer2Name}"),
                    new(
                        new($"{teamBPlayer1Name} / {teamBPlayer2Name}"),
                        RangeType.Duo,
                        new(
                            new(
                                new(
                                    fixture.TeamB.Team.Player1.Id),
                                    new(teamBPlayer1Name)),
                            new(
                                new(
                                    fixture.TeamB.Team.Player2!.Id),
                                    new(teamBPlayer2Name))),
                        fixture.TeamB.KnownTeamStatus));
            }
        }

        return teamsList;
    }
}
