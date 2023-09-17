using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimations : MonoBehaviour
{
    private const string IS_WALKING = "Walk";
    private const string IS_RUNNING = "Run";
    private const string GETHIT = "GetHitByEnemy";
    private const string DEATH = "Dead";
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Player.Instance.OnTakingHit += Player_OnTakingHit;
    }

    private void Player_OnTakingHit(object sender, EventArgs e)
    {
        if (Player.Instance.ReturnIsDead()) { animator.SetBool(DEATH, true); }
        animator.SetTrigger(GETHIT);
    }


    private void Update()
    {
        SetWalkingAnimation();
        
    }
    private void SetWalkingAnimation()
    {
        spriteRenderer.flipX = Player.Instance.CheckSpriteFlipped();

        if (Player.Instance.ReturnStateIsWalking())
        {
            //if (Player.Instance.ReturnStateIsRunning())
            //{
            //    animator.SetBool(IS_RUNNING, true);
            //    animator.SetBool(IS_WALKING, true);
            //}
            //else
            //{
            //    animator.SetBool(IS_WALKING, true);
            //    animator.SetBool(IS_RUNNING, false);
            //}
            animator.SetBool(IS_WALKING, true);
            animator.SetBool(IS_RUNNING, false);
        }
        else
        {
            animator.SetBool(IS_WALKING, false);
            animator.SetBool(IS_RUNNING, false);
        }
    }

}
