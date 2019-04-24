using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class TutorialPart : MonoBehaviour
{
    [ReadOnly]
    public bool isFinished;
    [ReadOnly]
    public bool isStarted;

    [Button]
    public virtual void StartPart()
    {
        isStarted = true;
    }

    [Button]
    public virtual void EndPart()
    {
        isFinished = true;
    }
}
