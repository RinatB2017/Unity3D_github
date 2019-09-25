using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class charizardScript : MonoBehaviour
{

    public Button attackButton2;
    public GameObject hose2;

    // Use this for initialization
    void Start()
    {
        attackButton2.onClick.AddListener(attackButton2Down);
        hose2.transform.Find("WaterShower").gameObject.SetActive(false);
    }

    IEnumerator Wait()
    {
        hose2.transform.Find("WaterShower").gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        hose2.transform.Find("WaterShower").gameObject.SetActive(false);

    }

    void attackButton2Down()
    {
        StartCoroutine(Wait());
    }
    // Update is called once per frame
    void Update()
    {

    }
}
