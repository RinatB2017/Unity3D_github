using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator animator;
    public GameObject ball;
    public GameObject[] bonuses;
    private SpriteRenderer spriteRenderer;
    public Sprite[] spritesPlayer;
    private int countSprite = 0;

    public float speed = 40f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
        GameManager.Instance.ballObj.Add(Instantiate(ball, new Vector3(transform.position.x, transform.position.y + 0.04f, 5), Quaternion.identity));
        GameManager.Instance.isBall = false;
        UIManager.Instance.ballImage.SetActive(false);
    }

    private void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed * Input.GetAxis("Horizontal") * GameManager.Instance.playerSpeed;
#endif
#if UNITY_ANDROID
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed * (Input.acceleration.x * 2) * GameManager.Instance.playerSpeed;
#endif

        if (transform.position.y <= -290.05f)
        {
            transform.position.Set(transform.position.x, -340.05f, transform.position.z);
            StartCoroutine(UnDmg());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bonus")
        {
            collision.gameObject.tag = "Untagged";
            Destroy(collision.GetComponent<Rigidbody2D>());
            collision.GetComponent<Animator>().SetBool("open", true);
            int bonus = Random.Range(0, 11);
            Instantiate(bonuses[bonus], new Vector3(collision.transform.position.x, collision.transform.position.y + 70f, collision.transform.position.z), Quaternion.identity);
            GameManager.Instance.GetBonus(bonus);
            Destroy(collision.gameObject, 0.4f);
        }
    }

    public void StartDie()
    {
        animator.enabled = true;
        animator.SetBool("die", true);
        Destroy(gameObject, 0.7f);
    }

    public IEnumerator GetDmg()
    {
        for (float time = 0.0f; time <= 1; time += Time.deltaTime)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(-270.05f, -340.05f, time), transform.position.z);
            yield return null;
        }
    }

    public IEnumerator UnDmg()
    {
        for (float time = 0.0f; time <= 1; time += Time.deltaTime)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(-340.05f, -270.05f, time), transform.position.z);
            yield return null;
        }
    }

    public void SetSprite(int count)
    {
        if (countSprite > 0 && count == (-1))
        {
            countSprite += count;
            spriteRenderer.sprite = spritesPlayer[countSprite];
            if (countSprite == 0)
            {
                GetComponent<BoxCollider2D>().size = new Vector2(0.11f, 0.04f);
            }
            else if (countSprite == 1)
            {
                GetComponent<BoxCollider2D>().size = new Vector2(0.17f, 0.04f);
            }
        }
        else if (countSprite < 2 && count == 1)
        {
            countSprite += count;
            spriteRenderer.sprite = spritesPlayer[countSprite];
            if (countSprite == 2)
            {
                GetComponent<BoxCollider2D>().size = new Vector2(0.21f, 0.04f);
            }
            else if (countSprite == 1)
            {
                GetComponent<BoxCollider2D>().size = new Vector2(0.17f, 0.04f);
            }
        }
    }
}
