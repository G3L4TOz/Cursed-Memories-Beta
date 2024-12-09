using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public GameObject gameplayUI;
    public bool isGameplay;
    void Start()
    {
        gameplayUI.SetActive(true);
        isGameplay = true;
    }

    void Update()
    {
        if (isGameplay && Input.GetKeyDown(KeyCode.C))
        {
            gameplayUI.SetActive(false);
            isGameplay = false;
        }
        else if (!isGameplay && Input.GetKeyDown(KeyCode.C))
        {
            gameplayUI.SetActive(true);
            isGameplay = true;
        }
    }
}