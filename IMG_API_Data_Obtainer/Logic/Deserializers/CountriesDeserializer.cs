using ADF.Library.Common.Logic.Serialization;

using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.TransportModels.Containers;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Cryptography;

namespace IMG_API_Data_Obtainer.Logic.Deserializers;

public sealed class CountriesDeserializer
    : IDeserializer<string, IReadOnlyList<Country>>
{
    public IReadOnlyList<Country> Deserialize(string source)
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
                country => new Country(
                    new(country.CountryCode)))
            .ToList();
    }
}
