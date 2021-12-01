using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{
    [SerializeField]
    private float fillDelay = 0.01f;
    public int xSize, ySize;
    public GameObject prefabCube;

    private GridManager gridManager;
    private Vector2 GetSize(GameObject tile)
    {
        return new Vector2(tile.GetComponent<SpriteRenderer>().bounds.size.x, tile.GetComponent<SpriteRenderer>().bounds.size.y);
    }
    void Start()
    {
        gridManager = this.GetComponent<GridManager>();

        Vector2 startPos = this.transform.position;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject tile = Instantiate(prefabCube);
                tile.transform.parent = this.transform;
                tile.transform.position = new Vector2(startPos.x + (GetSize(prefabCube).x * x), startPos.y + (GetSize(prefabCube).y * y));
                tile.tag = "White";
                tile.gameObject.AddComponent<BoxCollider2D>();
                tile.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                tile.layer = 7;
            }
        }
    }

    public IEnumerator Flood(int x, int y, Color oldColor, Color newColor)
    {
        WaitForSeconds wait = new WaitForSeconds(fillDelay);
        if (x >= 0 && x < gridManager.xSize && y >= 0 && y < gridManager.ySize)
        {
            yield return wait;
            if (gridManager.grid[x, y].GetComponent<SpriteRenderer>().color == oldColor)
            {
                gridManager.grid[x, y].GetComponent<SpriteRenderer>().color = newColor;
                gridManager.grid[x, y].tag = "Green";
                gridManager.grid[x, y].layer = 8;
                StartCoroutine(Flood(x + 1, y, oldColor, newColor));
                StartCoroutine(Flood(x - 1, y, oldColor, newColor));
                StartCoroutine(Flood(x, y + 1, oldColor, newColor));
                StartCoroutine(Flood(x, y - 1, oldColor, newColor));
                GameManager.Instance.LevelDone();
            }
        }
    }
}
