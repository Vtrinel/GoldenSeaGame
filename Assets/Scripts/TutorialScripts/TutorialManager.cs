using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public TutorialPart[] tutorialParts;
    public float[] timeBeforeNextPart;
    public UnityEvent[] eventsBetwenNextPart;

    public int currentTutorialIndex = 0;
    bool inTransition = false;
    bool endTutorial;

    private void Start()
    {
        StartTutorial();
    }

    private void Update()
    {
        if (endTutorial) return;

        if (tutorialParts[currentTutorialIndex].isFinished && !inTransition)
        {
            StartCoroutine(NextTuto());
        }
    }

    [Button]
    void StartTutorial()
    {
        tutorialParts[currentTutorialIndex].StartPart();
    }

    public IEnumerator NextTuto()
    {
        inTransition = true;
        eventsBetwenNextPart[currentTutorialIndex].Invoke();
        yield return new WaitForSeconds(timeBeforeNextPart[currentTutorialIndex]);
        currentTutorialIndex++;

        if(currentTutorialIndex > tutorialParts.Length - 1)
        {
            Debug.Log("END TUTORIAL");
            endTutorial = true;
            EndTutorial();
            yield break;
        }

        tutorialParts[currentTutorialIndex].StartPart();
        inTransition = false;
    }

    void EndTutorial()
    {
        SceneTransition.sceneTransition.LoadNextScene(); //return to main menu
    }

}
