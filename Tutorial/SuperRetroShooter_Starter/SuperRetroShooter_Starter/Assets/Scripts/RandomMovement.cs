/*
 * Copyright (c) 2015 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour 
{
	public float moveSpeed = 5.0f;

	private float maxX;
	private float minX;
	private float maxY;
	private float minY;

	private float tChange = 0.0f; // force new direction in the first Update
	private float randomX;
	private float randomY;

	void Start () 
	{
    	maxX = GameController.SharedInstance.rightBoundary.transform.position.x;
    	minX = GameController.SharedInstance.leftBoundary.transform.position.x;
    	maxY = GameController.SharedInstance.topBoundary.transform.position.y;
    	minY = GameController.SharedInstance.bottomBoundary.transform.position.y;
  	}

	void Update() 
	{
		// change to a new random direction at random intervals
		if (Time.time >= tChange) 
		{
			randomX = Random.Range(-2.0f, 2.0f);
			randomY = Random.Range(-2.0f, 2.0f); //  between -2.0 and 2.0 is returned
			// set a random interval between 0.5 and 1.5
			tChange = Time.time + Random.Range(0.5f,1.5f);
		}
		Vector3 newPosition = new Vector3(randomX, randomY, 0);
		transform.Translate(newPosition * moveSpeed * Time.deltaTime);
		// if any boundary is hit, change direction.
		if (transform.position.x >= maxX || transform.position.x <= minX) 
		{
			randomX = -randomX;
		}
		if (transform.position.y >= maxY || transform.position.y <= minY) 
		{
			randomY = -randomY;
		}
		Vector3 clampedPosition = transform.position;
		clampedPosition.x = Mathf.Clamp(transform.position.x, minX, maxX);
		clampedPosition.y = Mathf.Clamp(transform.position.y, minY, maxY);
		transform.position = clampedPosition;
	}
}
