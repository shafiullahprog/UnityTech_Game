using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotMachine : MonoBehaviour
{
    public Image[] reels;
    public Sprite[] items;
    public Button spinButton;
    public TextMeshProUGUI resultText;

    private bool isSpinning = false;

    void Start()
    {
        spinButton.onClick.AddListener(SpinReels);
    }

    void SpinReels()
    {
        if (!isSpinning)
        {
            StartCoroutine(SpinReelsCoroutine());
        }
    }

    IEnumerator SpinReelsCoroutine()
    {
        isSpinning = true;
        resultText.text = "Spinning...";

        // Spin each reel
        for (int i = 0; i < reels.Length; i++)
        {
            StartCoroutine(SpinReel(reels[i]));
        }

        // Wait for 2 seconds to simulate spinning
        yield return new WaitForSeconds(2f);

        // Stop each reel and assign a random item
        foreach (var reel in reels)
        {
            StopReel(reel);
        }

        // Check for win/lose
        CheckResult();

        isSpinning = false;
    }

    IEnumerator SpinReel(Image reel)
    {
        float spinDuration = 2f; // Duration of the spin
        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            // Randomly change the sprite during the spin
            reel.sprite = items[Random.Range(0, items.Length)];
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void StopReel(Image reel)
    {
        // Assign a random item to the reel when it stops
        reel.sprite = items[Random.Range(0, items.Length)];
    }

    void CheckResult()
    {
        // Check if all reels have the same sprite
        if (reels[0].sprite == reels[1].sprite && reels[1].sprite == reels[2].sprite)
        {
            resultText.text = "You Win!";
        }
        else
        {
            resultText.text = "You Lose!";
        }
    }
}
