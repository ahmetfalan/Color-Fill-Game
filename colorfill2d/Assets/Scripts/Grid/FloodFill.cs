using System.Collections;
using UnityEngine;

public class FloodFill : MonoBehaviour
{
    private GridManager gridManager;

    [SerializeField]
    private float fillDelay = 0.01f;

    void Start()
    {
        gridManager = this.GetComponent<GridManager>();
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
