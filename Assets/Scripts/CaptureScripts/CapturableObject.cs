using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturableObject : SpawnableObject, ICatchObject
{
    [Header("Capturable Object values")]
    public bool insideNet; //pour check s'il est dans le filet
    public bool isCaptured;
    [Range(0.0f, 2.0f)]
    public float netShakeAmplitude = 1;

    #region Interface functions

    public virtual void Catch()
    {
        isCaptured = true;
        GameManager.gameManager.netShakerManager.StartShaking(this.transform,netShakeAmplitude);
    }

    public void InsideNet(bool isInside)
    {
        insideNet = isInside;
    }

    public override void Spawn(Vector3 spawnPosition)
    {
        base.Spawn(spawnPosition);
        isCaptured = false;
        insideNet = false;
    }

    #endregion

    void Start()
    {

        
    }

    void Update()
    {
        //Inside();
    }

    void Inside()
    {
        if (!insideNet) return;
        insideNet = StillInsideNet();
    }

    /// <summary>
    /// Permet de savoir s'il est toujours dans le filet après avoir hit le raycast
    /// </summary>
    /// <returns></returns>
    bool StillInsideNet()
    {
        bool inside = true;

        float maxY = Mathf.Max(BoatManager.firstBoatPosition.y, BoatManager.secondBoatPosition.y);

        float maxX = Mathf.Max(BoatManager.firstBoatPosition.x, BoatManager.secondBoatPosition.x);
        float minX = Mathf.Min(BoatManager.firstBoatPosition.x, BoatManager.secondBoatPosition.x);


        if(transform.position.y > maxY)
        {
            inside = false;
            return inside;
        }

        if(transform.position.x < minX || transform.position.x > maxX)
        {
            inside = false;
            return inside;
        }

        return inside;
    }


}
