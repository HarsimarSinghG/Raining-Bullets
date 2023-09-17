using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    private float damage = 6f;
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
        if (enemy) { enemy.TakeDamage(damage); }
    }
}
