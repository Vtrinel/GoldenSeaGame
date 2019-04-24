using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GoldenFish : CapturableObject
{
    public enum FishType
    {
        Bronze,
        Silver,
        Gold
    }

    public enum FishSize
    {
        Small,
        Medium,
        Big
    }

    [Header("Scoring value")]
    public FishType fishType = FishType.Silver;
    public FishValue fishValue;
    public FishSize fishSize = FishSize.Medium;
    [Space(8)]
    public float value;
    public float size; 

    [Header("Movement")]
    public GoldenFishMovement goldenFishMovement;
    private PlayerCale playerCale;

    [Header("Rotation")]
    public Vector3 rotation = new Vector3(-90, 0,0);

    [Header("Capture Values")]
    //TEST : TOUT CE QUI SUIT EST DU TEST HEIN !!!!
    public Collider fishCollider;
    public bool needToBeInsideForCapture = true;

    [Header("VFX")]
    public Renderer fishRenderer;
    public ParticleTargetPosition particleTarget;

    #region  inheritance function

    /// <summary>
    /// Quand le poisson se fait attraper dans le filet
    /// </summary>
    public override void Catch()
    {
        base.Catch();

        if (needToBeInsideForCapture && !insideNet) //check s'il est à l'intérieur
        {
            return; 
        }
        

        playerCale.CollectFish(size, value); //incrémente la cale du player
        ScreenShake.screenShake.Shake(ScreenShake.ShakeType.fishCaptured);
        particleTarget.Play();
        fishCollider.enabled = false;
        goldenFishMovement.canMove = false;
        StartCoroutine(Captured());
    }

    /// <summary>
    /// Quand le poisson spawn
    /// </summary>
    /// <param name="spawnPosition"></param>
    public override void Spawn(Vector3 spawnPosition)
    {
        base.Spawn(spawnPosition);
        fishCollider.enabled = true; //ré-active le collider
        goldenFishMovement.StartMovement(); //Start le movement
    }

    #endregion

    private void Start()
    {
        transform.eulerAngles = rotation;
        playerCale = GameManager.gameManager.playerCale;
        fishValue.GenerateValue(); //calcul les valeurs
        value = fishValue.GetValue(fishType); //récupère et stock la valeur
    }

    public IEnumerator Captured()
    {
        yield return new WaitForSeconds(0.1f);
        //gameObject.SetActive(false);
        fishRenderer.enabled = false;
        base.insideNet = base.insideNet;
    }

    [Button("GENRATE PREVIEW FISH VALUES")]
    public void GeneratePreviewValue()
    {
        fishValue.GenerateValue(); //calcul les valeurs
        value = fishValue.GetValue(fishType); //récupère et stock la valeur
    }

}
