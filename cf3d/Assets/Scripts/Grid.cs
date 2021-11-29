using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public float size = 1.1f;

    public int xSize, zSize;
    public Vector3[] vertices;

    public List<Vector3> ff = new List<Vector3>();

    private void Start()
    {
        ff.Clear();
        Generate();
    }

    private void Generate()
    {
        vertices = new Vector3[(xSize) * (zSize) * Mathf.RoundToInt(size)];
        if (vertices == null)
            return;

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, z);
                //Debug.Log(vertices[i]);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        Gizmos.color = Color.grey;
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = point;
                Gizmos.DrawCube(point, new Vector3(1, 0f, 1));
            }
        }
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3((float)(xCount * size), 0, (float)(zCount * size));

        result += transform.position;

        return result;
    }

    public void AddPosition(Vector3 position)
    {
        if (!ff.Contains(position))
        {
            ff.Add(position);
        }
    }

    public bool Check(Vector3 position)
    {
        if (ff.Contains(position))
        {
            return false;
        }
        return true;
    }

    bool[, ,] grid;

    public void FloodFill(bool[,] grid)
    {
        grid = new bool[xSize, zSize];

        // Taken from http://rosettacode.org/wiki/Bitmap/Flood_fill#C.23
        Queue<Vector3> q = new Queue<Vector3>();
        q.Enqueue(Vector3.zero);
        while (q.Count > 0)
        {
            Vector3 n = q.Dequeue();
            if (grid[n.x, n.z])
                continue;
            Vector3 w = n, e = new Vector3(n.x + 1, 0, n.z);
            while ((w.X >= 0) && !grid[w.X, w.Y])
            {
                grid[w.X, w.Y] = true;

                if ((w.Y > 0) && !grid[w.X, w.Y - 1])
                    q.Enqueue(new Point(w.X, w.Y - 1));
                if ((w.Y < zSize - 1) && !grid[w.X, w.Y + 1])
                    q.Enqueue(new Point(w.X, w.Y + 1));
                w.X--;
            }
            while ((e.X <= xSize - 1) && !grid[e.X, e.Y])
            {
                grid[e.X, e.Y] = true;

                if ((e.Y > 0) && !grid[e.X, e.Y - 1])
                    q.Enqueue(new Point(e.X, e.Y - 1));
                if ((e.Y < zSize - 1) && !grid[e.X, e.Y + 1])
                    q.Enqueue(new Point(e.X, e.Y + 1));
                e.X++;
            }
        }
    }

    /*[SerializeField]
    private float size = 1f;

    [SerializeField]
    private float gridSize = 40;

    List<Vector3> fullArea = new List<Vector3>();

    public GameObject gameObject;
    private void Start()
    {
        
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {

        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3((float)(xCount * size), (float)(yCount * size), (float)(zCount * size));

        result += transform.position;


        return result;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = point;
                Gizmos.DrawCube(point, new Vector3(1, 0f, 1));
            }
        }
    }
    public bool Check(Vector3 position)
    {
        if (!fullArea.Contains(position))
        {
            AddPositionToList(position);
            Debug.Log("yes");
        }

        if (fullArea.Contains(position))
        {
            Debug.Log("no");
            return false;
        }
        /*for (int i = 0; i < fullArea.Count; i++)
        {
            if (fullArea[i] == nearest)
            {
                return false;
                Debug.Log("ds");
            }
        }*/

    /*foreach (var item in fullArea)
    {
        if (item.Contains(nearest))
        {
            Debug.Log("ds");
        }
    }
    return true;
}*/

    /*public void AddPositionToList(Vector3 position)
    {
        fullArea.Add(position);
    }*/
}
