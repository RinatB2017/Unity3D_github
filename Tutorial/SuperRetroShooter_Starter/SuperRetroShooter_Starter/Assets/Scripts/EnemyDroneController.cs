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

public class EnemyDroneController : MonoBehaviour 
{
	public GameObject powerUp;
	public GameObject explosion;
	public GameObject smallEnemyBullet;
	public float minReloadTime = 1.0f;
	public float maxReloadTime = 2.0f;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine("Shoot");
	}

	IEnumerator Shoot() 
  	{
    	yield return new WaitForSeconds((Random.Range(minReloadTime, maxReloadTime)));
    	while (true) 
		{
			Instantiate(smallEnemyBullet, gameObject.transform.position, gameObject.transform.rotation);
      		yield return new WaitForSeconds((Random.Range(minReloadTime, maxReloadTime)));
    	}
  	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Boundary" && other.gameObject.name != "Top Boundary") 
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Player Bullet") 
		{
			GameController.SharedInstance.IncrementScore(100);
			float randomNumber = Random.Range(0.0f, 10.0f);
			if (randomNumber > 9.0f) 
			{
				Instantiate(powerUp, gameObject.transform.position, gameObject.transform.rotation);
			}
			Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
