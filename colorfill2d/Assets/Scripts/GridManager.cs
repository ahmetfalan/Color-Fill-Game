using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabTile;
    public int xSize, ySize;
    public GameObject[,] grid;
    void Start()
    {
        CreateGrid(xSize, ySize);
    }

    public void ColorTile(int x, int y, Color color)
    {
        grid[x, y].GetComponent<SpriteRenderer>().color = color;
        grid[x, y].tag = "Green";
        grid[x, y].layer = 8;
    }

    public Vector2 GetCurrent(int x, int y)
    {
        return grid[x, y].transform.position;
    }

    private Vector2 GetSize(GameObject tile)
    {
        return new Vector2(tile.GetComponent<SpriteRenderer>().bounds.size.x, tile.GetComponent<SpriteRenderer>().bounds.size.y);
    }

    public bool CheckBordersFull()
    {
        return true;



    }

    private void CreateGrid(int width, int height)
    {
        grid = new GameObject[width, height];
        Vector2 startPos = this.transform.position;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject tile = Instantiate(prefabTile);
                tile.transform.parent = this.transform;
                tile.transform.position = new Vector2(startPos.x + (GetSize(prefabTile).x * x), startPos.y + (GetSize(prefabTile).y * y));
                tile.tag = "White";
                tile.layer = 7;
                grid[x, y] = tile;
            }
        }
        MakeBorders();
    }

    private void MakeBorders()
    {
        for (int i = 0; i < xSize; i++)
        {
            GameObject tile1 = Instantiate(prefabTile);
            tile1.transform.parent = this.transform;
            tile1.transform.position = new Vector2(xSize - i - 1, ySize);
            tile1.GetComponent<SpriteRenderer>().color = Color.black;

            tile1.gameObject.AddComponent<BoxCollider2D>();
            tile1.gameObject.tag = "Border";
            tile1.gameObject.layer = 6;


            GameObject tile2 = Instantiate(prefabTile);
            tile2.transform.parent = this.transform;
            tile2.transform.position = new Vector2(xSize - i - 1, -1);
            tile2.GetComponent<SpriteRenderer>().color = Color.black;

            tile2.gameObject.AddComponent<BoxCollider2D>();
            tile2.gameObject.tag = "Border";
            tile2.gameObject.layer = 6;
        }

        for (int i = 0; i < ySize; i++)
        {
            GameObject tile1 = Instantiate(prefabTile);
            tile1.transform.parent = this.transform;
            tile1.transform.position = new Vector2(xSize, ySize - i - 1);
            tile1.GetComponent<SpriteRenderer>().color = Color.black;

            tile1.gameObject.AddComponent<BoxCollider2D>();
            tile1.gameObject.tag = "Border";
            tile1.gameObject.layer = 6;


            GameObject tile2 = Instantiate(prefabTile);
            tile2.transform.parent = this.transform;
            tile2.transform.position = new Vector2(-1, ySize - i - 1);
            tile2.GetComponent<SpriteRenderer>().color = Color.black;

            tile2.gameObject.AddComponent<BoxCollider2D>();
            tile2.gameObject.tag = "Border";
            tile2.gameObject.layer = 6;
        }
    }
}
