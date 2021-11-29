using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private Grid grid;

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
            nextPosition = Vector3.forward;
            //currentDirection = up;
            mov = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            nextPosition = Vector3.back;
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
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPos;
        }
        grid.AddPosition(finalPos);
        //Debug.Log(finalPos);
        //}
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            mov = false;
            //var finalPos = grid.GetNearestPointOnGrid(this.transform.position);
            //this.transform.position = finalPos;
        }
    }
}
