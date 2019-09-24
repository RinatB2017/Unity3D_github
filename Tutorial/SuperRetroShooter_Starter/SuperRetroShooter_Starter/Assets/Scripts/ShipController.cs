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
using System.Collections.Generic;

public class ShipController : MonoBehaviour 
{
	public float moveSpeed = 10.0f;						// The players movement speed
	public float scatterShotTurretReloadTime = 2.0f;	// Reload time for the scatter shot turret!
	public GameObject startWeapon;						// The players initial 'turret' gameobject
	public List<GameObject> tripleShotTurrets;			//
	public List<GameObject> wideShotTurrets;			// References to the upgrade weapon turrets
	public List<GameObject> scatterShotTurrets;			//
	public List<GameObject> activePlayerTurrets;		//
	public GameObject explosion;						// Reference to the Expolsion prefab
	public GameObject playerBullet;						// Reference to the players bullet prefab
  	public ParticleSystem playerThrust;             	// The particle effect for the ships thruster
	private Rigidbody2D playerRigidbody;				// The players rigidbody: Required to apply directional force to move the player
	private Renderer playerRenderer;					// The Renderer for the players ship sprite
	private CircleCollider2D playerCollider;			// The Players ship collider

  	public int upgradeState = 0;						// A reference to the upgrade state of the player

  	private GameObject leftBoundary;                   	//
  	private GameObject rightBoundary;                  	// References to the screen bounds: Used to ensure the player
  	private GameObject topBoundary;                    	// is not able to leave the screen.
  	private GameObject bottomBoundary;                 	//

	private AudioSource shootSoundFX;					// The player shooting sound effect

	void Start () 
	{
    	leftBoundary = GameController.SharedInstance.leftBoundary;
    	rightBoundary = GameController.SharedInstance.rightBoundary;
    	topBoundary = GameController.SharedInstance.topBoundary;
    	bottomBoundary = GameController.SharedInstance.bottomBoundary;

		playerCollider = gameObject.GetComponent<CircleCollider2D>();
		playerRenderer = gameObject.GetComponent<Renderer>();
		activePlayerTurrets = new List<GameObject>();
		activePlayerTurrets.Add(startWeapon);
		shootSoundFX = gameObject.GetComponent<AudioSource>();
		playerRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
	{
		if (Input.GetKeyDown("space")) 
		{
    		Shoot();
		}
		float xDir = Input.GetAxis("Horizontal");
		float yDir = Input.GetAxis("Vertical");
		playerRigidbody.velocity = new Vector2(xDir * moveSpeed, yDir * moveSpeed);
		playerRigidbody.position = new Vector2
		(
			Mathf.Clamp (playerRigidbody.position.x, leftBoundary.transform.position.x, rightBoundary.transform.position.x),
			Mathf.Clamp (playerRigidbody.position.y, bottomBoundary.transform.position.y, topBoundary.transform.position.y)
		);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Powerup") 
		{
			CollectPowerup powerupScript = other.gameObject.GetComponent<CollectPowerup>();
			powerupScript.PowerupCollected();
			UpgradeWeapons();
		} 
    	else if (other.gameObject.tag == "Enemy Ship 1" || other.gameObject.tag == "Enemy Ship 2" || other.gameObject.tag == "Enemy Laser") 
		{
      		GameController.SharedInstance.ShowGameOver();	// If the player is hit by an enemy ship or laser it's game over.
			playerRenderer.enabled = false;       			// We can't destroy the player game object straight away or any code from this point on will not be executed
			playerCollider.enabled = false;       			// We turn off the players renderer so the player is not longer displayed and turn off the players collider
      		playerThrust.Stop();
			Instantiate(explosion, transform.position, transform.rotation);   // Then we Instantiate the explosions... one at the centre and some additional around the players location for a bigger bang!
			for (int i = 0 ; i < 8; i++) 
			{
				Vector3 randomOffset = new Vector3 (transform.position.x + Random.Range(-0.6f, 0.6f), transform.position.y + Random.Range(-0.6f, 0.6f), 0.0f); 
				Instantiate(explosion, randomOffset, transform.rotation);
			}
			Destroy(gameObject, 1.0f); // The second parameter in Destroy is a delay to make sure we have finished exploding before we remove the player from the scene.
		}
	}

  	void Shoot() 
  	{
  		foreach(GameObject turret in activePlayerTurrets) 
		{
      		Instantiate(playerBullet, turret.transform.position, turret.transform.rotation);
    	}
    	shootSoundFX.Play();
  	}

	void UpgradeWeapons() 
	{   
    	// We check the upgrade state of the player and add the appropriate additional turret gameobjects to the active turrets List.

		if (upgradeState == 0) 
		{
			foreach(GameObject turret in tripleShotTurrets) 
			{
				activePlayerTurrets.Add(turret);
			}
		} 
    	else if (upgradeState == 1) 
		{
			foreach(GameObject turret in wideShotTurrets) 
			{
				activePlayerTurrets.Add(turret);
			}
		} 
    	else if (upgradeState == 2) 
		{
			StartCoroutine("ActivateScatterShotTurret");
    	} 
    	else 
		{
      		return;
    	}
		upgradeState ++;
	}

    IEnumerator ActivateScatterShotTurret() 
	{
    	// The ScatterShot turret is shot independantly of the spacebar
    	// This Coroutine shoots the scatteshot at a reload interval

		while (true) 
		{
			foreach(GameObject turret in scatterShotTurrets) 
			{
				Instantiate(playerBullet, turret.transform.position, turret.transform.rotation);
			}
			shootSoundFX.Play();
			yield return new WaitForSeconds(scatterShotTurretReloadTime);
		}
	}
}
