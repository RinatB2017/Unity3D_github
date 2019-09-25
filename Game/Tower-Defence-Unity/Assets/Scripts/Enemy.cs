using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField] private int target = 0;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float navigationUpdate;
    [SerializeField] private int healthPoints;
    [SerializeField] private int rewardAmount;

    private Transform enemy;
    private Collider2D enemyCollider;
    private float navigationTime = 0;
    private bool isDead;
    private Animator anim;

    public bool IsDead {
        get {
            return isDead;
        }
    }

    private void Start () {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        GameManager.Instance.RegisterEnemy(this);
	}
	
	private void Update () {
		if(wayPoints != null && !isDead) {
            navigationTime += Time.deltaTime;
            if(navigationTime > navigationUpdate) {
                if(target < wayPoints.Length) {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                }
                else {
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
                }
                navigationTime = 0;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "checkpoint") {
            target += 1;
        }
        else if(other.tag == "Finish") {
            GameManager.Instance.RoundEscaped += 1;
            GameManager.Instance.TotalEscaped += 1;
            GameManager.Instance.UnregisterEnemy(this);
            GameManager.Instance.IsWaveOver();  //check if game finished, etc
        }
        else if(other.tag == "Projectile") {
            Projectile newProj = other.gameObject.GetComponent<Projectile>();
            EnemyHit(newProj.AttackStrength);
            Destroy(other.gameObject);
        }
    }

    private void EnemyHit(int hitPoints) {
        if(healthPoints - hitPoints > 0) {
            healthPoints -= hitPoints;
            GameManager.Instance.SoundFx.PlayOneShot(SoundManager.Instance.Hit);
            anim.Play("Hurt");  //call hurt animation
        }
        else {
            Die(); //enemy should die
        }
    }

    private void Die() {
        anim.SetTrigger("didDie");  //call die animation
        isDead = true;
        enemyCollider.enabled = false;
        GameManager.Instance.TotalKilled += 1;
        GameManager.Instance.SoundFx.PlayOneShot(SoundManager.Instance.Death);
        GameManager.Instance.AddMoney(rewardAmount);
        GameManager.Instance.IsWaveOver(); //check if game finished, etc
    }
}
