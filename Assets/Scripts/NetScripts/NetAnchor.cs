using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetAnchor : MonoBehaviour
{
    public Transform leftShip, rightShip;
    [Range(0,1)]
    public float percentPosition = 0.2f;

    private void Awake()
    {
        Positionning();
    }

    void LateUpdate()
    {
        Positionning();
    }

    void Positionning()
    {
        Vector3 targetPosition = Vector3.Lerp(leftShip.position, rightShip.position, percentPosition);
        transform.position = targetPosition;
    }
}
