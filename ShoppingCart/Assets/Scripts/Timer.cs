/*****************************************************************************
// File Name :         Timer.cs
// Author :            Kyle Grenier
// Creation Date :     02/28/2021
//
// Brief Description : Basic timer that counts up to keep track of in-game time.
*****************************************************************************/
using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private float time = 0f;
    [SerializeField] private TextMeshProUGUI timerText;

    private Color green = new Color32(41, 204, 84, 255);
    private Color red = new Color32(204, 55, 41, 255);
    private Color blue = new Color32(22, 166, 255, 255);
    private Color black = new Color32(255, 255, 255, 255);

    private bool gameWon = false;
    private bool gameOver = false;
    [SerializeField] private Animator anim;

    public float bestTime = 999f;

    private void OnEnable()
    {
        EventManager.OnGameWin += () => { gameWon = true; };
        EventManager.OnGameWin += () => { gameOver = true; };
        EventManager.OnGameLost += () => { gameOver = true; };
        EventManager.OnGameWin += () => { anim.SetTrigger("Expand"); };
        EventManager.OnGameLost += () => { anim.SetTrigger("Expand"); };
        if (PlayerPrefs.GetFloat("PB") == 0)
        {
            PlayerPrefs.SetFloat("PB", 999);
        }
        else
        {
            bestTime = PlayerPrefs.GetFloat("PB");
        }
        
        //StartCoroutine(ColorSwap());
    }

    private void Update()
    {
        if (!gameOver)
        {
            time += Time.deltaTime;
            timerText.text = "Time: " + Math.Round(time, 2) + "\nPersonal Best: " + Math.Round(bestTime, 2);
            if (time < bestTime)
            {
                timerText.color = green;
            }
            else
            {
                timerText.color = red;
            }
        }
        else if (gameWon)
        {
            if (time < bestTime)
            {
                PlayerPrefs.SetFloat("PB", time);
            }
            timerText.color = blue;
        }
        else
        {
            timerText.color = black;
        }
    }

    public IEnumerator ColorSwap()
    {
        timerText.color = green;
        yield return new WaitForSeconds(120);

        timerText.color = red;
        yield return new WaitForSeconds(UnityEngine.Random.Range(10, 30));
        StartCoroutine(ColorSwap());
    }
}
