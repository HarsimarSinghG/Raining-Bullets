using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsAndCollectibles : MonoBehaviour
{
    private float playerGrabRadius = 1.5f;
    [SerializeField] private LayerMask playerLayer;
    private float gemMoveAwaySpeed = 10f;
    private float gemMoveTowardsPlayerSpeed = 18f;
    private Vector3 playerMoveDir;
    private bool collected = false;
    private float timerForMoveAway = 0.25f;
    private bool movedAway = false;


    private void Update()
    {
        if (!collected)
        {
            InRangeOfPlayer();
        }
        if(collected && !movedAway){ transform.position += playerMoveDir * Time.deltaTime * gemMoveAwaySpeed;
            timerForMoveAway -= Time.deltaTime;
            if (timerForMoveAway < 0f) {
                movedAway = true; } }
        if(collected && movedAway) { MoveTowardsPlayer(); }
        

    }

    private void InRangeOfPlayer()
    {
        playerMoveDir = Player.Instance.ReturnMoveDir();
        Collider[] playerCheck = Physics.OverlapSphere(transform.position, playerGrabRadius, playerLayer);
        if (playerCheck.Length > 0)
        {
            collected = true;
	   
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 diffWithPlayer = Player.Instance.transform.position - transform.position;
        transform.position += diffWithPlayer * Time.deltaTime * gemMoveTowardsPlayerSpeed;
        if (Vector3.Distance(Player.Instance.transform.position, transform.position) < 0.3f) { Destroy(gameObject); }
    }
}
