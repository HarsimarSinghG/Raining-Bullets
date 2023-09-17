using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }
    bool isWalking;
    private bool checkSpriteFlipped;
    private float health = 20;
    public event EventHandler OnTakingHit;
    private bool isDead = false;
    private float onDeadTimer = 3f;
    private float moveSpeed = 7f;
    private Vector3 moveDir;
    private GameObject currentFloor;
    public event EventHandler OnNewAttackAcquired;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnDashPerformed += GameInput_OnDashPerformed;
    }

    private void GameInput_OnDashPerformed(object sender, EventArgs e)
    {
        float randomX = 0f;
        float randomY = 0f;
        if (moveDir.x > 0) { randomX = 3; }
        if (moveDir.x < 0) { randomX = -3; }
        if (moveDir.y > 0) { randomY = 3; }
        if (moveDir.y < 0) { randomY = -3; }
        Vector3 dashPosition = new Vector3(randomX, randomY, 0);
        float dashSpeed = 50f;
        transform.position += dashPosition * dashSpeed * Time.deltaTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { OnNewAttackAcquired?.Invoke(this, EventArgs.Empty); }
        if (!isDead)
        {
            HandleMovements();
        }

        else {
            onDeadTimer -= Time.deltaTime;
            if (onDeadTimer < 0f) { Destroy(gameObject); } }
    }

    private void HandleMovements()
    {

            Vector2 inputVector = GameInput.Instance.ReturnMovementVectorNormalised();
            moveDir = new Vector3(inputVector.x, inputVector.y, 0);
            isWalking = moveDir != Vector3.zero;
            if (moveDir.x > 0) { checkSpriteFlipped = false; }
            else
            { if (moveDir.x < 0) { checkSpriteFlipped = true; } }
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        
    }
    

    public bool ReturnStateIsWalking() {
        return isWalking;
    }


    public bool CheckSpriteFlipped() {
        return checkSpriteFlipped;
    }

    public Vector3 ReturnMoveDir() { return moveDir; }

    public void TakeDamage(float damageAmount) {
        if (!isDead)
        {
            this.health -= damageAmount;
            if (health <= 0f) { isDead = true; }
            OnTakingHit?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public float ReturnHealth() { return health; }

    // Program for player attacking the enemy so that
    // player cannot spam the attacks.

    //private void SetNextAttackTime() {
    //    if (Time.time)>= nextAttackTime){ Attack();
    //        nextAttackTime = Time.time + 1f / attackRate;  }
    // }

    public bool ReturnIsDead() { return isDead; }

    public void SetCurrentFloor(GameObject currentFloor) { this.currentFloor = currentFloor; }
    public GameObject ReturnCurrentFloor() { return currentFloor; }
}
