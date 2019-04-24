using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class KrakenVFX : MonoBehaviour
{

    public ParticleSystem[] particleSystems;
    [Range(0.0f,1.0f)]
    public float playTime = 0.1f;
    [Range(0.0f, 1.0f)]
    public float stopTime = 0.8f;
    [ReadOnly]
    public bool playDone, stopDone;

    [Button]
    public void PlayKrakenFx()
    {
        if (particleSystems[0].isPlaying) return;

        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }

        playDone = true;
    }

    [Button]
    public void StopKrakenFx()
    {
        if (!particleSystems[0].isPlaying) return;

        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop(true,ParticleSystemStopBehavior.StopEmitting);
        }

        stopDone = true;
    }

    public void ResetVFX()
    {
        playDone = false;
        stopDone = true;
    }

    public float GetTargetTime()
    {
        if (playDone)
        {
            return stopTime;
        }
        else
        {
            return playTime;
        }
    }

    public void PlayCorrectVoid()
    {
        if (playDone)
        {
            StopKrakenFx();
        }
        else
        {
            PlayKrakenFx();
        }
    }

}
