using ADF.Library.Common.Logic.Serialization;

using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.TransportModels.Containers;
using Newtonsoft.Json;
using System.Diagnostics;

namespace IMG_API_Data_Obtainer.Logic.Deserializers;

public sealed class TournamentsDeserializer
    : IDeserializer<string, IReadOnlyList<Tournament>>
{
    public IReadOnlyList<Tournament> Deserialize(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            throw new ArgumentException(
                "Source cannot be empty, or null, or has only white-space characters.",
                nameof(source));
        }

        using var reader = new JsonTextReader(new StringReader(source));

        var serializer = JsonSerializer.CreateDefault();

        var tournamentsList = serializer.Deserialize<TournamentsContainer>(reader);

        Debug.Assert(tournamentsList != null);

        return tournamentsList.Tournaments
            .Select(
                tournament => 
                    new Tournament(
                        new($"{tournament.TournamentName} {tournament.Year}"),
                        new(tournament.TournamentName),
                        new(SPORT_TENNIS_ID),
                        new(tournament.CountryCode),
                        tournament.Year,
                        tournament.Competitions
                            .Select(competition => competition.CompetitionID)
                            .ToList()))
            .ToList();
    }

    private const long SPORT_TENNIS_ID = 5L;
}
