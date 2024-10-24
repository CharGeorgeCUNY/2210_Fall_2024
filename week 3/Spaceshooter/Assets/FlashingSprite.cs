using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingSprite : MonoBehaviour
{
    public float FlashTime = .5f;
    public Color EndColor;
    public Color StartColor;
    // Start is called before the first frame update
    void Start()
    {
        StartColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFlash()
    {
        StartCoroutine(FlashingCoRoutine(EndColor));
    }

    IEnumerator FlashingCoRoutine(Color ColorAlternate)
    {
        while (true)
        {
            yield return new WaitForSeconds(FlashTime);
            GetComponent<SpriteRenderer>().color = ColorAlternate;
            yield return new WaitForSeconds(FlashTime);
            GetComponent<SpriteRenderer>().color = StartColor;
        }

    }
}
