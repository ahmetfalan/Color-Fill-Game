using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabTile;
    public int xSize, ySize;
    public GameObject[,] grid;

    List<Tuple<int, GameObject>> abstractGrid = new List<Tuple<int, GameObject>>();

    void Awake()
    {
        CreateGrid(xSize, ySize);
    }

    private void Update()
    {
        //CheckClosedShapes(starPoint, endPoint);
    }

    public void ColorTile(int x, int y, Color color)
    {
        grid[x, y].GetComponent<SpriteRenderer>().color = color;
        grid[x, y].tag = "Green";
        grid[x, y].layer = 8;
        GameManager.Instance.LevelDone();
    }


    private Vector2 GetSize(GameObject tile)
    {
        return new Vector2(tile.GetComponent<SpriteRenderer>().bounds.size.x, tile.GetComponent<SpriteRenderer>().bounds.size.y);
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
                tile.gameObject.AddComponent<BoxCollider2D>();
                tile.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                tile.layer = 7;
                grid[x, y] = tile;


                abstractGrid.Add(new Tuple<int, GameObject> (0, grid[x, y]));
            }
        }

        //var result1 = abstractGrid.Where(x => x.Item1 == 0);
        //Debug.Log(result1.Count());
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
