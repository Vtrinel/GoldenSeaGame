using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameHUDManager : MonoBehaviour
{
    [SerializeField] Animator[] animators;
    public bool appearOnStart = false;
    
    void Start()
    {
        if (appearOnStart)
        {
            HUDAppear();
        }
    }

    [Button]
    public void HUDAppear()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetTrigger("appear");
        }
    }
}
