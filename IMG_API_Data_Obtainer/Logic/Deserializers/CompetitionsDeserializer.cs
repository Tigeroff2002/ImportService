using ADF.Library.Common.Logic.Serialization;

using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.TransportModels.Containers;
using Newtonsoft.Json;
using System.Diagnostics;

namespace IMG_API_Data_Obtainer.Logic.Deserializers;

public sealed class CompetitionsDeserializer
    : IDeserializer<string, IReadOnlyDictionary<Name<Competition>, Competition>>
{
    public IReadOnlyDictionary<Name<Competition>, Competition> Deserialize(string source)
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

        var competitionDictionary = new Dictionary<Name<Competition>, Competition>();

        foreach(var tournament in tournamentsList.Tournaments)
        {
            foreach(var competition in tournament.Competitions)
            {
                competitionDictionary.Add(
                    new(competition.ExternalID),
                    new(
                        new(competition.ExternalID),
                        new(competition.CompetitionID)));
            }
        }

        return competitionDictionary;
    }
}
