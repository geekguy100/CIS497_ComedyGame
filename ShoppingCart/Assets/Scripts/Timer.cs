/*****************************************************************************
// File Name :         Timer.cs
// Author :            Kyle Grenier
// Creation Date :     02/28/2021
//
// Brief Description : Basic timer that counts up to keep track of in-game time.
*****************************************************************************/
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private float time = 0f;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool gameWon = false;

    private void OnEnable()
    {
        EventManager.OnGameWin += () => { gameWon = true; };
    }

    private void Update()
    {
        if (!gameWon)
        {
            time += Time.deltaTime;
            timerText.text = "Time: " + Math.Round(time, 2);
        }
    }
}
