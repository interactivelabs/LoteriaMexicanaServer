namespace LoteriaMexicanaTypes.Hubs;

public interface IGameHub
{
    Task JoinRoom();
    Task LeaveRoom();
    Task CallLoteria(Dictionary<int, bool> checkedCards);
}
