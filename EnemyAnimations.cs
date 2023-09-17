using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyAttackCollider enemyAttackCollider;
    private Animator animator;
    private const string WALK_ANIMATION = "Walk";
    private const string DEATH = "Dead";
    private const string GETHIT = "OnHitByPlayer";
    private const string IDLE = "Idle";
    private const string PLAYERDEAD = "PlayerDead";
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        animator.SetBool(WALK_ANIMATION, true);
        enemy.OnTakingDamage += Enemy_OnTakingDamage;
        enemyAttackCollider.IfPlayerInsideCollider += HitboxRegisteration_IfPlayerInsideCollider;
    }

    private void HitboxRegisteration_IfPlayerInsideCollider(object sender, EventArgs e)
    {
        animator.SetTrigger(IDLE);
    }

    private void Enemy_OnTakingDamage(object sender, EventArgs e)
    {
        if (enemy.ReturnIsDead()) { animator.SetBool(DEATH, true); }
        animator.SetTrigger(GETHIT);
    }

    private void Update()
    {
        spriteRenderer.flipX=enemy.CheckSpriteFlipped();

        if (Player.Instance.ReturnIsDead())
        {
            animator.SetBool(WALK_ANIMATION, false);
            animator.SetBool(PLAYERDEAD, true);
        }
    }
}
