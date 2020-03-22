using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bubble")
        {
            collision.GetComponent<Bubble>().StartDie();
        }
        else if (collision.gameObject.tag == "cloud")
        {
            Destroy(collision.gameObject);
        }
    }
}
