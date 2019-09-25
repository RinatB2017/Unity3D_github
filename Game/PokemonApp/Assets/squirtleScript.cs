using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class squirtleScript : MonoBehaviour {

    public Button attackButton1;
    public GameObject hose;

	// Use this for initialization
	void Start () {
        attackButton1.onClick.AddListener (attackButton1Down);
        hose.transform.Find("WaterShower").gameObject.SetActive(false);
    }
	
    IEnumerator Wait()
    {
        hose.transform.Find("WaterShower").gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        hose.transform.Find("WaterShower").gameObject.SetActive(false);

    }

    void attackButton1Down()
    {
        StartCoroutine(Wait());
    }
	// Update is called once per frame
	void Update () {
	
	}
}
 