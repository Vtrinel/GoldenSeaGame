using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatchTutorialPart : TutorialPart
{
    public bool catchFirstFishCompleted;
    public bool catchManyFishCompleted;

    public GoldenFish goldenFish;
    public GoldenFish[] goldenFishes;

    [Header("Event")]
    public UnityEvent startFirstFishEvent;
    public UnityEvent endFirstFishEvent, startManyFishEvent,endManyFishEvent;

    public float interPracticeTime;
    public enum Practice
    {
        CatchFirstFish,
        CatchManyFish,
        Interlude
    }

    public Practice currentPractice;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (isFinished) return;

        if (!isStarted) return;
        PracticeSelection();
    }

    public override void StartPart()
    {
        base.StartPart();
        StartPractice(Practice.CatchFirstFish);
    }

    void PracticeSelection()
    {
        switch (currentPractice)
        {
            case Practice.CatchFirstFish:
                FishCaptured();
                break;
            case Practice.CatchManyFish:
                ManyFishCaptured();
                break;
        }
    }

    void FishCaptured()
    {
        if (goldenFish.isCaptured)
        {
            catchFirstFishCompleted = true;
            endFirstFishEvent.Invoke();
            StartCoroutine(NextPractice(Practice.CatchManyFish));
        }
    }

    void ManyFishCaptured()
    {
        int count = 0;

        for (int i = 0; i < goldenFishes.Length; i++)
        {
            if (goldenFishes[i].isCaptured)
            {
                count++;
            }
        }

        if(count == goldenFishes.Length)
        {
            catchManyFishCompleted = true;
            endManyFishEvent.Invoke();
            EndPart();
        }
    }

    public void StartPractice(Practice _newPractive)
    {
        switch (_newPractive)
        {
            case Practice.CatchFirstFish:
                currentPractice = _newPractive;
                startFirstFishEvent.Invoke();
                break;
            case Practice.CatchManyFish:
                currentPractice = _newPractive;
                startManyFishEvent.Invoke();
                break;
            case Practice.Interlude:
                currentPractice = _newPractive;
                break;
        }
    }

    IEnumerator NextPractice(Practice _nextPractice)
    {
        currentPractice = Practice.Interlude;
        yield return new WaitForSeconds(interPracticeTime);
        StartPractice(_nextPractice);
    }

    public void StartAllFishes()
    {
        for (int i = 0; i < goldenFishes.Length; i++)
        {
            goldenFishes[i].goldenFishMovement.StartMovement();
        }
    }
}
