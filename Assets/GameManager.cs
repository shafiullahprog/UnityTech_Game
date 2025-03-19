using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    [SerializeField] private List<PlayerManager> players = new();
    private Dictionary<PlayerManager, int> bets = new();

    private void Awake() => Instance = this;

    public void RegisterPlayer(PlayerManager player)
    {
        if (!players.Contains(player)) players.Add(player);
    }

    [Server]
    public void StartRound()
    {
        Debug.Log("Rouch Start");
        bets.Clear();
        foreach (var player in players)
        {
            player.hasFolded = false;
            player.assignedNumber = Random.Range(1, 101);
        }
        RpcStartDecisionPhase();
        Invoke(nameof(EndRound), 10f); // 10 seconds timer
    }

    [ClientRpc]
    private void RpcStartDecisionPhase()
    {
        UIManager.Instance.ShowDecisionUI(); // Tells all clients to show buttons
    }

    [Server]
    public void PlayerFold(PlayerManager player) => player.hasFolded = true;

    [Server]
    public void PlayerContest(PlayerManager player) => player.isReady = true;

    [Server]
    public void RegisterBet(PlayerManager player, int amount)
    {
        if (!bets.ContainsKey(player)) bets[player] = 0;
        bets[player] += amount;
    }

    [Server]
    private void EndRound()
    {
        var remaining = players.FindAll(p => !p.hasFolded);
        PlayerManager winner = null;

        if (remaining.Count == 1)
        {
            winner = remaining[0];
        }
        else if (remaining.Count > 1)
        {
            int max = int.MinValue;
            foreach (var p in remaining)
            {
                if (p.assignedNumber > max)
                {
                    max = p.assignedNumber;
                    winner = p;
                }
            }
        }

        if (winner != null)
        {
            if (bets.ContainsKey(winner))
                winner.chips += bets[winner] * 2;
        }

        RpcDisplayWinner(winner?.netIdentity);
        Invoke(nameof(StartRound), 5f); // Delay before new round
    }

    [ClientRpc]
    private void RpcDisplayWinner(NetworkIdentity winner)
    {
        UIManager.Instance.ShowWinner(winner != null ? winner.GetComponent<PlayerManager>() : null);
    }
}
