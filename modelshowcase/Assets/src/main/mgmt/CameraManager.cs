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

using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
        
	public enum CameraState
	{
		Orbital,
		AutoOrbital
	}

	public static CameraManager Instance { get; set; } // Provide public static access
	public Transform cameraTracker;

	public CameraState CurrentCamState { get; set; }
        
	public CameraControllerInterface currentCam;
	public AutoOrbitalCameraControllerImpl autoOrbitalCamRef;
	public OrbitalCameraControllerImpl orbitalCamRef;
        
	void Awake ()
	{
                
		// Setup self instance
		if (Instance != null) {
			throw new UnityException ("CameraManager Instance already exist !");
		} else {
			Instance = this;        
		}
                
	}
	
	public void SetCameraManagementOnGameObject (GameObject go) {
		
		// Get the camera tracker transform and keep reference
		cameraTracker = go.transform;
		
		if (cameraTracker == null) {
			string errorMessage = "No cameraTracker founded. Add one under that GameObject !";
			Debug.LogError (errorMessage);
			throw new UnityException (errorMessage);
		}
                
		// Setup orbital camera reference
		orbitalCamRef = ScriptableObject.CreateInstance<OrbitalCameraControllerImpl> ();
		orbitalCamRef.setCamera (Camera.main.GetComponent<Camera>());
		orbitalCamRef.setTracker (cameraTracker);
		orbitalCamRef.InitCam ();
        
		// Setup normal camera reference (Not used currently in that project)
		autoOrbitalCamRef = ScriptableObject.CreateInstance<AutoOrbitalCameraControllerImpl> ();
		autoOrbitalCamRef.setCamera (Camera.main.GetComponent<Camera>());
		autoOrbitalCamRef.setTracker (cameraTracker);
		autoOrbitalCamRef.InitCam ();
		
	}
	
	public void updateCameraManagementOnGameObject (GameObject go) {
		
		// Get the camera tracker transform and keep reference
		cameraTracker = go.transform;
		
		if (cameraTracker == null) {
			string errorMessage = "No cameraTracker founded. Add one under that GameObject !";
			Debug.LogError (errorMessage);
			throw new UnityException (errorMessage);
		}
                
		// Update orbital camera reference
		orbitalCamRef.setCamera (Camera.main.GetComponent<Camera>());
		orbitalCamRef.setTracker (cameraTracker);
		orbitalCamRef.InitCam ();
        
		// Update normal camera reference (Not used currently in that project)
		autoOrbitalCamRef.setCamera (Camera.main.GetComponent<Camera>());
		autoOrbitalCamRef.setTracker (cameraTracker);
		autoOrbitalCamRef.InitCam ();
		
	}
	
	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Starting " + this.GetType ());
		CurrentCamState = CameraState.Orbital;
		currentCam = orbitalCamRef;
	}
        
	void LateUpdate ()
	{
		if(GameManager.Instance.ShipPrefabInShowRoom != null) {
			currentCam.LateUpdateCam ();
		}
	}
        
	public void SwitchNextCamera ()
	{
                
		Debug.Log ("Camera Switch");
                
		switch (CurrentCamState) {
                        
		case CameraState.AutoOrbital:
			
			// Change cam state
			CurrentCamState = CameraState.Orbital;
			
			// Update camera position from previous mode, fluid transition
			orbitalCamRef.XRot = autoOrbitalCamRef.XRot;
			orbitalCamRef.YRot = autoOrbitalCamRef.YRot;
			orbitalCamRef.RadiusVector = autoOrbitalCamRef.RadiusVector;
			
			// Change used current camera
			currentCam = orbitalCamRef;	
			break;
                        
		case CameraState.Orbital:
			
			// Change cam state
			CurrentCamState = CameraState.AutoOrbital;
			
			// Update camera position from previous mode, fluid transition
			autoOrbitalCamRef.XRot = orbitalCamRef.XRot;
			autoOrbitalCamRef.YRot = orbitalCamRef.YRot;
			autoOrbitalCamRef.RadiusVector = orbitalCamRef.RadiusVector;
			
			// Change used current camera
			currentCam = autoOrbitalCamRef;
			break;  
                        
		}
                
	}
	
	void OnGUI () {
		if(GameManager.Instance.ShipPrefabInShowRoom != null) {
			currentCam.OnGUICamController();	
		}
	}

}