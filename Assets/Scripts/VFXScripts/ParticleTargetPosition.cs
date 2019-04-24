using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class ParticleTargetPosition : MonoBehaviour
{
    public Transform parent, child;

    [Header("Direction offset")]
    public float offsetAmplitude = 1;
    public AnimationCurve offsetX;
    public AnimationCurve offsetY;
    public AnimationCurve offsetZ;

    [Header("Speed")]
    [Tooltip("La curve ne doit a AUCUN MOMENT avoir une valeur égal a 0")]
    public AnimationCurve speedCurve;
    public float speed = 0.25f;

    public bool isMoving;
    Vector3 targetPos;
    float distance;

    [Header("Trail")]
    [SerializeField] TrailRenderer trailRenderer;

    [Header("Events")]
    public UnityEvent startEvent;
    public UnityEvent endEvent;

    [Header("Debug")]
    public Transform debugTarget;

    
    void Start()
    {
        if(debugTarget != null)
            targetPos = debugTarget.position;

        trailRenderer.emitting = false;
    }

    void Update()
    {
        Moving();
    }

    [Button]
    public void Play()
    {
        GetGUIWorldPosition();
        StartMovement();
    }

    void GetGUIWorldPosition()
    {
        Vector3 pos = GameManager.gameManager.caleUI.GetBarTopPosition();
        targetPos = pos;      
    }

    public void StartMovement(Vector3 _targetPosition)
    {
        targetPos = _targetPosition;
        distance = Vector3.Distance(parent.position, targetPos);
        isMoving = true;
        startEvent.Invoke();
    }

    [Button("Play movement with debug target")]
    public void StartMovement()
    {
        distance = Vector3.Distance(parent.position, targetPos);
        isMoving = true;
        startEvent.Invoke();
        trailRenderer.emitting = true;
    }

    void Moving()
    {
        if (!isMoving) return;

        float currentDistance = Vector3.Distance(parent.position, targetPos);
        float pathPercent = currentDistance / distance;

        float currentSpeed = speed * speedCurve.Evaluate(pathPercent);


        Vector3 direction = targetPos - parent.position;
        Debug.DrawRay(parent.position, direction, Color.green, 10);
        parent.Translate(direction.normalized * speed,Space.World);
        

        Vector3 offsetPosition = new Vector3(offsetX.Evaluate(pathPercent),offsetY.Evaluate(pathPercent),offsetZ.Evaluate(pathPercent));
        child.localPosition = offsetPosition * offsetAmplitude;

        if (CustomMethod.AlmostEqual(parent.position, targetPos,0.3f))
        {
            isMoving = false;
            endEvent.Invoke();
            trailRenderer.emitting = false;
            GameManager.gameManager.caleUI.PlayHighlightFX();
        }

    }
}
