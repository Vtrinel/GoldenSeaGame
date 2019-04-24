using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenFishMovement : MonoBehaviour
{
    public Rigidbody body;
    public float speed;
    public bool canMove;

    public virtual void Move()
    {
        //Moving....
    }

    public void StartMovement()
    {
        canMove = true;
    }

    public void StopMovement()
    {
        canMove = false;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
        else
        {
            if(body.velocity != Vector3.zero)
            {
                body.velocity = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// EDITOR ONLY Permet de récupérer le rigidbody
    /// </summary>
    [ContextMenu("Get rigidbody")]
    public void GetRigidbody()
    {
        body = GetComponent<Rigidbody>();
    }
}
