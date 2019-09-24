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

public class AutoOrbitalCameraControllerImpl : OrbitalCameraControllerImpl
{
	float timeRefOnShipDrag = 0;
	float deadTimeOnFingerRelease = 1.0f;
	
	protected override void autoRotation () {
		
		if (Input.touchCount == 1)  {
			timeRefOnShipDrag = Time.time;	
		}
		
		if (Input.touchCount == 0) {
			
			if(Time.time > timeRefOnShipDrag + deadTimeOnFingerRelease) 
				xRot += Time.deltaTime * 7;		
		}
	}
        
}