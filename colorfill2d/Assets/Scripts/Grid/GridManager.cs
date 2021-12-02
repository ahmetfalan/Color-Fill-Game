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
            GameObject upBorder = Instantiate(prefabTile);
            upBorder.transform.parent = this.transform;
            upBorder.transform.position = new Vector2(xSize - i - 1, ySize);
            upBorder.GetComponent<SpriteRenderer>().color = Color.black;

            upBorder.gameObject.AddComponent<Rigidbody2D>();
            upBorder.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            upBorder.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            upBorder.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            upBorder.gameObject.AddComponent<BoxCollider2D>();
            upBorder.gameObject.tag = "UpBorder";
            upBorder.gameObject.layer = 6;




            GameObject downBorder = Instantiate(prefabTile);
            downBorder.transform.parent = this.transform;
            downBorder.transform.position = new Vector2(xSize - i - 1, -1);
            downBorder.GetComponent<SpriteRenderer>().color = Color.black;

            downBorder.gameObject.AddComponent<Rigidbody2D>();
            downBorder.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            downBorder.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            downBorder.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            downBorder.gameObject.AddComponent<BoxCollider2D>();
            downBorder.gameObject.tag = "DownBorder";
            downBorder.gameObject.layer = 6;
        }

        for (int i = 0; i < ySize; i++)
        {
            GameObject rightBorder = Instantiate(prefabTile);
            rightBorder.transform.parent = this.transform;
            rightBorder.transform.position = new Vector2(xSize, ySize - i - 1);
            rightBorder.GetComponent<SpriteRenderer>().color = Color.black;

            rightBorder.gameObject.AddComponent<Rigidbody2D>();
            rightBorder.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            rightBorder.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            rightBorder.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            rightBorder.gameObject.AddComponent<BoxCollider2D>();
            rightBorder.gameObject.tag = "RightBorder";
            rightBorder.gameObject.layer = 6;




            GameObject leftBorder = Instantiate(prefabTile);
            leftBorder.transform.parent = this.transform;
            leftBorder.transform.position = new Vector2(-1, ySize - i - 1);
            leftBorder.GetComponent<SpriteRenderer>().color = Color.black;

            leftBorder.gameObject.AddComponent<Rigidbody2D>();
            leftBorder.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            leftBorder.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            leftBorder.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            leftBorder.gameObject.AddComponent<BoxCollider2D>();
            leftBorder.gameObject.tag = "LeftBorder";
            leftBorder.gameObject.layer = 6;
        }
    }

}
