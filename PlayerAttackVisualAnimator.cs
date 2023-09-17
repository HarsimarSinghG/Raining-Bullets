using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackVisualAnimator : MonoBehaviour
{
    //private bool attackInProgress = false;
    //private float attackRange = 1.2f;
    //[SerializeField] private LayerMask enemyLayer;
    //private float damage = 7f;
    //private Animator animator;
    //private const string ATTACK = "Attack";

    //private void Awake()
    //{
    //    animator = GetComponent<Animator>();
    //}

    //private void Start()
    //{
    //    Player.Instance.OnAttack += Player_OnAttack;
    //}

    //private void Player_OnAttack(object sender, EventArgs e)
    //{
    //    attackInProgress = true;
    //    animator.SetTrigger(ATTACK);
    //}

    //private void Update()
    //{
    //    if(attackInProgress && gameObject.activeSelf) {
    //        Collider[] enemiesHit = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
    //        foreach(Collider enemyCollider in enemiesHit) {
    //            if (!enemyCollider.gameObject.GetComponentInParent<Enemy>().ReturnIsDead())
    //            {
    //                enemyCollider.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
    //            }
    //        }
    //        attackInProgress = false;
    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}

    private bool isAttacking = false;
    private float nextAttackWaitTime = 4f;
    [SerializeField] GameObject attackVisual;
    private float attackDuration = 3f;
    private const string ATTACK_ANIMATION = "Attack";
    private const string IDLE_ANIMATION = "Idle";


    private void Update()
    {
        TriggerAttack();
    }

    private void TriggerAttack() 
    {
        if (!isAttacking) { StartCoroutine(AttackRoutine()); }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        attackVisual.SetActive(true);
        attackVisual.GetComponent<Animator>().SetTrigger(ATTACK_ANIMATION);
        yield return new WaitForSeconds(attackDuration);
        attackVisual.GetComponent<Animator>().SetTrigger(IDLE_ANIMATION);
        attackVisual.SetActive(false);
        yield return new WaitForSeconds(nextAttackWaitTime);
        isAttacking = false;


    }

}
