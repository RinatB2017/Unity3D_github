using UnityEngine;

public class Bubble : MonoBehaviour
{
    Animator animator;

    private float speed;
    private float speedMax = 7f;
    private bool die = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        speed = Random.Range(0.1f, speedMax);
    }
    private void Update()
    {
        if (!die)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + speed, transform.position.z), transform.rotation);
        }
    }

    public void StartDie()
    {
        die = true;
        animator.SetBool("die", true);
        Destroy(gameObject, 0.7f);
    }
}
