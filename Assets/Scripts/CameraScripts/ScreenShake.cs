using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake screenShake;

    private Vector3 _originalPos;
    private float _timeAtCurrentFrame;
    private float _timeAtLastFrame;
    private float _fakeDelta;

    [Header("Shake Profiles")]
    public ShakeProfile playerHurtShake;
    public ShakeProfile fishCapturedShake;

    [Header("Debug values")]
    public float shakeDebugDuration = 1;
    public float shakeDebugAmplitude = 1;

    public enum ShakeType
    {
        playerHurt,
        fishCaptured,
        superFishCaptured,
    }

    void Awake()
    {
        screenShake = this;
        screenShake._originalPos = screenShake.gameObject.transform.localPosition;
    }

    void Update()
    {
        
        _timeAtCurrentFrame = Time.realtimeSinceStartup;
        _fakeDelta = _timeAtCurrentFrame - _timeAtLastFrame;
        _timeAtLastFrame = _timeAtCurrentFrame;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeDebug();
        }
    }

    public void Shake(ShakeType shakeType)
    {
        float duration = 0;
        float amount = 0;

        switch (shakeType)
        {
            case ShakeType.playerHurt:
                duration = playerHurtShake.shakeDuration;
                amount = playerHurtShake.shakeAmplitude;
                break;
            case ShakeType.fishCaptured:
                duration = fishCapturedShake.shakeDuration;
                amount = fishCapturedShake.shakeAmplitude;
                break;
            case ShakeType.superFishCaptured:
                duration = fishCapturedShake.shakeDuration;
                amount = fishCapturedShake.shakeAmplitude;
                break;
            default:
                break;
        }

        StopAllCoroutines();
        StartCoroutine(CamShake(duration, amount));
    }

    public void Shake(float duration, float amount)
    {
        StopAllCoroutines();
        StartCoroutine(CamShake(duration, amount));
    }

    public IEnumerator CamShake(float duration, float amount)
    {
        float endTime = Time.time + duration;

        while (duration > 0)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            duration -= _fakeDelta;

            yield return null;
        }

        transform.localPosition = _originalPos;
    }

    [Button]
    public void ShakeDebug()
    {
        Shake(shakeDebugDuration, shakeDebugAmplitude);
    }

}

[System.Serializable]
public class ShakeProfile
{
    public float shakeDuration;
    public float shakeAmplitude;
}

