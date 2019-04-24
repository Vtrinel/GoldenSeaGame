using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class KrakenSurfaceEffect : MonoBehaviour
{
    public SpriteRenderer effectSurfaceSprite;
    public LayerMask surfaceLayer;
    public float zOffset = 1.75f;

    void Start()
    {
        
    }
    
    void Update()
    {
        SetEffetPosition();
    }

    public void Positionning(Transform _transformToGet)
    {
        transform.localPosition = new Vector3(
            _transformToGet.localPosition.x, 
            _transformToGet.localPosition.y, 
            transform.localPosition.z
            );
    }

    void SetEffetPosition()
    {
        Ray ray = new Ray(transform.position, -Vector3.forward);
        Physics.Raycast(ray.origin, ray.direction * 50,out RaycastHit hit,100,surfaceLayer,QueryTriggerInteraction.Collide);
        Debug.DrawRay(ray.origin, ray.direction * 50,Color.red);

        if (hit.collider != null)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y, hit.point.z - zOffset);
        }
    }

    [Button]
    public void StartEffect()
    {
        effectSurfaceSprite.enabled = true;
        SetEffetPosition();
    }

    [Button]
    public void StopEffect()
    {
        effectSurfaceSprite.enabled = false;
    }
}
