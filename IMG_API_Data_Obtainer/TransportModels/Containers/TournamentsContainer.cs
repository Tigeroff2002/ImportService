namespace IMG_API_Data_Obtainer.TransportModels.Containers;

public sealed class TournamentsContainer
{
    public Tournament[] Tournaments { get; init; } = Array.Empty<Tournament>();
}
