using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float speed;
    public float speedMax = 1f;

    private void Start()
    {
        speed = Random.Range(0.1f, speedMax);
    }
    private void Update()
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x - speed, transform.position.y, transform.position.z), transform.rotation);
    }
}
