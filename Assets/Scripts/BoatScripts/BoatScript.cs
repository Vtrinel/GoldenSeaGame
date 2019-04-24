using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BoatScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]  private float moveSpeed = 100;
    [SerializeField] [Range(0, 2f)] private float movePrecision = 0.1f;
    public Rigidbody body;
    public LayerMask moveSurfaceLayer;

    private bool hasTarget = false;
    private int targetTouchId = 100;

    [Header("Sprite")]
    public GameObject fingerOnScreenSprite;


    public bool HasTarget
    {
        get => hasTarget;
        set
        {
            if (value == true)
            {
                fingerOnScreenSprite.SetActive(true);
            }
            else
            {
                fingerOnScreenSprite.SetActive(false);
            }

            hasTarget = value;
        }

    }

    public int TargetTouchId { get => targetTouchId; set => targetTouchId = value; }

    void Start()
    {
        Input.multiTouchEnabled = true;
        fingerOnScreenSprite.SetActive (false);
    }

    void Update()
    {
        if (hasTarget)
        {
            foreach (Touch _touch in Input.touches)
            {
                if (_touch.fingerId == targetTouchId)
                {
                    MoveBoat(GetImpactPoint(_touch));
                    

                    if (_touch.phase == TouchPhase.Canceled || _touch.phase == TouchPhase.Ended)
                    {
                        HasTarget = false;
                        TargetTouchId = 100;
                        body.velocity = Vector3.zero;
                    }
                }
            }
        }
    }

    public void MoveBoat(RaycastHit _hit)
    {
        if (_hit.collider == null)
        {
            body.velocity = Vector3.zero;
            return;

        }

        Vector3 targetPosition = new Vector3(_hit.point.x, _hit.point.y, transform.position.z);
        float distance = Vector2.Distance(targetPosition, transform.position);

        if(distance > movePrecision)
        {
            Vector2 direction = targetPosition - transform.position;
            body.velocity = direction * moveSpeed; 
        }
        else
        {
            body.velocity = Vector3.zero;
        }
        

        /*
        transform.position = Vector3.MoveTowards(transform.position, _hit.point, moveSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        */
    }

    private RaycastHit GetImpactPoint(Touch _touch)
    {
        Vector3 touchPosFar = Camera.main.ScreenToWorldPoint(new Vector3(_touch.position.x, _touch.position.y, Camera.main.farClipPlane));
        Vector3 touchPosNear = Camera.main.ScreenToWorldPoint(new Vector3(_touch.position.x, _touch.position.y, Camera.main.nearClipPlane));

        RaycastHit _hit;
        Physics.Raycast(touchPosNear, touchPosFar - touchPosNear, out _hit,1000,moveSurfaceLayer);
        return _hit;
    }

    [Button("Get required components")]
    void GetRequiredComponents()
    {
        //fingerOnScreenSprite = GetComponentInChildren<SpriteRenderer>();
        body = GetComponent<Rigidbody>();
    }
}
