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

public class GUIManager : MonoBehaviour
{
    #region Attributes
	private Texture backImg;
	private Texture autoOffImg;
	private Texture autoOnImg;
	private bool displayButtons;
	private float offset = 40;
	
	
	private Texture crossArrow_up;
	private Texture crossArrow_down;
	private Texture crossArrow_left;
	private Texture crossArrow_right;
	private Texture zoomInImg;
	private Texture zoomOutImg;
	private Texture resetImg;
	private Texture fullScreen;
    #endregion
        
	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Starting " + this.GetType ());
		
		if (GameManager.tabletMode) {
			backImg = (Texture)Resources.Load ("main/images/back_1_x2");
			autoOffImg = (Texture)Resources.Load ("main/images/auto_off_1x2");
			autoOnImg = (Texture)Resources.Load ("main/images/auto_on_1x2");
			offset *= 2;
				
		} else {
			backImg = (Texture)Resources.Load ("main/images/back_1");
			autoOffImg = (Texture)Resources.Load ("main/images/auto_off_1");
			autoOnImg = (Texture)Resources.Load ("main/images/auto_on_1");
			
			crossArrow_up = (Texture)Resources.Load ("main/images/crossArrow_up");
			crossArrow_down = (Texture)Resources.Load ("main/images/crossArrow_down");
			crossArrow_left = (Texture)Resources.Load ("main/images/crossArrow_left");
			crossArrow_right = (Texture)Resources.Load ("main/images/crossArrow_right");
			
			zoomInImg = (Texture)Resources.Load ("main/images/zoomIn");
			zoomOutImg = (Texture)Resources.Load ("main/images/zoomOut");
			resetImg = (Texture)Resources.Load ("main/images/reset");
			fullScreen = (Texture)Resources.Load ("main/images/fullScreen");
		}	

