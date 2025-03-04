using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text roundInfoText;
    public Button foldButton, contestButton;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowWinner(int winnerId)
    {
        roundInfoText.text = $"Player {winnerId} Wins!";
    }

    public void SetUpButtons(PlayerManager player)
    {
        foldButton.onClick.AddListener(() => player.CmdFold());
        contestButton.onClick.AddListener(() => player.CmdContest());
    }

    private void Update()
    {
        
    }
}



