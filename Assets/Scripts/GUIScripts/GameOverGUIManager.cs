using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverGUIManager : MonoBehaviour
{
    public Animator anim;

    public void CanvasAppear()
    {
        anim.SetTrigger("appear");
    }


}
