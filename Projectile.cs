using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileMoveSpeed = 2f;

    public void SetProjectileSpeed(float projectileSpeed) {
        this.projectileMoveSpeed = projectileSpeed;
    }

    public void Awake()
    {
        transform.right = Vector3.right;
    }

    public virtual void SetTransformRight(Vector3 right) { }

    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * projectileMoveSpeed);
    }

}
