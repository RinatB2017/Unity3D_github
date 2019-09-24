
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // This script applies a physics explosion force to all
    // nearby rigidbodies within a specified radius.
    public float radius = 4;
    public float force = 8;

    void Start()
    {
        // Explosion applies force to all nearby objects as soon as it is instantiated.
        Collider[] affected = Physics.OverlapSphere(transform.position,5);

        // find all colliders overlapping the explosion radius
        foreach (var col in affected)
        {
            // apply a force to each of those colliders that has a rigidbody
            if (col.GetComponent<Rigidbody>() != null)
            {
                col.GetComponent<Rigidbody>().AddExplosionForce(force,transform.position, radius, force*0.5f, ForceMode.Impulse);
            }
        }

        Destroy(gameObject,5);  
    }
}
