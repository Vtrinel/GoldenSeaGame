using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Transform leftBoat;
    public  BoatScript leftBoatScript;
    public Transform rightBoat;
    public BoatScript rightBoatScript;
    [SerializeField] [Range(0, 5f)] private float interactionRange;

    void Start()
    {
        Application.targetFrameRate = 60;
        GetRequiredComponenet();
    }

    void Update()
    {
        if (UnityEngine.Input.touches.Length > 0)
        {
            Inputs();
        }
    }

    private void Inputs()
    {
        foreach (Touch _touch in Input.touches)
        {
            if(_touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                hit = GetImpactPoint(_touch);

                if (leftBoatScript.HasTarget == false && _touch.fingerId != rightBoatScript.TargetTouchId)
                {
                    if (Vector2.Distance(hit.point, leftBoat.position) < interactionRange)
                    {
                        leftBoatScript.HasTarget = true;
                        leftBoatScript.TargetTouchId = _touch.fingerId;
                    }
                }
                if (rightBoatScript.HasTarget == false && _touch.fingerId != leftBoatScript.TargetTouchId)
                {
                    if (Vector2.Distance(hit.point, rightBoat.position) < interactionRange)
                    {
                        rightBoatScript.HasTarget = true;
                        rightBoatScript.TargetTouchId = _touch.fingerId;
                    }
                }
            }
        }
    }

    [ContextMenu("GET COMPONENT")]
    void GetRequiredComponenet()
    {
        leftBoatScript = leftBoat.GetComponent<BoatScript>();
        rightBoatScript = rightBoat.GetComponent<BoatScript>();
    }


    private RaycastHit GetImpactPoint(Touch _touch)
    {
        Vector3 touchPosFar = Camera.main.ScreenToWorldPoint(new Vector3(_touch.position.x, _touch.position.y, Camera.main.farClipPlane));
        Vector3 touchPosNear = Camera.main.ScreenToWorldPoint(new Vector3(_touch.position.x, _touch.position.y, Camera.main.nearClipPlane));

        RaycastHit _hit;
        Physics.Raycast(touchPosNear, touchPosFar - touchPosNear, out _hit);
        Debug.DrawRay(touchPosNear, touchPosFar - touchPosNear,Color.red,5f);
        return _hit;
    }
}
