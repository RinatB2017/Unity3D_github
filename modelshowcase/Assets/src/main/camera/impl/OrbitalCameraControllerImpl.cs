//
//  Copyright 2014  Thomas Champagne
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.


/// <summary>
/// Orbital camera controller impl.
/// </summary>
/// <exception cref='UnityException'>
/// Is thrown when the unity exception.
/// </exception>
using UnityEngine;
using System.Collections;

public class OrbitalCameraControllerImpl : AbstractCameraController, CameraControllerInterface
{
	
	//The position of the cursor on the screen. Used to rotate the camera.
	protected float xRot = 0.0f;
	private float yRot = 0.0f;
        
	//The speed of the camera. Control how fast the camera will rotate.
	public float mouseXSpeed = 5f;
	public float mouseYSpeed = 5f;
        
	//Distance vector.
	private Vector3 radiusVector;
        
	//The default distance of the camera from the target.
	private float radiusValue = Constants.defaultOrbitalCamRadiusValue;
        
	//Control the speed of zooming and dezooming.
	private float zoomStep = 1.0f;
	
	// rotation factors
	private float XrotationSpeedFactor, YrotationSpeedFactor, zoomSpeedFactor;
	
	// One finger move variables
	private Vector2 fingerOneTouchPositionOnBegan, fingerOneTouchPositionOffset;
        
	// Use this for camera initialization
	public void InitCam ()
	{
		
		if (camera == null) {
			string errorMessage = "No camera founded. Use setCamera on this Object !";
			Debug.LogError (errorMessage);
			throw new UnityException (errorMessage);
		}
                
		if (tracker == null) {
			string errorMessage = "No tracker founded. Use setTacker on this Object !";
			Debug.LogError (errorMessage);
			throw new UnityException (errorMessage);
		}
                
                
		radiusVector = new Vector3 (0, 0, -radiusValue); // don't touch
                
		// Init orbital angle, iso view...
		xRot = Constants.defaultAngleOrbitalCamValue;
		yRot = Constants.defaultAngleOrbitalCamValue;
		
		XrotationSpeedFactor = Constants.XrotationSpeedFactor;
		YrotationSpeedFactor = Constants.YrotationSpeedFactor;
		zoomSpeedFactor = Constants.zoomSpeedFactor;
		
		// If we are running on ipad 3
		if(GameManager.tabletMode && GameManager.ipadTabletVersion == 3) {
			
			Debug.Log("App is running on iPad 3");
			XrotationSpeedFactor = XrotationSpeedFactor / 2;
			YrotationSpeedFactor = YrotationSpeedFactor / 2;
			zoomSpeedFactor = zoomSpeedFactor / 2;
			
		}
		
	}
	
	/// <summary>
	/// update cam late call
	/// </summary>
	public void LateUpdateCam ()
	{
		this.MobileRotateControls ();
		//this.RotateControls();
		this.Rotate (xRot, yRot);
		this.Zoom ();
		camera.transform.LookAt (tracker.position);
	}
	
	/// <summary>
	/// Mobile touch rotate controls.
	/// </summary>
	void MobileRotateControls ()
	{
		// 1 Finger, drag ship
		if (Input.touchCount == 1)  
			handleOneFinger ();
		
		//if (!GameManager.tabletMode) { // Not into tablet
			//if (Input.GetMouseButton (Constants.MOUSE_LEFT_CLICK)) {
		//		xRot += Input.GetAxis ("Mouse X") * mouseXSpeed;
		//		yRot += -Input.GetAxis ("Mouse Y") * mouseYSpeed;
			//}
		//}
		
		// Fade out the rotation of the ship with Linear interpolation
		fadeInterpolationShipRotation ();
		
		// (Virtual) Auto rotate ship 
		autoRotation ();
		
		// 2 Fingers or more, pinch zoom
		if (Input.touchCount == 2)  
			handleTwoFingers ();

	}

	/// <summary>
	/// Handles the one finger.
	/// </summary>
	void handleOneFinger ()
	{
		Touch fingerOne = Input.touches [0];
		
		if (fingerOne.phase == TouchPhase.Began) {
			
			fingerOneTouchPositionOnBegan = fingerOne.position;
			fingerOneTouchPositionOffset = Vector2.zero;
			
		} else if (fingerOne.phase == TouchPhase.Moved) {
				
			fingerOneTouchPositionOffset = fingerOneTouchPositionOnBegan - fingerOne.position;
			fingerOneTouchPositionOffset = fingerOneTouchPositionOffset * -1;
		
		} else if (fingerOne.phase == TouchPhase.Stationary) {
			
			fingerOneTouchPositionOnBegan = fingerOne.position;
			
		}
	}
	
	/// <summary>
	/// Fades the interpolation of ship rotation.
	/// </summary>
	void fadeInterpolationShipRotation ()
	{
		fingerOneTouchPositionOffset = Vector2.Lerp (fingerOneTouchPositionOffset, Vector2.zero, Time.deltaTime * 5);
		
		xRot += fingerOneTouchPositionOffset.x * XrotationSpeedFactor;
		yRot += fingerOneTouchPositionOffset.y * YrotationSpeedFactor * -1;
		
		float angleYLimit = Constants.YrotationAngleLimit;
		yRot = Mathf.Clamp (yRot, -angleYLimit, angleYLimit);
	}
	
	
	/// <summary>
	/// Automatization of the rotation: if needed in a sub class
	/// </summary>
	protected virtual void autoRotation ()
	{

	}
	
