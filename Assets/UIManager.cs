using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject decisionPanel;
    public TextMeshProUGUI winnerText;
    public Button foldButton;
    public Button contestButton;

    private void Awake()
    {
        Instance = this;
        decisionPanel.SetActive(false);
    }

    public void ShowDecisionPanel(PlayerManager player)
    {
        decisionPanel.SetActive(true);
        // Dynamically assign button functions
        foldButton.onClick.RemoveAllListeners();
        contestButton.onClick.RemoveAllListeners();

        foldButton.onClick.AddListener(() => player.CmdFold());
        contestButton.onClick.AddListener(() => player.CmdContest());
    }

    public void HideDecisionPanel()
    {
        decisionPanel.SetActive(false);
    }

    public void ShowDecisionUI()
    {
        decisionPanel.SetActive(true);
    }

    public void ShowWinner(PlayerManager winner)
    {
        decisionPanel.SetActive(false);
        winnerText.text = winner ? $"Winner: Player {winner.netId} Number: {winner.assignedNumber}" : "No Winner";
    }
}
