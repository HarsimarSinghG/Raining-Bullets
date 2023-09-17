using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAttackCollider : MonoBehaviour
{
    private BoxCollider boxCollider;
    private float nextHitTime = 0f;
    public event EventHandler IfPlayerInsideCollider;
    [SerializeField] private EnemySO enemySO;

    //private void Update()
    //{
    //    if (hitboxNumber != null && hitboxNumber.activeSelf && attackInProgress)
    //    {
    //        //if (Vector3.Distance(transform.position, Player.Instance.transform.position)<hitboxRegisterationDistance) { Player.Instance.TakeDamage(damage);
    //        //    Debug.Log(Player.Instance.ReturnHealth()); }
    //        Collider[] hitPlayer = Physics.OverlapSphere(transform.position,hitboxRegisterationDistance,playerLayer);
    //        if (hitPlayer.Length>=1) { Player.Instance.TakeDamage(damage);
    //            Debug.Log(Player.Instance.ReturnHealth()); }
    //        attackInProgress = false;
    //    }

    //}

    private void Update()
    {
        nextHitTime -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!Player.Instance.ReturnIsDead())
        {
            Player player = other.gameObject.GetComponentInParent<Player>();
            AllAttacks allAttacks = other.gameObject.GetComponentInParent<AllAttacks>();
            if (player!=null && allAttacks==null)
            {
                IfPlayerInsideCollider?.Invoke(this, EventArgs.Empty);
                if (nextHitTime <= 0f)
                {
                    Player.Instance.TakeDamage(enemySO.damage);
                    Debug.Log(Player.Instance.ReturnHealth());
                    nextHitTime = 0.5f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nextHitTime = 0f;
    }

}
