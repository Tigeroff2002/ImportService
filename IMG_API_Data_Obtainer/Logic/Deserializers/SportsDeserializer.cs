using ADF.Library.Common.Logic.Serialization;
using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.TransportModels.Containers;
using Newtonsoft.Json;
using System.Diagnostics;

namespace IMG_API_Data_Obtainer.Logic.Deserializers;

public sealed class SportsDeserializer
    : IDeserializer<string, IReadOnlyDictionary<Id<Sport>, Sport>>
{
    public IReadOnlyDictionary<Id<Sport>, Sport> Deserialize(string source)
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
            sport => new Id<Sport>(SPORT_TENNIS_ID),
            sport => new Sport(
                new(SPORT_TENNIS_ID),
                new(sport.Sport)));
    }

    private const long SPORT_TENNIS_ID = 5L;
}
