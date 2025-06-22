using LoteriaMexicanaServer.Models;

namespace LoteriaMexicanaServer.Managers;

public class PlayerManager
{
    private readonly List<Player> _players = [];

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }

    public void RemovePlayer(Player player) => _players.Remove(player);

    public Player? GetPlayerById(string id) => _players.FirstOrDefault(x => x.Id == id);

    public int GetNumberOfPlayers() => _players.Count;
}
