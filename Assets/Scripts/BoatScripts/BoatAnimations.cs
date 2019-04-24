using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAnimations : MonoBehaviour
{
    public Animator animator;

    public void PlaySinkAnimation()
    {
        animator.SetTrigger("sink");
    }
}
