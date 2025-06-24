namespace LoteriaMexicanaTypes.Hubs;

public interface IGameHub
{
    Task JoinRoom();
    Task LeaveRoom();
}