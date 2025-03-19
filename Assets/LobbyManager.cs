using Mirror;

public class LobbyManager : NetworkRoomManager
{
    public override void OnRoomServerPlayersReady()
    {
        base.OnRoomServerPlayersReady();
        //GameManager.Instance.StartRound();
    }
}
