using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus { Next, Play, Gameover, Win }

public class GameManager: Singleton<GameManager> {
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private int totalEnemies;
    [SerializeField] private int enemiesPerSpawn;

    private const float spawnDelay = 0.5f;

    [SerializeField] private int totalWaves = 10;
    [SerializeField] private Text totalMoneyLbl;
    [SerializeField] private Text currentWaveLbl;
    [SerializeField] private Text totalEscapedLbl;
    [SerializeField] private Text playBtnLbl;
    [SerializeField] private Button playBtn;
    private int currentWave;
    private int totalMoney = 10;
    public int TotalEscaped { get; set; }
    public int RoundEscaped { get; set; }
    public int TotalKilled { get; set; }
    private int currentTypeOfEnemies;
    private GameStatus currentState = GameStatus.Play;
    private AudioSource soundFx;
    private int typeOfEnemies;

    public int TotalMoney {
        get { return totalMoney; }
        set {
            totalMoney = value;
            totalMoneyLbl.text = totalMoney.ToString();
        }
    }

    public AudioSource SoundFx { get { return soundFx; } }

    public List<Enemy> enemyList = new List<Enemy>();

    private void Start() {
        //StartCoroutine(Spawn());
        playBtn.gameObject.SetActive(false);
        soundFx = GetComponent<AudioSource>();
        ShowMenu();
    }

    private void Update() {
        HandleEscape();
    }

    IEnumerator Spawn() {
        if(enemiesPerSpawn > 0 && enemyList.Count < totalEnemies) {
            for(int i = 0; i < enemiesPerSpawn; ++i) {
                if(enemyList.Count < totalEnemies) {
                    Enemy newEnemy;
                    if(typeOfEnemies < enemies.Length - 1)
                        newEnemy = Instantiate(enemies[typeOfEnemies]);
                    else
                        newEnemy = Instantiate(enemies[Random.Range(0, typeOfEnemies)]);
                    newEnemy.transform.position = spawnPoint.transform.position;
                    newEnemy.name = "enemy, num: " + enemyList.Count;
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    private void ShowMenu() {
        switch(currentState) {
            case GameStatus.Next:
                playBtnLbl.text = "Next Wave";
                break;

            case GameStatus.Play:
                playBtnLbl.text = "Play";
                break;

            case GameStatus.Gameover:
                playBtnLbl.text = "Play Again!";
                GameManager.Instance.SoundFx.PlayOneShot(SoundManager.Instance.Gameover);
                break;

            case GameStatus.Win:
                playBtnLbl.text = "Play";
                //Add win sounds
                break;
        }

        playBtn.gameObject.SetActive(true);
    }

    public void PlayButtonPressed() {
        switch(currentState) {
            case GameStatus.Next:
                currentWave += 1;
                if(currentWave <= enemies.Length) {
                    typeOfEnemies = currentWave;
                }
                totalEnemies += currentWave;
                break;

            default:
                totalEnemies = 3;
                TotalEscaped = 0;
                TotalMoney = 10;
                typeOfEnemies = 0;
                TowerManager.Instance.DestroyAllTowers();
                TowerManager.Instance.RenameTagsBuildSites();
                totalMoneyLbl.text = TotalMoney.ToString();
                totalEscapedLbl.text = "Escaped " + TotalEscaped + "/10";
                soundFx.PlayOneShot(SoundManager.Instance.Newgame);
                break;
        }
        DestroyAllEnemies();
        TotalKilled = RoundEscaped = 0;
        currentWaveLbl.text = "Wave " + (currentWave + 1);
        StartCoroutine(Spawn());
        playBtn.gameObject.SetActive(false);
    }

    private void HandleEscape() {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1)) {
            TowerManager.Instance.DisableDragSprite();
            TowerManager.Instance.TowerBtnPressed = null;
        }
    }

    public void RegisterEnemy(Enemy enemy) {
        enemyList.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy) {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies() {
        foreach(Enemy enemy in enemyList) {
            Destroy(enemy.gameObject);
        }
        enemyList.Clear();
    }

    public void AddMoney(int amount) {
        TotalMoney += amount;
    }

    public void SubtractMoney(int amount) {
        TotalMoney -= amount;
    }

    public void IsWaveOver() {
        totalEscapedLbl.text = "Escaped " + TotalEscaped + "/10";
        if(RoundEscaped + TotalKilled == totalEnemies) {
            SetCurrentGameState();
            ShowMenu();
        }
    }

    private void SetCurrentGameState() {
        if(TotalEscaped >= 10) {
            currentState = GameStatus.Gameover;
        }
        else if(currentWave == 0 && RoundEscaped + TotalKilled == 0) {
            currentState = GameStatus.Play;
        }
        else if(currentWave >= totalWaves) {
            currentState = GameStatus.Win;
        }
        else {
            currentState = GameStatus.Next;
        }
    }
}