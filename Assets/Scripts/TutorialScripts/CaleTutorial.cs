using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CaleTutorial : TutorialPart
{
    public float holdBarTextTime;
    float currentTime;


    public GoldenFish[] bigGoldenFishes;

    [Header("Event")]
    public UnityEvent startHoldBarTextEvent;
    public UnityEvent endHoldBarTextEvent, startOverfillBarEvent, endOverfillBarEvent, startCatchObstacleEvent, endCatchObstacleEvent;

    public float interPracticeTime;

    public enum Practice
    {
        HoldBarText,
        OverfillBar,
        CatchObstacle,
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

    public void StartPractice(Practice _newPractive)
    {
        switch (_newPractive)
        {
            case Practice.HoldBarText:
                currentPractice = _newPractive;
                startHoldBarTextEvent.Invoke();

                break;
            case Practice.OverfillBar:
                currentPractice = _newPractive;
                startOverfillBarEvent.Invoke();

                break;
            case Practice.CatchObstacle:
                currentPractice = _newPractive;
                startCatchObstacleEvent.Invoke();

                break;
        }
    }

    IEnumerator NextPractice(Practice _nextPractice)
    {
        currentPractice = Practice.Interlude;
        yield return new WaitForSeconds(interPracticeTime);
        StartPractice(_nextPractice);
    }

    void PracticeSelection()
    {
        switch (currentPractice)
        {
            case Practice.HoldBarText:
                HoldBarText();
                break;
            case Practice.OverfillBar:
                OverfillBar();
                break;
            case Practice.CatchObstacle:
                Catch();
                break;
        }
    }

    public void StartAllBigFish()
    {
        for (int i = 0; i < bigGoldenFishes.Length; i++)
        {
            bigGoldenFishes[i].goldenFishMovement.StartMovement();
        }
    }

    public override void StartPart()
    {
        base.StartPart();
        StartPractice(Practice.HoldBarText);
    }

    void HoldBarText()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= holdBarTextTime)
        {
            endHoldBarTextEvent.Invoke();
            StartCoroutine(NextPractice(Practice.OverfillBar));
        }
    }

    void OverfillBar()
    {
        if (!GameManager.gameManager.canDamagePlayer)
        {
            endOverfillBarEvent.Invoke();
            StartCoroutine(NextPractice(Practice.CatchObstacle));
        }
    }

    void Catch()
    {
        endOverfillBarEvent.Invoke();
        EndPart();
    }
}
