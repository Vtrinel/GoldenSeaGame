using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RockObstacle : Obstacle
{
    [Header("Rock values")]
    public Rigidbody body;
    public float speed = 50;
    Vector3 dir = Vector3.down ;
    public bool canMove;

    [Header("VFX")]
    public Renderer rockRenderer;

    public override void Spawn(Vector3 spawnPosition)
    {
        base.Spawn(spawnPosition);
        canMove = true;

    }

    public override void Catch()
    {
        base.Catch();
        rockRenderer.enabled = false;
        GameManager.gameManager.fXManager.PlaySmokeVFX(transform.position);
    }

    private void Start()
    {
        if(body == null)
        {
            body = GetComponent<Rigidbody>();
        }

        SetGoodRotation();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (!canMove) return;

        body.velocity = dir * speed * Time.deltaTime;
    }

    [Button]
    void SetGoodRotation()
    {
        float _z = Random.Range(0, 360);

        transform.eulerAngles = new Vector3(180, 0, _z);
    }

    public void StartMovement()
    {
        canMove = true;
    }
}
