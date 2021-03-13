using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupWindow : MonoBehaviour
{
    private PlayerInteraction playerInteraction;
    private CharacterInventory playerInventory;
    public PlayerCartControl playerCartControl;
    public SFXManager sfx;
    //public GameObject cart;
    public List<ItemContainer> nearbyItems;
    public TextMeshProUGUI display;
    public int selection;
    public Image cursor;
    public Vector3 cursorPos;
    public TextMeshProUGUI control;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //cart = GameObject.FindGameObjectWithTag("Cart");
        display.text = "";
        selection = 0;
        cursorPos = cursor.rectTransform.anchoredPosition;
    }

    public void Init(PlayerInteraction playerInteraction, CharacterInventory playerInventory)
    {
        this.playerInteraction = playerInteraction;
        this.playerInventory = playerInventory;

        // Subscribe to the PlayerInteraction's events.
        playerInteraction.Player_OnInteractableNearby += AddItem;
        playerInteraction.Player_OnInteractableNearby_Removed += RemoveItem;
    }

    private void OnDisable()
    {
        if (playerInteraction != null)
        {
            playerInteraction.Player_OnInteractableNearby -= AddItem;
            playerInteraction.Player_OnInteractableNearby_Removed -= RemoveItem;
        }
    }


    #region --- Adding and Removing ItemContainers from nearbyItems ---
    private void AddItem(IItemInteractable i)
    {
        ItemContainer itemContainer = i as ItemContainer;
        if (itemContainer == null)
        {
            Debug.LogWarning("Cannot add interactable to pickupwindow list.");
            return;
        }

        if (!nearbyItems.Contains(itemContainer))
        {
            nearbyItems.Add(itemContainer);
            // If an NPC picks up an item while you are trying to grab it as well, 
            // make sure to update the display.
            itemContainer.OnQuantityReduced += UpdateDisplay;
            UpdateDisplay();
        }

    }

    private void RemoveItem(IItemInteractable i)
    {
        ItemContainer itemContainer = i as ItemContainer;
        if (itemContainer == null)
        {
            Debug.LogWarning("Cannot remove interactable to pickupwindow list.");
            return;
        }

        if (nearbyItems.Contains(itemContainer))
        {
            selection = 0;
            cursor.rectTransform.anchoredPosition = cursorPos;
            nearbyItems.Remove(itemContainer);
            itemContainer.OnQuantityReduced -= UpdateDisplay;
            UpdateDisplay();
        }

    }

    #endregion


    // Update is called once per frame
    void Update()
    {
        if (nearbyItems.Count == 0)
        {
            cursor.enabled = false;
            this.GetComponent<Image>().enabled = false;
            control.enabled = false;
        }
        else if (!cursor.enabled)
        {
            cursor.enabled = true;
            this.GetComponent<Image>().enabled = true;
            control.enabled = true;
        }

        if (Input.mouseScrollDelta.y < 0 && selection < nearbyItems.Count - 1)
        {
            selection++;
            cursor.rectTransform.anchoredPosition -= new Vector2(0, 80);
            Debug.Log(selection);
        }
        if (Input.mouseScrollDelta.y > 0 && selection != 0)
        {
            selection--;
            cursor.rectTransform.anchoredPosition += new Vector2(0, 80);
            Debug.Log(selection);
        }
        if (Input.GetKeyDown(KeyCode.F) && nearbyItems.Count != 0 && playerCartControl.didAttach)
        {
            //nearbyItems[selection].transform.position = cart.transform.position + new Vector3(0, 1, 0);
            //nearbyItems[selection].isInPlayerCart = true;
            nearbyItems[selection].Interact(playerInventory);
            if (sfx != null)
                sfx.source.PlayOneShot(sfx.pickup);

            // If we picked up the last nearby item, reset our selection and cursor
            // position.
            if (nearbyItems.Count == 0)
            {
                selection = 0;
                cursor.rectTransform.anchoredPosition = cursorPos;
                return;
            }
        }
    }

    public void UpdateDisplay()
    {
        display.text = "";

        for (int i = 0; i < nearbyItems.Count; ++i)
        {
            // If we are out of this item, remove it from the list.
            if (nearbyItems[i].GetData().Quantity <= 0)
            {
                RemoveItem(nearbyItems[i]);
                selection = 0;
                cursor.rectTransform.anchoredPosition = cursorPos;
                //i -= 1;
                continue;
            }

            // Append to the display text.
            display.text += nearbyItems[i].GetData().ToString() + "\n";
        }
    }
}
