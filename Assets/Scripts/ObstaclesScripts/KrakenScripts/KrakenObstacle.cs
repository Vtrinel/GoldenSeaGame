using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class KrakenObstacle : Obstacle
{
    [Header("Kraken values")]
    public Vector3 cubeSize = Vector3.one;
    public GameObject tentacle;

    public float speed = 100;
    public Rigidbody body;
    public bool canMove;
    Vector3 direction = Vector3.down;

    [Header("Tentacle")]
    public KrakenTentacle krakenTentacle;
    public Transform krakenVfX;
    public float emergeCycleDuration;

    [Header("Tentacle Collision")]
    [Required]
    public CapsuleCollider capsuleCollider;
    public Vector3 colliderOffset;


    void Start()
    {
        
    }

    void Update()
    {
        ColliderPositionning();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Spawn(Vector3 spawnPosition)
    {
        base.Spawn(spawnPosition);
        StartKraken();
    }

    [Button("START KRAKEN")]
    private void StartKraken()
    {
        canMove = true;
        StartCoroutine(SetAttackTentacle());
    }

    public override void Catch()
    {
        base.Catch();
        canMove = false;
        StopAllCoroutines();
    }

    private void OnDrawGizmosSelected()
    {
        DebugVube();
    }

    void DebugVube()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, cubeSize);
    }

    void Move()
    {
        if (!canMove) return;

        body.velocity = direction * speed * Time.deltaTime;
    }

    void ColliderPositionning()
    {
        Vector3 position = tentacle.transform.localPosition + colliderOffset;
        capsuleCollider.center = position;
    }

    Vector3 RandomPositionInCube(Vector3 center, Vector3 size)
    {
        float x = (Random.value - 0.5f) * size.x;
        float y = (Random.value - 0.5f) * size.y;
        float z = (Random.value - 0.5f) * size.z;

        return center + new Vector3(x,y,z);
    }

    [Button("Set new Tentacle Position")]
    void MoveTentacle()
    {
        Vector3 newPosition = RandomPositionInCube(transform.position, cubeSize);
        tentacle.transform.position = new Vector3(newPosition.x, newPosition.y, tentacle.transform.position.z);
        krakenVfX.position = new Vector3(newPosition.x, newPosition.y, krakenVfX.position.z);
    }

    IEnumerator SetAttackTentacle()
    {
        MoveTentacle();
        krakenTentacle.isEmerging = true;
        yield return new WaitForSeconds(emergeCycleDuration);
        StartCoroutine(SetAttackTentacle());
    }
}
