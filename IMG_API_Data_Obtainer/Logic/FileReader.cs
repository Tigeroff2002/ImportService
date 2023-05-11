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
        { "matches", "DataSamples/fixtures.json" },
        { "sports", "nothing.json" },
        { "players", "DataSamples/players.json" },
        { "competitions", "DataSamples/competitions.json" },
        { "tournaments", "DataSamples/tournaments.json" },
        { "teams", "DataSamples/teams.json" },
    };

    private static readonly string _emptyResult = new StringBuilder().ToString();
}