	/// <summary>
	/// Handles the two fingers.
	/// </summary>
	void handleTwoFingers ()
	{
		Touch fingerOne = Input.touches [0];
		Touch fingerTwo = Input.touches [1];
		
		if (fingerOne.phase == TouchPhase.Moved && fingerTwo.phase == TouchPhase.Moved) {
		
			Vector2 currentPinchDistance = fingerOne.position - fingerTwo.position;
			Vector2 previousPinchDistance = (fingerOne.position - fingerOne.deltaPosition) - (fingerTwo.position - fingerTwo.deltaPosition);
			
			float pinchDelta = currentPinchDistance.magnitude - previousPinchDistance.magnitude;

			radiusValue -= pinchDelta * zoomSpeedFactor * Time.deltaTime * 10;
			
			radiusValue = Mathf.Clamp (radiusValue, Constants.radiusZoomMin, Constants.radiusZoomMax);
			radiusVector = new Vector3 (0, 0, -radiusValue);
			this.Rotate (xRot, yRot);
		}
	}
	
	public void RotateControls (Vector2 v2)
	{
		xRot += v2.x * Time.deltaTime * Constants.XrotationSpeedFactorGuiControl;
		yRot += v2.y * Time.deltaTime * Constants.YrotationSpeedFactorGuiControl;
	}
	
	/// <summary>
	/// Rotate the specified x and y.
	/// </summary>
	/// <param name='x'>
	/// X.
	/// </param>
	/// <param name='y'>
	/// Y.
	/// </param>
	void Rotate (float x, float y)
	{
		Quaternion rotation = Quaternion.Euler (y, x, 0.0f);
		Vector3 position = rotation * radiusVector + tracker.position;
		camera.transform.position = position;
	}
	
	/// <summary>
	/// Zoom
	/// </summary>
	void Zoom ()
	{
		if (Input.GetAxis ("Mouse ScrollWheel") < 0.0f) {
			this.ZoomOut ();
		} else if (Input.GetAxis ("Mouse ScrollWheel") > 0.0f) {
			this.ZoomIn ();
		}
	}
        
	/**
    * Reduce the distance from the camera to the target and
    * update the position of the camera (with the Rotate function).
    */
	public void ZoomIn ()
	{
		radiusValue -= zoomStep * Time.deltaTime * Constants.ZoomSpeedFactorGuiControl;
		radiusValue = Mathf.Clamp (radiusValue, Constants.radiusZoomMin, Constants.radiusZoomMax);
		radiusVector = new Vector3 (0, 0, -radiusValue);
		this.Rotate (xRot, yRot);
	}
	
	void OnGUI ()
	{

		if (GameManager.developperMode) {
	         
			GUI.Box (new Rect (0, 10, 300, Screen.height), "Info and Tweaking");
			
			XrotationSpeedFactor = GUI.HorizontalSlider (new Rect (0, 20, 200, 30), XrotationSpeedFactor, 0, 0.10f);
			
		}
		
	}
	
	/**
        * Increase the distance from the camera to the target and
        * update the position of the camera (with the Rotate function).
        */
	public void ZoomOut ()
	{
		radiusValue += zoomStep * Time.deltaTime * Constants.ZoomSpeedFactorGuiControl;
		radiusValue = Mathf.Clamp (radiusValue, Constants.radiusZoomMin, Constants.radiusZoomMax);
		radiusVector = new Vector3 (0, 0, -radiusValue);
		this.Rotate (xRot, yRot);
	}

	public float XRot {
		get {
			return this.xRot;
		}
		set {
			xRot = value;
		}
	}

	public float YRot {
		get {
			return this.yRot;
		}
		set {
			yRot = value;
		}
	}
	
	public Vector3 RadiusVector {
		get {
			return this.radiusVector;
		}
		set {
			radiusVector = value;
		}
	}
       
	public void resetPosition ()
	{
		xRot = Constants.defaultAngleOrbitalCamValue;
		yRot = Constants.defaultAngleOrbitalCamValue;
		radiusValue = Constants.defaultOrbitalCamRadiusValue;
	}
	
	public void setCamera (Camera camera)
	{
		this.camera = camera;
	}

	public void setTracker (Transform tracker)
	{
		this.tracker = tracker;
	}
        
	public Camera getCamera ()
	{
		return this.camera;
	}

	public Transform getTracker ()
	{
		return this.tracker;
	}
	
	public void OnGUICamController ()
	{
		if (GameManager.developperMode)
			showTweak ();
	}
	
	void showTweak ()
	{
		GUI.Label (new Rect (0, 60, 300, 100), "X rot factor: " + XrotationSpeedFactor.ToString ());
		XrotationSpeedFactor = GUI.HorizontalSlider (new Rect (0, 80, 300, 30), XrotationSpeedFactor, 0.0F, 0.5F);
		
		GUI.Label (new Rect (0, 100, 300, 100), "Y rot factor: " + YrotationSpeedFactor.ToString ());
		YrotationSpeedFactor = GUI.HorizontalSlider (new Rect (0, 120, 300, 30), YrotationSpeedFactor, 0.0F, 0.5F);
		
		GUI.Label (new Rect (0, 140, 300, 100), "zoomSpeedFactor: " + zoomSpeedFactor.ToString ());
		zoomSpeedFactor = GUI.HorizontalSlider (new Rect (0, 160, 300, 30), zoomSpeedFactor, 0.0F, 0.1F);
	}
        
}