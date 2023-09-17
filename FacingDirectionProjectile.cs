using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingDirectionProjectile : Projectile
{
    public override void SetTransformRight(Vector3 right)
    {
        if (Player.Instance != null)
        {
            Vector3 playerMoveDir = Player.Instance.ReturnMoveDir();
            if (playerMoveDir != Vector3.zero) { }

        }
    }
}
