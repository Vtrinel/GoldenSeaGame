using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TimeManager : MonoBehaviour
{

    [Header("Pause Values")]
    private bool timePaused;
    float previousTimeScale = 1;
    bool wasInSlowMotion = false;


    [Header("Hit Slow Motion Values")]
    public AnimationCurve timeRecoverEvolution;
    public float timeRecoverDuration = 1.25f;
    [Range(0.01f,1.0f)]
    public float slowMotionScale = 0.4f;
    [ShowNonSerializedField]
    float currentRecoverDuration = 0.0f;
    [ShowNonSerializedField]
    bool inSlowMotion = false;

    public bool TimePaused
    {
        get
        {
            return timePaused;
        }
        set
        {
            if(value == true)
            {
                if (inSlowMotion)
                {
                    wasInSlowMotion = true;
                    inSlowMotion = false;
                }

                previousTimeScale = Time.timeScale;
                Time.timeScale = 0;
            }
            else
            {
                if (wasInSlowMotion)
                {
                    inSlowMotion = true;
                }

                Time.timeScale = previousTimeScale;
            }

            timePaused = value;
        }
    }

    void Start()
    {
        inSlowMotion = false;
    }
    
    void Update()
    {
        SlowMotion();   
    }

    [Button]
    public void StartHitSlowMotion()
    {
        inSlowMotion = true;
        GameManager.gameManager.canDamagePlayer = false;
    }

    void SlowMotion()
    {
        if (!inSlowMotion) return;

        currentRecoverDuration += Time.unscaledDeltaTime;

        float percent = currentRecoverDuration / timeRecoverDuration;
        float curveEval = timeRecoverEvolution.Evaluate(percent);
        float targetTimeScale = Mathf.Lerp(slowMotionScale, 1, curveEval);
        Time.timeScale = targetTimeScale;

        if(currentRecoverDuration >= timeRecoverDuration)
        {
            currentRecoverDuration = 0.0f;
            inSlowMotion = false;
            GameManager.gameManager.canDamagePlayer = true;
            Time.timeScale = 1;
        }
    }

    [Button]
    void Pause()
    {
        TimePaused = true;
    }

    [Button]
    void Resume()
    {
        TimePaused = false;
    }

}
