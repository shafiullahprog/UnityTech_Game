using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    [SyncVar] public int playerId;
    [SyncVar] public int assignedNumber;
    [SyncVar] public bool hasFolded;
    [SyncVar] public int chips = 100;
    
    private void Start()
    {
        if (isLocalPlayer)
        {
            CmdAssignRandomNumber();
        }
    }

    [Command]
    public void CmdAssignRandomNumber()
    {
        assignedNumber = Random.Range(1, 101);
    }

    [Command]
    public void CmdFold()
    {
        hasFolded = true;
        GameManager.Instance.CheckRoundState();
    }

    [Command]
    public void CmdContest()
    {
        hasFolded = false;
        GameManager.Instance.CheckRoundState();
    }
}
