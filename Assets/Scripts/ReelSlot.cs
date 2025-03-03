using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelSlot : MonoBehaviour
{
    [SerializeField] Sprite[] randSprites;
    [SerializeField] Image image;
    [SerializeField] float timer =0;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            image.sprite = randSprites[Random.Range(0, randSprites.Length)];
        
            timer = 0;
        }
    }
}
