using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class ResultWindowManager : MonoBehaviour
{

    [BoxGroup("Reference")]
    public ValueChanger levelGoldValue;
    [BoxGroup("Reference")]
    public ValueChanger RecordGoldValue;
    [BoxGroup("Reference")]
    public ValueChanger totalGoldValue;
    [BoxGroup("References")]
    public Animator animator;
    [BoxGroup("References")]
    public Button[] buttons;

    [BoxGroup("Timeline Result Values")]
    public float timeBeforeWindowAppear = 1, timeBeforeLevelGoldResult = 1, timeBeforeRecordGoldResult = 1, timeBeforeTotalGoldResult = 1,timeBeforeButtonAppear = 1;

    private void Start()
    {
        SetButtonsActive(false);
    }

    [Button("Test Result")]
    public void StartResult()
    {
        StartCoroutine(ResultTimeline());
    }

    public void SetButtonsActive(bool _active)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = _active;
        }
    }

    IEnumerator ResultTimeline()
    {
        //MESSAGE VICTOIRE
        animator.SetTrigger("WinTextAppear");
        yield return new WaitForSeconds(timeBeforeWindowAppear);
        //APPARITION FENETRE
        animator.SetTrigger("WindowAppear");
        yield return new WaitForSeconds(timeBeforeLevelGoldResult);
        //Agmentation du score
        levelGoldValue.Increaser();
        yield return new WaitForSeconds(timeBeforeTotalGoldResult);
        RecordGoldValue.Increaser();
        yield return new WaitForSeconds(timeBeforeRecordGoldResult);
        //augmentation ressource or
        totalGoldValue.Increaser();
        yield return new WaitForSeconds(timeBeforeButtonAppear);
        //interaction possible
        SetButtonsActive(true);
    }
}
