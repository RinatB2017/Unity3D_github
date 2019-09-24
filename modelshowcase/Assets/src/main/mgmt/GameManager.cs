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
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
	
	#region Developper Zone
	public static bool developperMode = Constants.developperMode;
	public static bool tabletMode = true;
	public static int ipadTabletVersion = 0;
	#endregion
	
	#region Static variables
	public static GameManager Instance { get; set; } // Provide public static access of GameManager 
	public static string Lang { get; set; }
	#endregion

	#region Attributes
	public Services Services { get; set; }

	public CameraManager CameraManager { get; set; }

	public string prefabToLoad { get; set; }

	public GameObject ShipPrefabInShowRoom { get; set; }

	public string Message { get; set; }
	#endregion

	void Awake ()
	{
		
		DontDestroyOnLoad (this); // keep it running along scenes
		
		// Define build type tableMode equals true by default
		// if (Application.isEditor || Application.isWebPlayer) {
		if (Application.isEditor) {
			tabletMode = false;
		}
		
		if(tabletMode) {
			
			Debug.Log("We are in tablet mode with plateform: " +  Application.platform.ToString());
			
			if(Application.platform.Equals(RuntimePlatform.IPhonePlayer)){ // ipad or iphone
				// Getting iPad platform
				ipadTabletVersion = this.getIpadPlatform();
				Debug.Log("ipadTabletVersion is " + ipadTabletVersion);
			}
			
		}
		
		if (Instance != null) {
			throw new UnityException ("GameManager Instance already exist !");
		} else {
			Instance = this;        
		}
		
		// Setup Services link
		this.Services = GetComponent<Services> ();
    
		// Setup CameraManager link
		this.CameraManager = GetComponent<CameraManager> ();
		
		this.Message = "";

	}

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Starting " + this.GetType ());

		this.prefabToLoad = Constants.prefabNameToDeploy;
		
		setupPrefabInShowRoom (this.prefabToLoad);
	}

	// Update is called once per frame
	void Update ()
	{
		if (tabletMode && ipadTabletVersion != 0) {
			if (doesPrefabChange ()) { // Verify if the ship prefab prefs has change
			
				this.prefabToLoad = PlayerPrefs.GetString ("navire");
			
				// Changes appends from from navire prefs. Camera tracking must be reset
				updatePrefabInShowRoom (this.prefabToLoad);
			}
		}
		
		// check if AutoOrbital/orbital toggle
		if (!GameManager.tabletMode) {
			if (Input.GetKeyDown (KeyCode.A)) {
				GameManager.Instance.CameraManager.SwitchNextCamera ();	
			}
		}

		// Back android button
		if(Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
	}
	
	bool doesPrefabChange ()
	{
		// Retreive preferences from App prefs and compare
		string shipOnFlyPref = PlayerPrefs.GetString ("navire");
		return (! shipOnFlyPref.Equals (this.prefabToLoad));
	}

	public void setupPrefabInShowRoom (string prefabToload)
	{
	
		Debug.Log ("Trying to load prefab called: " + prefabToload);

			
			// Instanciate new game object from prefab
			ShipPrefabInShowRoom = this.Services.PrefabsFactory.makePrefabFromResource (prefabToload);
			
			// Check for instantiation OK
			if (ShipPrefabInShowRoom != null) {
				setupCameraManagementOnGameObject (ShipPrefabInShowRoom); // Setup camera management on gameObject Tracker
			} else {
				Debug.Log ("Unable to load prefab using preference: " + prefabToload + ". Does prefab exist in unity project?");	
				this.Message = "Ship selected do not exist into that 3D application";
			}
	}
	
	public void updatePrefabInShowRoom (string prefabToload)
	{
		
		Debug.Log ("Trying to update in showroom prefab called: " + prefabToload);
		
		if (prefabToload != "") {
			
			// Remove previous GameObject ship
			Destroy (ShipPrefabInShowRoom);
			
			// Instanciate new game object from prefab
			ShipPrefabInShowRoom = this.Services.PrefabsFactory.makePrefabFromResource (prefabToload);
			
			if (ShipPrefabInShowRoom != null) {
				
				// Update camera tracking on this new GameObject
				updateCameraManagementOnGameObject (ShipPrefabInShowRoom); // Setup camera management on gameObject Tracker
				
			} else {
				Debug.Log ("Unable to load prefab using preference: " + prefabToload + ". Does prefab exist in unity project?");	
				this.Message = "Ship selected do not exist into that 3D application";
			}
		} else {
			Debug.Log ("Navire preferences seems to be empty, pref value: " + prefabToload);
			this.Message = "You must use main app at first.";
		}
		
	}
	
	public void setupCameraManagementOnGameObject (GameObject go)
	{
		this.CameraManager.SetCameraManagementOnGameObject (go);	
	}
	
	public void updateCameraManagementOnGameObject (GameObject go)
	{
		this.CameraManager.updateCameraManagementOnGameObject (go);	
	}

	public int getIpadPlatform() {
			
		if(tabletMode) {
			return Services.iPadPlateform();
		}else {
			return 0;	
		}
	}
}
