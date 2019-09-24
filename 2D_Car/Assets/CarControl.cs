using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour {
	
	public WheelJoint2D[] wheels;
	void Start(){
		wheels = gameObject.GetComponents<WheelJoint2D> ();
	}
	void Update () {
		
		if (Input.GetAxis ("Horizontal") <-0.8 || Input.GetAxis ("Horizontal") >0.8) {
			foreach (WheelJoint2D joint in wheels) {
				var motor = joint.motor;
				motor.motorSpeed = 1000 * Input.GetAxis ("Horizontal")*-1; 
				joint.motor = motor;
				joint.useMotor = true;
			}
		} else {
			foreach (WheelJoint2D joint in wheels) {
				var motor = joint.motor;
				motor.motorSpeed = 0;
				joint.motor = motor;
				joint.useMotor = false;
			}
		}
		
	}
	void FixedUpdate()
	{
		PlayerPrefs.SetFloat ("ObjX", gameObject.transform.position.x);
		PlayerPrefs.SetFloat ("ObjY", gameObject.transform.position.y);
	//	PlayerPrefs.SetFloat ("CarSpeed", CarSpeed / 10 * 0.9f);
	}
}

