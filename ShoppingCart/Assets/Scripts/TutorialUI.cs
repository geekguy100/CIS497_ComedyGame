using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    public GameObject resumeButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!paused)
        {
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("FinalScene");
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
    }

    public IEnumerator LongDelay()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0;
        PauseGame();
        resumeButton.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.OnGameWin += () => { StartCoroutine(LongDelay()); };
        EventManager.OnGameLost += () => { StartCoroutine(LongDelay()); };
    }

    private void OnDisable()
    {
        EventManager.OnGameWin -= () => { StartCoroutine(LongDelay()); };
        EventManager.OnGameLost -= () => { StartCoroutine(LongDelay()); };
    }
}
