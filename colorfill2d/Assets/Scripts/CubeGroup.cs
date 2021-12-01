using System.Collections;
using UnityEngine;

public class CubeGroup : MonoBehaviour
{
    [SerializeField] private GameObject prefabTile;
    public int xSize, ySize;
    public GameObject[,] grid;

    public Vector2 startPoint;
    public Vector2 endPoint;

    public float moveSpeed = 1f;
    void Awake()
    {
        CreateGrid(xSize, ySize);
        startPoint = transform.position;
    }

    IEnumerator Start()
    {
        startPoint = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, startPoint, endPoint, moveSpeed));
            yield return StartCoroutine(MoveObject(transform, endPoint, startPoint, moveSpeed));
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector2 startPos, Vector2 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector2.Lerp(startPos, endPos, i);
            yield return null;
        }
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
                tile.tag = "Red";
                tile.gameObject.AddComponent<BoxCollider2D>();
                tile.gameObject.AddComponent<DestroyCube>();
                tile.gameObject.AddComponent<Rigidbody2D>();
                tile.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                tile.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                tile.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                tile.layer = 9;
                grid[x, y] = tile;
            }
        }
    }
}
