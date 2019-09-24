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

using System;
using UnityEngine;

/// <summary>
/// Game services is the entry point for accessing all external services
/// Such as OptionsManager, WebService manager, ...
/// </summary>
public class Services : MonoBehaviour
{
        
        #region Attributes
	public GUIManager GuiManager { get; set; }

	public PrefabsFactory PrefabsFactory { get; set; }
        #endregion
        
	// Use this for initialization
	void Awake ()
	{
		
		this.GuiManager = GetComponent<GUIManager> ();
		if (this.GuiManager == null) 
			Debug.LogError ("GUIManager well linked to service?");
		
		this.PrefabsFactory = GetComponent<PrefabsFactory> ();
		if (this.PrefabsFactory == null) 
			Debug.LogError ("PrefabsFactory well linked to service?");
	}
	
	void Start ()
	{
    	Debug.Log("Starting " + this.GetType());
	}
        
	
	public static int iPadPlateform() {
		
		Debug.Log("iPadPlateform() call. width: " + Screen.width + ", height: " + Screen.height + ", dpi: " + Screen.dpi);
		
		if(Screen.width == 2048){
			return 3;
		}else {
			return 2;	
		}
	
	}
}
 