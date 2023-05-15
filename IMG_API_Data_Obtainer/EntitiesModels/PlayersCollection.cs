namespace IMG_API_Data_Obtainer.EntitiesModels;

public sealed class PlayersCollection
{
    public Player Player1 { get; set; }

    public Player? Player2 { get; init; } = null!;

    public PlayersCollection(Player player1)
    {
        Player1 = player1;
    }

    public PlayersCollection(Player player1, Player player2) 
    { 
        Player1 = player1;
        Player2 = player2;
    }
}
