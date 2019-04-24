using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class ControlTutorialPart : TutorialPart
{
    [Header("Control tutorial values")]
    public bool touchPraticeCompleted;
    public bool movePracticeCompleted;

    public float interPractiveTime = 2;
    public float positionDiff = 5;
    Vector3 leftBoatPos, rightBoatPos;

    [Header("Events")]
    public UnityEvent endTouchBoat;
    public UnityEvent startTouchBoat;
    public UnityEvent endMoveBoat, startMoveBoat;

    public enum Practice
    {
        TouchOnBoat,
        MoveBoat,
        Interlude,
    }

    public Practice currentPractice;

    private void Start()
    {
        leftBoatPos = GameManager.gameManager.inputManager.leftBoat.position;
        rightBoatPos = GameManager.gameManager.inputManager.rightBoat.position;
    }

    private void Update()
    {
        if (isFinished) return;

        if (!isStarted) return;
        PracticeSelection();
    }

    IEnumerator NextPractice(Practice _nextPractice)
    {
        currentPractice = Practice.Interlude;
        yield return new WaitForSeconds(interPractiveTime);
        StartPractice(_nextPractice);
    }

    public void StartPractice(Practice _newPractive)
    {
        switch (_newPractive)
        {
            case Practice.TouchOnBoat:
                currentPractice = _newPractive;
                startTouchBoat.Invoke();
                break;
            case Practice.MoveBoat:
                currentPractice = _newPractive;
                startMoveBoat.Invoke();
                break;
            case Practice.Interlude:
                currentPractice = _newPractive;
                break;
        }
    }

    void PracticeSelection()
    {
        switch (currentPractice)
        {
            case Practice.TouchOnBoat:
                TouchBoat();
                break;
            case Practice.MoveBoat:
                MoveBoat();
                break;
        }
    }

    void TouchBoat()
    {
        if(GameManager.gameManager.inputManager.rightBoatScript.HasTarget && GameManager.gameManager.inputManager.rightBoatScript.HasTarget)
        {
            touchPraticeCompleted = true;
            endTouchBoat.Invoke();
            StartCoroutine(NextPractice(Practice.MoveBoat));
        }
    }

    void MoveBoat()
    {
        bool leftMoved = CustomMethod.AlmostEqual(GameManager.gameManager.inputManager.leftBoat.position, leftBoatPos, positionDiff);
        bool rightMoved = CustomMethod.AlmostEqual(GameManager.gameManager.inputManager.rightBoat.position, rightBoatPos, positionDiff);

        if (!leftMoved && !rightMoved)
        {
            movePracticeCompleted = true;
            endMoveBoat.Invoke();
            EndPart();
        }
    }

    public override void StartPart()
    {
        base.StartPart();
        StartPractice(Practice.TouchOnBoat);
    }
}
