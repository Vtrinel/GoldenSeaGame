using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetShaker : MonoBehaviour
{
    [Header("Shake Values")]
    public float shakeAmplitude = 1;
    float currentShakeAmplitude;

    public float minY = -1, maxY = 1;
    public AnimationCurve shakeCurve;

    public bool isShaking;
    public float timeToShake;
    float currentShakeTime;

    private void Update()
    {
        Shaking();
    }

    public void Shake(float _amplitude)
    {
        currentShakeAmplitude = _amplitude;
        isShaking = true;
    }

    void Shaking()
    {
        if (!isShaking) return;

        if (currentShakeTime >= timeToShake)
        {
            isShaking = false;
            currentShakeTime = 0.0f;
        }

        currentShakeTime += Time.deltaTime;

        float percent = currentShakeTime / timeToShake;
        float curveEval = shakeCurve.Evaluate(percent);
        float newY = Mathf.Lerp(minY, maxY, curveEval) * currentShakeAmplitude;

        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }


}
