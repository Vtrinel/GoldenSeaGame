using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFishMovement : GoldenFishMovement
{
    [Header("Flying movement")]
    public AnimationCurve flyCurve;
    public AnimationCurve speedCurve;
    public float flyCycle = 3;
    public float flyAmplitude = 1;
    private float currentTime;

    private float iniPositionZ;
    private float currentSpeed;

    Vector3 origin;

    public Vector3 first;
    public Vector3 last;
    
    void Start()
    {
        iniPositionZ = transform.position.z;
        origin = transform.position;
        first = transform.position;
    }


    void Update()
    {
        Rotation();
    }

    private void LateUpdate()
    {
        origin = transform.position;
    }

    void Fly()
    {
        currentTime += Time.deltaTime;

        float percent = currentTime / flyCycle;
        float curveEval = flyCurve.Evaluate(percent);
        currentSpeed = speed * speedCurve.Evaluate(percent);

        float newZ = iniPositionZ + (-curveEval) * flyAmplitude;

        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        if(currentTime >= flyCycle)
        {
            currentTime = 0.0f;
            last = transform.position;
        }

    }

    void Rotation()
    {
        Vector3 dir = origin - transform.position;

        if(dir != Vector3.zero)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        }
        
    }

    public override void Move()
    {
        base.Move();
        Fly();
        Vector2 direction = new Vector2(0, -1);

        body.velocity = direction * currentSpeed * Time.deltaTime; //Apply movement to rigidbody
        
    }
}
