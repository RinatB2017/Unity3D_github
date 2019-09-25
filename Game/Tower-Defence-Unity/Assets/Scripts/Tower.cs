using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float attackRadius;
    [SerializeField]  private Projectile projectile;

    private Enemy targetEnemy;
    private float attackCounter;
    private bool isAttacking;

	void Update () {
        attackCounter -= Time.deltaTime;
        if(targetEnemy == null || targetEnemy.IsDead) {
            Enemy nearestEnemy = GetNearestEnemyInRange();
            if(nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius) {
                targetEnemy = nearestEnemy;
            }
        }
        else {
            if(attackCounter <= 0) {
                isAttacking = true;
                attackCounter = timeBetweenAttacks; //reset attack counter
            }
            else {
                isAttacking = false;
            }

            if(Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius) {
                targetEnemy = null;
            }
        }
	}

    void FixedUpdate() {
        if(isAttacking) {
            Attack();
        }
    }

    private void Attack() {
        isAttacking = false;
        Projectile newProj = Instantiate(projectile) as Projectile;
        newProj.transform.localPosition = transform.localPosition;
        switch(newProj.ProType) {
        case ProjectileType.Arrow:
        GameManager.Instance.SoundFx.PlayOneShot(SoundManager.Instance.Arrow);
        break;

        case ProjectileType.Fireball:
        GameManager.Instance.SoundFx.PlayOneShot(SoundManager.Instance.Fireball);
        break;

        case ProjectileType.Rock:
        GameManager.Instance.SoundFx.PlayOneShot(SoundManager.Instance.Rock);
        break;
        }
        if(targetEnemy == null) {
            Destroy(newProj);
        }
        else {
            StartCoroutine(ShootProjectile(newProj));
        }
    }

    private IEnumerator ShootProjectile(Projectile projectile) {
        while(GetTargetDistance(targetEnemy) > 0.20f && projectile != null && targetEnemy != null) {
            var dir = targetEnemy.transform.localPosition - transform.localPosition;
            float angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition,
                targetEnemy.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }
        if(projectile != null || targetEnemy == null) {
            Destroy(projectile); 
        }
    }

    private float GetTargetDistance(Enemy enemy) {
        if(enemy == null) {
            enemy = GetNearestEnemyInRange();
            if(enemy == null) return 0f;
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, enemy.transform.localPosition));
    }

    private List<Enemy> GetEnemiesInRange() {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach(Enemy enemy in GameManager.Instance.enemyList) {
            if(Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius) {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }

    private Enemy GetNearestEnemyInRange() {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;    //Initalize it to INFINITY
        foreach(Enemy enemy in GetEnemiesInRange()) {
            if(Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistance) {
                smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
