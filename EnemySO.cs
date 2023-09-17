using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    public Enemy enemy;
    public float speed;
    public bool meleeEnemy;
    public bool groupableEnemy;
    public bool surroundEnemy;
    public float damage;
    public float health;
}
