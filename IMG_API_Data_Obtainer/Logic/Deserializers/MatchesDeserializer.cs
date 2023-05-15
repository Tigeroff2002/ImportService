using ADF.Library.Common.Logic.Serialization;

using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.TransportModels.Containers;
using IMG_API_Data_Obtainer.TransportModels;
using Newtonsoft.Json;
using System.Diagnostics;

namespace IMG_API_Data_Obtainer.Logic.Deserializers;

public sealed class MatchesDeserializer
    : IDeserializer<string, IReadOnlyDictionary<Name<Match>, Match>>
{
    public IReadOnlyDictionary<Name<Match>, Match> Deserialize(string source)
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



        return fixturesList.CompetitionFixtures.ToDictionary(
            match => new Name<Match>(match.EventId),
            match => new Match(
                new(match.EventId),
                match.StartTime.Time,
                new(match.CompetitionId),
                new(),
                new(),
                match.Status == ResultStatus.Cancelled,
                match.MatchType == RawMatchType.MS || match.MatchType == RawMatchType.LS
                    || match.MatchType == RawMatchType.QMS || match.MatchType == RawMatchType.QLS
                        ? TeamType.Solo
                        : match.MatchType == RawMatchType.MD || match.MatchType == RawMatchType.LD
                            || match.MatchType == RawMatchType.QMD || match.MatchType == RawMatchType.QLD
                                ? TeamType.DuoSimilar
                                : TeamType.DuoMixed));
    }
}
