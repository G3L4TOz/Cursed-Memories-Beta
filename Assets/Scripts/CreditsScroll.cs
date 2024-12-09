using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public RectTransform creditsPanel;
    public float scrollSpeed = 50f;

    public float runCooldownTime = 3f;

    private void Update()
    {
        runCooldownTime -= Time.deltaTime;
        if (runCooldownTime <= 0)
        {
            creditsPanel.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        }
    }
}