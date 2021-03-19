/* Frank Calabrese
 * Tutorial.cs
 * simple tutorial manager
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] string[] tutorialMessages;
    [SerializeField] GameObject continueText;
    PickupWindow pickUpWindow;
    PlayerCartControl cartControl;
    NPC npc;

    private int index = 0;
    void Start()
    {
        continueText.SetActive(true);

        pickUpWindow = FindObjectOfType<PickupWindow>();
        cartControl = FindObjectOfType<PlayerCartControl>();
        npc = FindObjectOfType<NPC>();

        tutorialText.text = tutorialMessages[index];
    }

    
    void Update()
    {
        switch (index)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    index++;
                    tutorialText.text = tutorialMessages[index];
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    index++;
                    tutorialText.text = tutorialMessages[index];
                }
                break;
            case 2:
                continueText.SetActive(false);

                if (cartControl.didAttach)
                {
                    continueText.SetActive(true);
                    if(Input.GetKeyDown(KeyCode.Return))
                    {
                        index++;
                        tutorialText.text = tutorialMessages[index];
                    }
                }

                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    index++;
                    tutorialText.text = tutorialMessages[index];
                }
                break;
            case 4:
                continueText.SetActive(false);

                if (pickUpWindow.pickedUpItem)
                {
                    continueText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        index++;
                        tutorialText.text = tutorialMessages[index];
                    }
                }
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    index++;
                    tutorialText.text = tutorialMessages[index];
                }
                break;
            case 6:
                continueText.SetActive(false);

                if (npc.lostCart)
                {
                    continueText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        index++;
                        tutorialText.text = tutorialMessages[index];
                    }
                }
                break;
            case 7:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    index++;
                    tutorialText.text = tutorialMessages[index];
                }
                break;
            case 8:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    index++;
                    tutorialText.text = tutorialMessages[index];
                }
                break;
            case 9:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    tutorialText.text = tutorialMessages[index];
                }
                break;
        }


    }

    
}
