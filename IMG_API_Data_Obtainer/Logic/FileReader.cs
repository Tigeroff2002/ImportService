using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Logic.Abstractions;
using System.Text;

namespace IMG_API_Data_Obtainer.Logic;

public static class FileReader
{
    public static async Task<string> ReadMatchesData(string direction, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(direction))
        {
            throw new ArgumentException(nameof(direction)); 
        }

        if (_pathsToData.TryGetValue(direction, out var path)){

            return await File.ReadAllTextAsync(path);
        }

        return _emptyResult;
    }

    private static Dictionary<string, string> _pathsToData = new Dictionary<string, string>()
    {
        { nameof(Sport), "DataSamples/tournaments.json" },
        { nameof(Country), "DataSamples/tournaments.json" },
        { nameof(Tournament), "DataSamples/tournaments.json" },
        { nameof(Competition), "DataSamples/tournaments.json" },
        { nameof(Team), "DataSamples/fixtures.json" },
        { nameof(Match), "DataSamples/fixtures.json" },
    };

    private static readonly string _emptyResult = new StringBuilder().ToString();
}
