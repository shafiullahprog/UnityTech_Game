using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    private List<PlayerManager> players = new List<PlayerManager>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterPlayer(PlayerManager player)
    {
        players.Add(player);
    }

    [Server]
    public void StartRound()
    {
        foreach (var player in players)
        {
            player.CmdAssignRandomNumber();
        }
        StartCoroutine(Countdown());
    }

    [Server]
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(10);
        CheckRoundState();
    }

    [Server]
    public void CheckRoundState()
    {
        var activePlayers = players.Where(p => !p.hasFolded).ToList();
        if (activePlayers.Count == 1)
        {
            DeclareWinner(activePlayers[0]);
        }
        else if (activePlayers.Count > 1)
        {
            var winner = activePlayers.OrderByDescending(p => p.assignedNumber).First();
            DeclareWinner(winner);
        }
    }

    [Server]
    private void DeclareWinner(PlayerManager winner)
    {
        RpcShowWinner(winner.playerId);
        StartCoroutine(ResetRound());
    }

    [ClientRpc]
    private void RpcShowWinner(int winnerId)
    {
        UIManager.Instance.ShowWinner(winnerId);
    }

    [Server]
    private IEnumerator ResetRound()
    {
        yield return new WaitForSeconds(3);
        StartRound();
    }
}
