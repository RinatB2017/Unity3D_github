using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject[] bullet;
    private float speed = 30f;

    private float TimeGun;
    private float controlTimeGun = 2f;

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        Fire();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "col")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            speed *= (-1);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            float dmg;
            if (collision.gameObject.GetComponent<BallScript>().big == false)
            {
                dmg = Random.Range(0.01f, 0.05f);
            }
            else
            {
                dmg = Random.Range(0.1f, 0.2f);
            }
            GameManager.Instance.enemyHP -= dmg;
            StartCoroutine(UIManager.Instance.ChangeEnemyLifeBar(dmg));
        }
    }

    private void Fire()
    {
        TimeGun += Time.deltaTime;
        if (TimeGun >= controlTimeGun / (GameManager.Instance.speed / 4) && GameManager.Instance.ready == false)
        {
            Instantiate(bullet[Random.Range(0, 4)], new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), Quaternion.identity);
            TimeGun = 0;
        }
    }
}
