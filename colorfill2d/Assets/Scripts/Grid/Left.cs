using UnityEngine;

public class Left : MonoBehaviour
{
    FloodFill floodFill;

    void Start()
    {
        floodFill = FindObjectOfType<FloodFill>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Green")
        {
            PlayerControl.Instance.leftCounter += 1;
            if (PlayerControl.Instance.leftCounter >= 2)
            {
                PlayerControl.Instance.rightCounter = 0;

                PlayerControl.Instance.leftCounter = 0;
                StartCoroutine(floodFill.Flood(Mathf.RoundToInt(this.transform.GetChild(0).position.x), Mathf.RoundToInt(this.transform.GetChild(0).position.y), Color.white, Color.green));
            }
        }
    }
}
