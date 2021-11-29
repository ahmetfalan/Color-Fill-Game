using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    FloodFill floodFill;
    GridManager gridManager;

    Vector3 up = Vector3.zero;

    Vector3 right = new Vector3(0, 0, 270);

    Vector3 down = new Vector3(0, 0, 180);

    Vector3 left = new Vector3(0, 0, 90);

    Vector3 currentDirection = Vector3.zero;

    Vector3 nextPosition, destination, direction;

    public float speed = 1f;

    public bool mov = false;

    public LayerMask layerMask;

    private void Awake()
    {
        floodFill = FindObjectOfType<FloodFill>();
        gridManager = FindObjectOfType<GridManager>();
    }
    //private Cube cube;

    void Start()
    {
        currentDirection = up;
        nextPosition = Vector2.up;
        destination = transform.position;
        // cube = FindObjectOfType<Cube>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!mov)
        {
            nextPosition = Vector2.zero;
        }
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        //cube = FindObjectOfType<Cube>();
        if (Input.GetKeyDown(KeyCode.W))
        {
            mov = true;
            nextPosition = Vector2.up;
            currentDirection = up;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mov = true;
            nextPosition = Vector2.down;
            currentDirection = down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            mov = true;
            nextPosition = Vector2.right;
            currentDirection = right;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            mov = true;
            nextPosition = Vector2.left;
            currentDirection = left;
        }

        if (mov)
        {
            if (Vector3.Distance(destination, transform.position) <= 0.00001f)
            {
                transform.localEulerAngles = currentDirection;
                destination = transform.position + nextPosition;
            }

            gridManager.ColorTile(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), Color.green);
        }
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1, layerMask);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 1, Color.blue);

        RaycastHit2D frontHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1, layerMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 1, Color.blue);

        RaycastHit2D lefthit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1, layerMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 1, Color.red);
        
        RaycastHit2D righthit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1, layerMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 1, Color.green);

        if (true)
        {

        }

        if (frontHit.collider != null)
        {
            mov = false;
            if (Mathf.RoundToInt(this.transform.position.x) >= gridManager.xSize / 2 && Mathf.RoundToInt(this.transform.position.y) >= gridManager.ySize / 2)
            {
                if (currentDirection == up)
                {
                    StartCoroutine(floodFill.Flood(Mathf.RoundToInt(this.transform.position.x + 1), Mathf.RoundToInt(this.transform.position.y), Color.white, Color.green));
                }

                if (currentDirection == right)
                {
                    StartCoroutine(floodFill.Flood(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y + 1), Color.white, Color.green));
                }
            }

            if (Mathf.RoundToInt(this.transform.position.x) <= gridManager.xSize / 2 && Mathf.RoundToInt(this.transform.position.y) <= gridManager.ySize / 2)
            {
                if (currentDirection == down)
                {
                    StartCoroutine(floodFill.Flood(Mathf.RoundToInt(this.transform.position.x - 1), Mathf.RoundToInt(this.transform.position.y), Color.white, Color.green));
                }

                if (currentDirection == left)
                {
                    StartCoroutine(floodFill.Flood(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y - 1), Color.white, Color.green));
                }
            }

            if (Mathf.RoundToInt(this.transform.position.x) < gridManager.xSize / 2 && Mathf.RoundToInt(this.transform.position.y) > gridManager.ySize / 2)
            {
                if (currentDirection == up)
                {
                    StartCoroutine(floodFill.Flood(Mathf.RoundToInt(this.transform.position.x - 1), Mathf.RoundToInt(this.transform.position.y), Color.white, Color.green));
                }

                if (currentDirection == left)
                {
                    StartCoroutine(floodFill.Flood(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y + 1), Color.white, Color.green));
                }
            }

            if (Mathf.RoundToInt(this.transform.position.x) > gridManager.xSize / 2 && Mathf.RoundToInt(this.transform.position.y) < gridManager.ySize / 2)
            {
                if (currentDirection == down)
                {
                    StartCoroutine(floodFill.Flood(Mathf.RoundToInt(this.transform.position.x - 1), Mathf.RoundToInt(this.transform.position.y), Color.white, Color.green));
                }

                if (currentDirection == right)
                {
                    StartCoroutine(floodFill.Flood(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y - 1), Color.white, Color.green));
                }
            }
        }
        else
        {
            mov = true;
        }


        //Debug.Log(boardManager.GetCurrent(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y)).ToString());

        /*RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1, layerMask);//from my transform in the direction to the player as seen in the debug ray
        Vector3 forward = transform.TransformDirection(Vector3.up) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
        //Debug.DrawRay(transform.position, currentDirection * 10, Color.blue);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.tag);//write HIT
        }*/
    }


    //public static PlayerControl Instance;

    //public float speed = 5.0f;

    //private Vector3 pos;
    //private Transform tr;

    //FloodFill floodFill;
    //BoardManager boardManager;

    //private void Awake()
    //{
    //    Instance = this;
    //}
    //void Start()
    //{
    //    pos = transform.position;
    //    tr = transform;
    //    floodFill = FindObjectOfType<FloodFill>();
    //    boardManager = FindObjectOfType<BoardManager>();
    //}
    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.D) && tr.position == pos)
    //    {
    //        pos += Vector3.right;
    //    }
    //    else if (Input.GetKey(KeyCode.A) && tr.position == pos)
    //    {
    //        pos += Vector3.left;
    //    }
    //    else if (Input.GetKey(KeyCode.W) && tr.position == pos)
    //    {
    //        pos += Vector3.up;
    //    }
    //    else if (Input.GetKey(KeyCode.S) && tr.position == pos)
    //    {
    //        pos += Vector3.down;
    //    }

    //    transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);

    //    boardManager.ColorTile(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), Color.red);


    //    /*RaycastHit2D hit;
    //    if (Physics2D.Raycast(Camera.main.transform.position, transform.up, out hit))
    //    {
    //        //isHit = false;
    //        GameObject.Find(hit.collider.gameObject.name);
    //    }*/

    //    //RaycastHit2D hit = Physics2D.Raycast(pos, transform.up, 5);
    //    /*RaycastHit hit;

    //    if (Physics2D.Raycast(transform.position, transform.up, out hit))
    //    {
    //        if (hit.collider.gameObject.tag == "Border")
    //        {
    //            Debug.DrawRay(transform.position, transform.up, Color.green);
    //            Debug.Log("Hit");
    //        }
    //    }*/

    //    //Vector3 mousePos = Input.mousePosition;
    //    //mousePos.z = 10;

    //    Vector3 screenPos = Camera.main.ScreenToWorldPoint(pos);

    //    RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.up);

    //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.blue);
    //    if (hit)
    //    {
    //        Debug.Log(hit.collider.name);
    //    }
    //}


    //public static PlayerControl Instance;
    //private bool isMoving;

    //private Vector3 originPosition, targetPosition;
    //private float timeToMove = 0.1f;

    //Grid grid;
    //BoardManager boardManager;
    //FloodFill floodFill;

    //public LayerMask layerMask;
    //private void Awake()
    //{
    //    Instance = this;
    //}

    //private void Start()
    //{
    //    grid = FindObjectOfType<Grid>();
    //    floodFill = FindObjectOfType<FloodFill>();
    //    boardManager = FindObjectOfType<BoardManager>();
    //}
    //RaycastHit hit;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.W) && !isMoving)
    //        Move(Vector3.up);
    //    if (Input.GetKeyDown(KeyCode.A) && !isMoving)
    //        Move(Vector3.left);
    //    if (Input.GetKeyDown(KeyCode.S) && !isMoving)
    //        Move(Vector3.down);
    //    if (Input.GetKeyDown(KeyCode.D) && !isMoving)
    //        Move(Vector3.right);


    //    boardManager.ColorTile(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), Color.red);

    //    //RaycastHit hit;
    //    // Does the ray intersect any objects excluding the player layer
    //    /*if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    //        Debug.Log("Hit");
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.blue);
    //        Debug.Log("No Hit");
    //    }
    //    else
    //    {

    //    }*/

    //    if (Physics.Raycast(transform.position, transform.up, out hit)) 
    //    { 
    //        if (hit.collider.gameObject.tag == "Border") 
    //        {
    //            Debug.DrawRay(transform.position, transform.up, Color.green);
    //            Debug.Log("Hit"); 
    //        } 
    //    }


    //    // Does the ray intersect any objects excluding the player layer
    //    /*if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    //        Debug.Log("Did Hit");
    //    }*/
    //    //if (Input.GetKeyDown("a"))
    //    //{
    //    //StartCoroutine(floodFill.Flood(Mathf.RoundToInt(this.transform.position.x + 1), Mathf.RoundToInt(this.transform.position.y + 1), Color.white, Color.red));
    //    //}

    //    ///var finalPos = grid.GetNearestPointOnGrid(this.transform.position);
    //    //if (grid.Check(finalPos))
    //    //{
    //    //GameObject.CreatePrimitive(PrimitiveType.Quad).transform.position = finalPos;
    //    //}
    //    //grid.AddPosition(finalPos);
    //}

    //private void Move(Vector3 direction)
    //{
    //    isMoving = true;
    //    originPosition = transform.position;
    //    targetPosition = originPosition + direction;
    //    /*while (elapsedTime < timeToMove)
    //    {
    //        transform.position = Vector3.Lerp(originPosition, targetPosition, (elapsedTime / timeToMove));
    //        elapsedTime += Time.deltaTime;
    //        //yield return null;
    //    }*/
    //    transform.position = targetPosition;
    //    //transform.position += originPosition;

    //    isMoving = false;
    //}
    /*private Grid grid;

    Vector3 up = Vector3.zero;

    Vector3 right = new Vector3(0, 90, 0);

    Vector3 down = new Vector3(0, 180, 0);

    Vector3 left = new Vector3(0, 270, 0);

    //Vector3 currentDirection = Vector3.zero;

    Vector3 nextPosition, destination, direction;

    public float speed = 1f;

    public bool mov;

    public GameObject gameObject;

    //private Cube cube;

    void Start()
    {
        grid = FindObjectOfType<Grid>();
        //currentDirection = up;
        nextPosition = Vector3.forward;
        destination = transform.position;
        // cube = FindObjectOfType<Cube>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        //cube = FindObjectOfType<Cube>();
        if (Input.GetKeyDown(KeyCode.W))
        {
            nextPosition = Vector3.up;
            //currentDirection = up;
            mov = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            nextPosition = Vector3.down;
            //currentDirection = down;
            mov = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            nextPosition = Vector3.right;
            //currentDirection = right;
            mov = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            nextPosition = Vector3.left;
            //currentDirection = left;
            mov = true;
        }

        if (mov == true)
        {
            //destination.x = Mathf.Clamp(destination.x + nextPosition.x, 0, 10);
            //destination.z = Mathf.Clamp(destination.z + nextPosition.z, 0, 10);
            destination = transform.position + nextPosition;
            //Debug.Log(destination);
        }

        //if (Vector3.Distance(destination, transform.position) <= 0.00001f)
        //{
        //transform.localEulerAngles = currentDirection;
        //}
        //var finalPos = grid.GetNearestPointOnGrid();
        var finalPos = grid.GetNearestPointOnGrid(this.transform.position);
        //if (cube.transform.position != finalPos)
        //{
        //if (grid.Check(finalPos))
        //{
        if (grid.Check(finalPos))
        {
            GameObject.CreatePrimitive(PrimitiveType.Quad).transform.position = finalPos;
        }
        grid.AddPosition(finalPos);
        //Debug.Log(finalPos);
        //}
        //}
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            mov = false;
            //var finalPos = grid.GetNearestPointOnGrid(this.transform.position);
            //this.transform.position = finalPos;
        }
    }*/
}
