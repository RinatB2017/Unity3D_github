using UnityEngine;

public class StartObj : MonoBehaviour
{
    private Vector3 startPoint;
    public Transform EndPoint;

    private float timeMove = 0.0f;
    public bool verticalStart = false;

    private void Start()
    {
        startPoint = transform.position;
    }

    private void Update()
    {
        if (!verticalStart) transform.position = new Vector3(Mathf.Lerp(startPoint.x, EndPoint.position.x, timeMove), startPoint.y, startPoint.z);
        else transform.position = new Vector3(startPoint.x, Mathf.Lerp(startPoint.y, EndPoint.position.y, timeMove), startPoint.z);
        timeMove += 0.5f * Time.deltaTime;

        if (transform.position.x >= EndPoint.position.x && !verticalStart)
        {
            if (gameObject.GetComponent<EnemyScript>() != null) gameObject.GetComponent<EnemyScript>().enabled = true;
            else gameObject.GetComponent<PlayerScript>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(gameObject.GetComponent<StartObj>());
        }
        else if (transform.position.y <= EndPoint.position.y && verticalStart)
        {
            if (gameObject.GetComponent<EnemyScript>() != null) gameObject.GetComponent<EnemyScript>().enabled = true;
            else gameObject.GetComponent<PlayerScript>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(gameObject.GetComponent<StartObj>());
        }
    }
}
