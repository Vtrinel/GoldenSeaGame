using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ValueAndSizeTutorial : TutorialPart
{
    public float tutoPartTime = 30;
    float currentTime;

    public GoldenFishMovement[] movements; 

    void Start()
    {

    }

    void Update()
    {
        if (isFinished) return;

        if (!isStarted) return;
        Timer();
    }

    public void StartAllBigFish()
    {
        for (int i = 0; i < movements.Length; i++)
        {
            movements[i].StartMovement();
        }
    }

    public override void StartPart()
    {
        base.StartPart();
        StartAllBigFish();
    }

    void Timer()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= tutoPartTime)
        {
            EndPart();
        }
    }
}
