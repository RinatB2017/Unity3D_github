
using System.Collections.Generic;
using UnityEngine;
using Homebrew;

public class Loader : MonoBehaviour
{


    public GameObject prefab;
 
    
    public List<GameObject> objs = new List<GameObject>();
 

    void Awake()
    {
        ManagerPool.Instance.AddPool(PoolType.Entities);
     
    }



    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            for (var i = 0; i < 1; i++)
            {
                objs.Add(ManagerPool.Instance.Spawn(PoolType.Entities,prefab)); 
            }

        }
      
        if (Input.GetKeyDown(KeyCode.W))
        {
            for (var i = 0; i < objs.Count; i++)
            {
               ManagerPool.Instance.Despawn(PoolType.Entities, objs[i]);
            }
            objs.Clear();
        }
     

    }
    
}
