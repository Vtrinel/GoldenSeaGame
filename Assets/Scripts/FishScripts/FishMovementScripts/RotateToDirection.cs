using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToDirection : MonoBehaviour
{
    public enum GetDirectionType
    {
        Manually,
        FromRigidbody,
        Automatic,
    }

    public GetDirectionType getDirectionType = GetDirectionType.FromRigidbody;
    public bool canRotate = true;
    [Range(-360f,360)]
    public float angleOffset = 0;


    private Rigidbody body;
    private Vector2 direction;
    private Vector2 origin;

    public Vector2 Direction { get => direction; set => direction = value; }

    void Start()
    {
        if(getDirectionType == GetDirectionType.FromRigidbody)
        {
            body = GetComponent<Rigidbody>();
        }
    }


    void Update()
    {
        if (canRotate)
        {
            RotateTowardsDirection();
        }
    }

    private void LateUpdate()
    {
        if (getDirectionType == GetDirectionType.Automatic)
        {
            origin = transform.position;
        }
    }

    public void RotateTowardsDirection()
    {
        switch (getDirectionType)
        {
            case GetDirectionType.FromRigidbody:
                Direction = body.velocity.normalized;
                break;
            case GetDirectionType.Automatic:
                Direction = (Vector2)transform.position - origin;
                break;
        }

        if (Direction == Vector2.zero) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float newAngle = angle + angleOffset;
        transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
    }
}
