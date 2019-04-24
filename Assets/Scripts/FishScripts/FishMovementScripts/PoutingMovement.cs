using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script de mouvement du tacaud
/// </summary>
public class PoutingMovement : GoldenFishMovement
{
    [Header("Pouting movement values")]
    [Tooltip("Fluctuation de la direction du movement (1 = Droite, -1 = Gauche")]
    public AnimationCurve moveDirectionCurve;

    [Range(0,360)]
    [Tooltip("angle de direction du mouvement")]
    public float dirAngle = 270;

    [Tooltip("l'amplitude de la fluctuation du mouvement")]
    public float curveAmplitude = 20;

    Vector2 dir; //direction du mouvement

   

    // Start is called before the first frame update
    void Start()
    {
        moveDirectionCurve.postWrapMode = WrapMode.Loop; //Set curve to loop
        dir = CustomMethod.DirFromAngle(dirAngle-90);
        dir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Move()
    {
        base.Move();

        //Set direction
        float fluctuation = moveDirectionCurve.Evaluate(Time.time);
        float newAngle = fluctuation * curveAmplitude;
        Vector2 direction = new Vector2(dir.x + newAngle, dir.y);

        //Apply movement to rigidbody
        body.velocity = direction * (speed * Time.deltaTime);
    }
}
