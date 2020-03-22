using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScript : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * 400f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "endText")
        {
            if (gameObject.name == "gameover(Clone)")
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                GameManager.Instance.playerSpeed = 1f;
                GameManager.Instance.controlBall = false;
                GameManager.Instance.ready = false;
                Destroy(gameObject);
            }
        }
    }
}
