/*****************************************************************************
// File Name :         UIManager.cs
// Author :            Kyle Grenier
// Creation Date :     03/06/2021
//
// Brief Description : Manages updating game state UI.
*****************************************************************************/
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;

    private void OnEnable()
    {
        EventManager.OnGameWin += OnGameWin;
        EventManager.OnGameLost += OnGameLose;
    }

    private void OnDisable()
    {
        EventManager.OnGameWin -= OnGameWin;
        EventManager.OnGameLost -= OnGameLose;
    }

    private void OnGameWin()
    {
        winText.SetActive(true);
    }

    void OnGameLose()
    {
        loseText.SetActive(true);
    }
}
