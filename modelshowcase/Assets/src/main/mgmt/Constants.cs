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

public class Constants  {

	public static class PrefabNames {
		public const string cube = "prefab_01";
		public const string formula_one = "formula_one";
	}

	public const string prefabNameToDeploy = PrefabNames.formula_one;

	#region Orbital Camera constants
	/// <summary>
	/// Constant default orbital cam radius value.
	/// </summary>
	public const float defaultOrbitalCamRadiusValue = 4.0f;

	/// <summary>
	/// Constant default angle orbital cam value.
	/// </summary>
	public const float defaultAngleOrbitalCamValue = 10.0f;
	
	/// <summary>
	/// Constant radius zoom minimum.
	/// </summary>
	public const float radiusZoomMin = 3.0f;
	
	/// <summary>
	/// Constant radius zoom max.
	/// </summary>
	public const float radiusZoomMax = 18.0f;
	
	/// <summary>
	/// Constant xrotation speed factor.
	/// </summary>
	public const float XrotationSpeedFactor = 0.022f;
	
	/// <summary>
	/// Constant yrotation speed factor.
	/// </summary>
	public const float YrotationSpeedFactor = 0.022f;
	
	/// <summary>
	/// Constant xrotation speed factor GUI control.
	/// </summary>
	public const float XrotationSpeedFactorGuiControl = 45.0f;
	
	/// <summary>
	/// Constant yrotation speed factor GUI control.
	/// </summary>
	public const float YrotationSpeedFactorGuiControl = 45.0f;
	
	public const float ZoomSpeedFactorGuiControl = 5.0f;
	
	
	/// <summary>
	/// Constant zoom speed factor.
	/// </summary>
	public const float zoomSpeedFactor = 0.0957f;
	
	/// <summary>
	/// Constant yrotation angle limit.
	/// </summary>
	public const float YrotationAngleLimit = 89.99f;
	#endregion
	
	#region Inputs shortcuts
	
	public const int MOUSE_LEFT_CLICK = 0;
	
	#endregion
	
	#region Others
	public const bool developperMode = false;
	#endregion
}
