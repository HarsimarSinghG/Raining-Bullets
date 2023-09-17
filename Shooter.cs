using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// File acquired from Stephen Hubbard youtube channel.

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField][Range(0,359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private bool stagger;
    [SerializeField] private bool oscillate;
    private bool isShooting = false;

    private void Update()
    {
	    Attack();
    }

    private void Attack()
    {
        if (!isShooting) { StartCoroutine(ShootRoutine()); }
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        Vector3 targetDirection;
        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;
        TargetConeOfInfluence(out targetDirection, out startAngle, out currentAngle, out angleStep, out endAngle);
        if (stagger) { timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst; }

        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out targetDirection, out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if(oscillate && i % 2 != 1) { TargetConeOfInfluence(out targetDirection, out startAngle, out currentAngle, out angleStep, out endAngle); }

            else if (oscillate){ currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1; }

            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPosition(currentAngle);
                Vector3 actualPosition = new Vector3(pos.x, pos.y, -1);
                GameObject newBullet = Instantiate(bulletPrefab, actualPosition, Quaternion.identity);
                Player player = GetComponentInParent<Player>();
                if (player != null)
                {
                    if (Player.Instance.ReturnMoveDir() != Vector3.zero)
                    {
                        newBullet.transform.right = Player.Instance.ReturnMoveDir();
                    }
                    else
                    {
                        if (Player.Instance.CheckSpriteFlipped() is false) { newBullet.transform.right = Vector3.right; }
                        else { newBullet.transform.right = new Vector3(-1, 0, 0); }
                    }
                }
                else
                {
                    newBullet.transform.right = newBullet.transform.position - transform.position;
                }
                //newBullet.GetComponent<Projectile>().SetTargetDirection(targetDirection);

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.SetProjectileSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;
                if (stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }

            }

            currentAngle = startAngle;

            if (!stagger)
            {
                yield return new WaitForSeconds(timeBetweenBursts);
            }

        }

        yield return new WaitForSeconds(restTime);

        isShooting = false;
    }

    private void TargetConeOfInfluence(out Vector3 targetDirection, out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        targetDirection = Player.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPosition(float currentAngle) 
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        Vector2 pos = new Vector2(x, y);
        return pos;
    }
}
