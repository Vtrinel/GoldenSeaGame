using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : CapturableObject
{
    [Header("Obstacle values")]
    public bool canDamage = true;
    public Collider obstacleCollider;
    public OutlineModifier outlineModifier;

    public override void Catch()
    {
        base.Catch();

        if (isCaptured && canDamage)
        {
            if (!GameManager.gameManager.canDamagePlayer)
            {
                return;
            }

            GameManager.gameManager.playerCale.CaleDamage(); //degats sur la cale du player
            GameManager.gameManager.fXManager.PlayImpactVFX(transform.position); //impact FX
            ScreenShake.screenShake.Shake(ScreenShake.ShakeType.playerHurt);
            
            obstacleCollider.enabled = false; //désactive le collider
            outlineModifier.Configure();
            outlineModifier.ChangeWidth(0, 0); //enléve l'outline
        }
    }

    public override void Spawn(Vector3 spawnPosition)
    {
        base.Spawn(spawnPosition);
        obstacleCollider.enabled = true;
    }
}
