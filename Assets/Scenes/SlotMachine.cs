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

        for (int i = 0; i < reels.Length; i++)
        {
            StartCoroutine(SpinReel(reels[i]));
        }

        yield return new WaitForSeconds(1f);

        foreach (var reel in reels)
        {
            StopReel(reel);
        }

        CheckResult();

        isSpinning = false;
    }

    IEnumerator SpinReel(Image reel)
    {
        float spinDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            reel.sprite = items[Random.Range(0, items.Length)];
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void StopReel(Image reel)
    {
        reel.sprite = items[Random.Range(0, items.Length)];
    }

    void CheckResult()
    {
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
