using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
		
	public Camera camera;

	void Start () {
		camera=gameObject.GetComponent("Camera") as Camera;	
	}
	void Update () {
		var cam=camera.transform.position;
		cam.x =PlayerPrefs.GetFloat ("ObjX");
		cam.y=PlayerPrefs.GetFloat("ObjY");
		camera.transform.position=cam;		
	}	
}

