using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private float diffInEnemyDistance = 1.2f;
    private Transform allEnemiesOnScreen;
    private float moveVelocity;
    private Vector3 differencePosition;
    private bool checkSpriteFlipped;
    private float attackDistance = 0.1f;
    private float health = 5;
    public event EventHandler OnTakingDamage;
    private bool isDead = false;
    private float onDeadTimer = 2f;


    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Player.Instance != null)
        {
            if (!isDead && !Player.Instance.ReturnIsDead())
            {
                differencePosition = Player.Instance.transform.position - transform.position;
                if (Vector3.Distance(Player.Instance.transform.position, transform.position) > attackDistance) // && !enemyVisual.ReturnAttackInProgress()
                { transform.position += differencePosition.normalized * Time.deltaTime * moveVelocity; }
                if (allEnemiesOnScreen != null)
                {
                    foreach (Transform enemy in allEnemiesOnScreen)
                    {
                        if (enemy != this && enemy != null)
                        {
                            float enemyPositionDifference = Vector3.Distance(transform.position, enemy.transform.position);
                            if (enemyPositionDifference < diffInEnemyDistance)
                            {
                                Vector3 dist = transform.position - enemy.transform.position;
                                transform.position += dist.normalized * Time.deltaTime * moveVelocity;
                            }

                        }
                    }
                }
                if (differencePosition.x > 0) { checkSpriteFlipped = false; }
                else { if (differencePosition.x < 0) { checkSpriteFlipped = true; } }
            }
            
            if(isDead)
            {
                onDeadTimer -= Time.deltaTime;
                if (onDeadTimer < 0f) { Destroy(gameObject); }
            }

        }
    }
    public bool CheckSpriteFlipped() {
        return checkSpriteFlipped;
    }

    public void SetAllEnemiesOnScreen(Transform allEnemiesOnScreen) {
        this.allEnemiesOnScreen = allEnemiesOnScreen;
    }

    public void TakeDamage(float damage) {
        if (!isDead)
        {
            this.health -= damage;
            if (health <= 0f) { isDead = true; }
            OnTakingDamage.Invoke(this, EventArgs.Empty);
        }
    }

    public bool ReturnIsDead() { return isDead; }

    public void SetSpeed(float speed) { this.moveVelocity = speed; }

    public void SetHealth(float health) { this.health = health; }

}
