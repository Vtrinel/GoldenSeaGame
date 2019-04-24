using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFishShadow : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform shadow;

    void Update()
    {
        Raycast();
    }

    void Raycast()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        Physics.Raycast(ray.origin, ray.direction,out RaycastHit hit, 100, layerMask, QueryTriggerInteraction.Collide);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if(hit.collider != null)
        {
            shadow.transform.position = hit.point;
        }
    }
}
