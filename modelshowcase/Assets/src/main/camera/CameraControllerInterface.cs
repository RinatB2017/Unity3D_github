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

public interface CameraControllerInterface
{
	void InitCam ();

	void LateUpdateCam ();

	void setCamera (Camera camera);

	void setTracker (Transform tracker);
	
	Camera getCamera ();

	Transform getTracker ();
	
	void OnGUICamController ();
	
	void resetPosition ();
	
	void ZoomOut ();
	
	void ZoomIn ();
	
	void RotateControls (Vector2 v2);

}