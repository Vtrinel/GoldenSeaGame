using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script d'un movement simple ou le fish descend avec une fluctuation possible sur la descente
/// </summary>
public class SimpleFishMovement : GoldenFishMovement
{

    private void Start()
    {
        
    }

    public override void Move()
    {
        base.Move();

        //Set direction
        Vector2 direction = new Vector2(0,-1);

        //Apply movement to rigidbody
        body.velocity = direction * speed * Time.deltaTime;

    }

}
