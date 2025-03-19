using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    [SyncVar] public int assignedNumber;
    [SyncVar] public bool hasFolded;
    [SyncVar] public int chips = 100;
    [SyncVar] public bool isReady;

    private GameManager gameManager;

    public override void OnStartServer()
    {
        gameManager = GameManager.Instance;
        gameManager.RegisterPlayer(this);
    }

    [Command]
    public void CmdFold() => gameManager.PlayerFold(this);

    [Command]
    public void CmdContest() => gameManager.PlayerContest(this);

    [Command]
    public void CmdPlaceBet(int amount)
    {
        if (chips >= amount)
        {
            chips -= amount;
            gameManager.RegisterBet(this, amount);
        }
    }
}
