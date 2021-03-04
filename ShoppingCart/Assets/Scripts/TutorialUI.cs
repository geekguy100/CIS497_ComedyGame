using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    public bool paused;
    public bool doTutorial;
    public int action;
    public TextMeshProUGUI text;
    public PlayerCartControl pcc;
    public PlayerInteraction pi;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        doTutorial = true;
        action = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (doTutorial)
        {
            Tutorial();
        }
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
        }
        else
        {
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
    }

    public void Tutorial()
    {
        switch (action)
        {
            case 0:
                text.text = "Use the mouse to look around.\n";
                if (Input.anyKey)
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 1:
                text.text = "Use WASD to move.\n";
                if (Input.GetKeyDown(KeyCode.W))
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 2:
                text.text = "Press E to attach/detach from your cart.\n";
                if (pcc.didAttach)
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 3:
                text.text = "The window on the right shows nearby items.\nWalk near something and press F to pick it up.\nScroll with the mouse to select which item to pick up.\n";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 4: 
                text.text = "Get all of the items on your list then head to checkout and press F. Press F now to dismiss.\n";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 5:
                text.enabled = false;
                doTutorial = false;
                break;
            default:
                break;
        }
    }
}