		displayButtons = true;
	}
	
	void Update ()
	{
		
		if (Input.touchCount == 3) {
			
			bool toggleButtons = false;
			
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {
					toggleButtons = true;
				}
			}
			if (toggleButtons) {
				displayButtons = !displayButtons;
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			displayButtons = !displayButtons;
		}
		
	}
	
	void OnGUI ()
	{
		
		if (GameManager.Instance.Message != "") { // Check for game errors
			float x = 300;
			float y = 100;
			
			if (GUI.Button (new Rect ((Screen.width - x) / 2, (Screen.height - y) / 2, x, y), GameManager.Instance.Message + "\n\nPress button to open the main app.")) {
				GameManager.Instance.Message = "";
				Debug.Log ("Back to main app");	
				Application.OpenURL ("euronaval2012://");
			}
			
		} else { // Display game GUI
			
			if (GameManager.developperMode) {
				showFps ();
			}
		
			if (displayButtons) {
				
				if (GameManager.tabletMode && GameManager.ipadTabletVersion != 0) {
					handleBackToMainApp ();
				}
				
				handleAutoMode ();
				
				
				if (!GameManager.tabletMode) {
					handleCrossInputGui ();
				}
			}
		}
	}
    
	void handleCrossInputGui ()
	{
		float imgH = 60.0f;
		float imgW = 60.0f;
		float paddingLeft = 10;
		float paddingBottom = 0;
		
		// Cross
		GUI.BeginGroup (new Rect (paddingLeft, Screen.height - imgH * 3 - paddingBottom, imgH * 3, imgW * 3));
		bool pressedUp = GUI.RepeatButton (new Rect (imgW, 0, imgH, imgW), crossArrow_up, new GUIStyle ());
		bool pressedDown = GUI.RepeatButton (new Rect (imgW, imgH * 2, imgH, imgW), crossArrow_down, new GUIStyle ());
		bool pressedLeft = GUI.RepeatButton (new Rect (0, imgH, imgH, imgW), crossArrow_left, new GUIStyle ());
		bool pressedRight = GUI.RepeatButton (new Rect (imgW * 2, imgH, imgH, imgW), crossArrow_right, new GUIStyle ());
		
		// Zoom
		bool pressedZoomIn = GUI.RepeatButton (new Rect (0, 0, imgH, imgW), zoomInImg,  new GUIStyle ());
		bool pressedZoomOut = GUI.RepeatButton (new Rect (0, imgH * 2, imgH, imgW), zoomOutImg,  new GUIStyle ());
		
		// Reset
		bool pressedReset = GUI.RepeatButton (new Rect (imgW, imgH, imgH, imgW), resetImg,  new GUIStyle ());
		
		GUI.EndGroup ();
		
		if (pressedUp) { 
			handleCrossUpGUI ();
		}
		if (pressedDown) { 
			handleCrossDownGUI ();
		}
		
		if (pressedLeft) { 
			handleCrossLeftGUI ();
		}
		
		if (pressedRight) { 
			handleCrossRightGUI ();
		}
		
		if (pressedZoomIn) { 
			handleZoomInGUI ();
		}
		
		if (pressedZoomOut) { 
			handleZoomOutGUI ();
		}
		if (pressedReset) { 
			handleResetGUI ();
		}
	}


	void handleCrossUpGUI ()
	{
		GameManager.Instance.CameraManager.currentCam.RotateControls (new Vector2 (0, -1));
	}
	
	void handleCrossDownGUI ()
	{
		GameManager.Instance.CameraManager.currentCam.RotateControls (new Vector2 (0, 1));
	}

	void handleCrossLeftGUI ()
	{
		GameManager.Instance.CameraManager.currentCam.RotateControls (new Vector2 (-1, 0));
	}

	void handleCrossRightGUI ()
	{
		GameManager.Instance.CameraManager.currentCam.RotateControls (new Vector2 (1, 0));
	}
	
	void handleZoomInGUI ()
	{
		GameManager.Instance.CameraManager.currentCam.ZoomIn ();
	}

	void handleZoomOutGUI ()
	{
		GameManager.Instance.CameraManager.currentCam.ZoomOut ();
	}

	void handleResetGUI ()
	{
		GameManager.Instance.CameraManager.currentCam.resetPosition ();
	}
        
	/// <summary>
	/// Handles the back to main app.
	/// </summary>
	void handleBackToMainApp ()
	{
         
		float imgH = backImg.height / 1;
		float imgW = backImg.width / 1;

		if (GUI.Button (new Rect (Screen.width - imgW,
                                  	Screen.height - imgH - offset, imgW, imgH), 
									backImg, 
									new GUIStyle ())) {
			
			Application.OpenURL ("euronaval2012://");
			//Application.OpenURL ("com.dcns.Sea-the-Future://");
			Debug.Log ("Back to main app");
			GameManager.Instance.CameraManager.currentCam.resetPosition ();
			
		}
	}

	void handleAutoMode ()
	{
		
		Texture currentAutoImg = (GameManager.Instance.CameraManager.CurrentCamState == CameraManager.CameraState.Orbital) ? autoOffImg : autoOnImg;
		
		float imgH = currentAutoImg.height / 1;
		float imgW = currentAutoImg.width / 1;
		
		float marginLeft = imgW;
		
		if (GUI.Button (new Rect (Screen.width - marginLeft,
	                            Screen.height - imgH * 2 - 1 - offset, imgW, imgH), 
								currentAutoImg, 
								new GUIStyle ())) {
		
			GameManager.Instance.CameraManager.SwitchNextCamera ();
		
		}
		
		
		if(!GameManager.tabletMode) {
			if (GUI.Button (new Rect (Screen.width - marginLeft,
	                            Screen.height - imgH * 2 + 10, imgW, imgH), 
								fullScreen, new GUIStyle ())) {
				Screen.fullScreen = !Screen.fullScreen;
			}
		}
	}

	void showFps ()
	{
		GUI.Label (new Rect (80, 10, 60, 100), "fps: " + 1 / Time.deltaTime);
	}
}
