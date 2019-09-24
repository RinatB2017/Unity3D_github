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
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour 
{
	public static GameController SharedInstance;

	public Text scoreLabel;
	private int currentScore = 0;
	public Text gameOverLabel;
	public Button restartGameButton;

	public GameObject enemyType1;
	public GameObject enemyType2;

	public GameObject subBoss;

	public float startWait = 1.0f;
	public float waveInterval = 2.0f;
	public float spawnInterval = 0.5f;
	public int enemiesPerWave = 5;

  	public GameObject leftBoundary;                   //
  	public GameObject rightBoundary;                  // References to the screen bounds: Used to ensure the player
  	public GameObject topBoundary;                    // is not able to leave the screen.
  	public GameObject bottomBoundary;                 //

	void Awake () 
	{
		SharedInstance = this;
	}

	void Start () 
	{
		StartCoroutine(SpawnEnemyWaves());
	}

	IEnumerator SpawnEnemyWaves () 
	{
		yield return new WaitForSeconds (startWait);
		while (true) 
		{
			float waveType = Random.Range(0.0f, 10.0f);
			for (int i = 0 ; i < enemiesPerWave; i++) 
			{
				Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight + 2, 0));
				Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight + 2, 0));
				Vector3 spawnPosition = new Vector3 (Random.Range(topLeft.x, topRight.x), topLeft.y, 0);
				Quaternion spawnRotation = Quaternion.Euler(0, 0, 180);
				if (waveType >= 5.0f) 
				{
					Instantiate(enemyType1, spawnPosition, spawnRotation);
				}
				else
				{
					Instantiate(enemyType2, spawnPosition, spawnRotation);
				} 
        	yield return new WaitForSeconds (spawnInterval);
			} 
      		yield return new WaitForSeconds (waveInterval);
		}
	}

	public void IncrementScore(int increment) 
	{
		currentScore += increment;
		scoreLabel.text = "Score: " + currentScore;
	}

	public void ShowGameOver() 
	{
		gameOverLabel.rectTransform.anchoredPosition3D = new Vector3 (0, 0, 0);
		restartGameButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (0, -50, 0);
	}

	public void RestartGame() 
	{
		SceneManager.LoadScene("GameScene");
	}
}
