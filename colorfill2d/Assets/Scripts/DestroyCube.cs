using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Green")
        {
            Destroy(this.gameObject);
        }
    }
}
