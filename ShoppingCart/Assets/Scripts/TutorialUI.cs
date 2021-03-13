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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
    }

    public IEnumerator LongDelay()
    {
        yield return new WaitForSeconds(5f);
        PauseGame();
    }

    public void Tutorial()
    {
        switch (action)
        {
            case 0:
                text.text = "Use the mouse to look around.\n";
                StartCoroutine(Delay());
                if (Input.anyKey)
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 1:
                text.text = "Use WASD to move.\n";
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 2:
                text.text = "Press E to attach/detach from your cart.\n";
                if (pcc.didAttach || Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 3:
                text.text = "Walking near a blue cube will show what items are inside.\nPress F while attached to your cart to pick up.\nScroll with the mouse to select which item to pick up.\n";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 4:
                text.text = "On the bottom left is your Dash Meter.\n When it is full, press left shift to Dash.\nDashing into other shoppers makes them drop their items.\n";
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 5: 
                text.text = "Get all of the items on your list then head to checkout.\nGo quickly, supplies are limited.\nIf another shopper checks out with an item you need, you lose.\nPress F now to dismiss.\n";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 6:
                text.text = "You can press P to pause at any time.\n";
                if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(Delay());
                    action++;
                }
                break;
            case 7:
                text.enabled = false;
                doTutorial = false;
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        EventManager.OnGameWin += () => { StartCoroutine(LongDelay()); };
    }
}
