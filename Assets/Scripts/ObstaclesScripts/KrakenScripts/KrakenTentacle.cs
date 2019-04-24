using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenTentacle : MonoBehaviour
{
    public float underwaterZ = -1;
    public AnimationCurve emergeCurve;
    public float amplitude;
    public float emergeDuration = 2;
    float currentEmergeDuration;
    public bool isEmerging;

    public KrakenObstacle kraken;
    public KrakenAnimation krakenAnimation;
    public KrakenVFX krakenVFX;
    

    void Update()
    {
        Emerge();
    }

    void Emerge()
    {
        if (!isEmerging) return;

        currentEmergeDuration += Time.deltaTime;
        float percent = currentEmergeDuration / emergeDuration;

        //Animation & VFX
        if (!krakenAnimation.allEventPlayed)
        {
            float animTime = krakenAnimation.GetCurrentAnimEventTime();

            if (percent >= animTime)
            {
                krakenAnimation.PlayCurrentAnim();
                Debug.Log("Play anim");
            }

            float vfxTime = krakenVFX.GetTargetTime();

            if (percent >= vfxTime)
            {
                krakenVFX.PlayCorrectVoid();
                Debug.Log("Play VFX");
            }
        }
        
        float curveEval = emergeCurve.Evaluate(percent);
        float newZ = underwaterZ + -(curveEval * amplitude);
        transform.position = new Vector3(transform.position.x, transform.position.y,newZ);

        if(currentEmergeDuration > emergeDuration)
        {
            currentEmergeDuration = 0;
            krakenAnimation.allEventPlayed = false;
            isEmerging = false;
            krakenVFX.ResetVFX();
        }
    }
}
