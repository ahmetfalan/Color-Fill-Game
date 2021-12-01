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
    Vector3 nextPosition, destination;

    public float moveSpeed = 1f;


    public LayerMask layerMask;


    public int leftCounter = 0;
    public int rightCounter = 0;

    public bool canMove = false;
    public bool isDead = false;
    private void Awake()
    {
        Instance = this;
        isDead = false;
        floodFill = FindObjectOfType<FloodFill>();
        gridManager = FindObjectOfType<GridManager>();
    }

    void Start()
    {
        currentDirection = up;
        nextPosition = Vector2.up;
        destination = transform.position;
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        if (!canMove)
        {
            nextPosition = Vector2.zero;
            leftCounter = 0;
            rightCounter = 0;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            canMove = true;
            nextPosition = Vector2.up;
            currentDirection = up;

            leftCounter = 0;
            rightCounter = 0;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            canMove = true;
            nextPosition = Vector2.down;
            currentDirection = down;

            leftCounter = 0;
            rightCounter = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            canMove = true;
            nextPosition = Vector2.right;
            currentDirection = right;

            leftCounter = 0;
            rightCounter = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            canMove = true;
            nextPosition = Vector2.left;
            currentDirection = left;

            leftCounter = 0;
            rightCounter = 0;
        }

        if (canMove)
        {
            if (Vector3.Distance(destination, transform.position) <= 0.00001f)
            {
                transform.localEulerAngles = currentDirection;
                destination = transform.position + nextPosition;
            }

            gridManager.ColorTile(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), Color.green);
        }
 
        RaycastHit2D frontHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1, layerMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 1, Color.blue);

        RaycastHit2D lefthit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1, layerMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 1, Color.red);
        
        RaycastHit2D righthit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1, layerMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 1, Color.green);

        
        if (frontHit.collider != null)
        {
            canMove = false;
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
            canMove = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Red")
        {
            isDead = true;
        }
    }
}
