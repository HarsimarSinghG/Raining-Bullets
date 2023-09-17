using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAttacks : MonoBehaviour
{
    [SerializeField] private List<AttacksSO> attacksSOs;

    private void Start()
    {
        Player.Instance.OnNewAttackAcquired += Player_OnNewAttackAcquired;
    }

    private void Player_OnNewAttackAcquired(object sender, System.EventArgs e)
    {
        Instantiate(attacksSOs[1].attackObject, gameObject.transform);

    }
}
