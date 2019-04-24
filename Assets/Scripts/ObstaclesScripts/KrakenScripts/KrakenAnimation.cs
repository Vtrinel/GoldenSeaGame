using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class KrakenAnimation : MonoBehaviour
{
    [System.Serializable]
    public class AnimCurveEvent
    {
        [Range(0.0f,1.0f)]
        public float time;
        public string animTriggerName;
    }

    public AnimCurveEvent[] animCurveEvents;

    public Animator anim;
    public int currentEventIndex = 0;
    [ReadOnly]
    public bool allEventPlayed = false;

    public void TriggerAnim(string _animName)
    {
        anim.SetTrigger(_animName);
    }

    public void PlayCurrentAnim()
    {
        TriggerAnim(animCurveEvents[currentEventIndex].animTriggerName);
        currentEventIndex++;

        if(currentEventIndex > animCurveEvents.Length - 1)
        {
            currentEventIndex = 0;
            allEventPlayed = true;
        }
    }

    public float GetCurrentAnimEventTime()
    {
         return animCurveEvents[currentEventIndex].time;
    }
}
