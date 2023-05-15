using ADF.Library.Common.Logic.Serialization;

using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.TransportModels.Containers;
using Newtonsoft.Json;
using System.Diagnostics;

namespace IMG_API_Data_Obtainer.Logic.Deserializers;

public sealed class TournamentsDeserializer
    : IDeserializer<string, IReadOnlyDictionary<Id<Tournament>, Tournament>>
{
    public IReadOnlyDictionary<Id<Tournament>, Tournament> Deserialize(string source)
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

        return tournamentsList.Tournaments.ToDictionary(
            tournament => new Id<Tournament>(tournament.Identifier),
            tournament => 
                new Tournament(
                    new(tournament.Identifier),
                    new(tournament.CountryCode.GetHashCode()),
                    new(SPORT_TENNIS_ID),
                    new(tournament.TournamentName),
                    tournament.Year));
    }

    private const long SPORT_TENNIS_ID = 5L;
}
