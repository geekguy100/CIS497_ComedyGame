/*****************************************************************************
// File Name :         ItemCartDisplay.cs
// Author :            Kyle Grenier
// Creation Date :     03/17/2021
//
// Brief Description : Displays a character's inventory in the shopping cart.
*****************************************************************************/
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class ItemCartDisplay : MonoBehaviour
{
    [Tooltip("The inventory to display the items of.")]
    [SerializeField] private CharacterInventory inventory;

    [Tooltip("The transform that holds the location to spawn the item in.")]
    [SerializeField] private Transform spawnPos;

    // Keep track of what items are in the cart so we can delete them.
    private List<GameObject> itemsInCart;

    Grid grid;
    GridTile tile;

    private void Awake()
    {
        if (inventory == null)
        {
            Debug.LogWarning(gameObject.name + ": No inventory to display items of. Removing component...");
            Destroy(this);
        }

        grid = new Grid(3, 3, spawnPos.localPosition);
        tile = new GridTile(grid);

        itemsInCart = new List<GameObject>();
    }

    private void OnEnable()
    {
        inventory.OnItemAdded += AddItem;
        inventory.OnCartEmptied += RemoveItems;
    }

    private void OnDisable()
    {
        inventory.OnItemAdded -= AddItem;
        inventory.OnCartEmptied -= RemoveItems;
    }

    private void AddItem(System.Type itemType)
    {
        // Make sure this is a new item we're adding to the cart.
        if (inventory.GetQuantity(itemType) == 1)
        {
            GameObject prefab = ItemFactory.Spawn(itemType.ToString());
            GameObject visual = Instantiate(prefab, spawnPos.position, prefab.transform.rotation, transform);
            //print("VISUAL : " + (visual == null));
            Destroy(visual.GetComponent<Collider>()); // Destroy any collider on this visual so we don't have collision issues.
            itemsInCart.Add(visual);
            UpdateLocation();
        }
    }

    /// <summary>
    /// Updates the location of the spawnPos so we know where
    /// to spawn in the next item.
    /// </summary>
    private void UpdateLocation()
    {
        tile = new GridTile(grid, tile.ROW + 1, tile.COLUMN, tile.HEIGHT);
        spawnPos.localPosition = tile.POS;
    }

    private void RemoveItems()
    {
        // If there are any items in the cart,
        // just remove them in bulk.
        if (itemsInCart.Count > 0)
        {
            foreach (GameObject item in itemsInCart)
                Destroy(item);

            tile = new GridTile(grid);
            spawnPos.localPosition = tile.POS;
        }
    }
}

public class Grid
{
    private readonly int ROWS;
    private readonly int COLS;
    private readonly Vector3 ORIGIN_POS;

    private const float TILE_WIDTH = 0.206f;
    private const float TILE_LENGTH = 0.3f;
    private const float TILE_HEIGHT = 0.25f;

    public Grid(int rows, int cols, Vector3 originPos)
    {
        ROWS = rows;
        COLS = cols;
        ORIGIN_POS = originPos;
    }

    public Vector2 GetRowsCols()
    {
        return new Vector2(ROWS, COLS);
    }

    /// <summary>
    /// Returns the tile's dimensions.
    /// </summary>
    /// <returns>A Vector3 representing the tile's width, length, and height.</returns>
    public Vector3 GetTileSize()
    {
        return new Vector3(TILE_WIDTH, TILE_HEIGHT, TILE_LENGTH);
    }

    public Vector3 GetOriginPos()
    {
        return ORIGIN_POS;
    }
}

public struct GridTile
{
    private Grid grid;
    public readonly int ROW;
    public readonly int COLUMN;
    public readonly int HEIGHT;
    public readonly Vector3 POS;

    public GridTile(Grid g, int row = 1, int column = 1, int height = 1)
    {
        grid = g;
        ROW = row;
        COLUMN = column;
        HEIGHT = height;

        POS = grid.GetOriginPos();

        if (ROW > grid.GetRowsCols().x)
        {
            ROW = 1;
            ++COLUMN;

            if (COLUMN > grid.GetRowsCols().y)
            {
                COLUMN = 1;
                ++HEIGHT;
            }
        }

        for (int i = 1; i < ROW; ++i)
        {
            POS.x += grid.GetTileSize().x;
        }

        for (int j = 1; j < COLUMN; ++j)
        {
            POS.z -= grid.GetTileSize().z;
        }

        for (int k = 1; k < HEIGHT; ++k)
        {
            POS.y += grid.GetTileSize().y;
        }
    }
}