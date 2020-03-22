using UnityEngine;

public class deathScript : MonoBehaviour
{
    public float deathTime = 1.5f;
    private void Start()
    {
        Destroy(gameObject, deathTime);
    }
}
