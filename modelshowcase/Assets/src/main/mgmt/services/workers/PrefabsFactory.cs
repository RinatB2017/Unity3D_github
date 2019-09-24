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

public class PrefabsFactory : MonoBehaviour
{
	public GameObject makePrefabFromResource(string prefabResourceName) {
		
		UnityEngine.Object obj = Resources.Load("main/prefabs/" + prefabResourceName);
		
		if(obj != null) {
			return (GameObject) GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity);
		} else {
			return null;
		}
	}
	
	// Use this for initi  alization
	void Start ()
	{
		Debug.Log ("Starting " + this.GetType ());
	}
  
	// Update is called once per frame
	void Update ()
	{
  
	}
}
