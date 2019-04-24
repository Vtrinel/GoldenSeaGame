using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour
{
    [Header("References")]
    public Animator anim;
    public GameObject leftCircle;
    public GameObject rightCircle; 

    public UnityEvent startGameEvent;

    void Update()
    {
        SpritesCircle();

        if (CanStartGame())
        {
            GameManager.gameManager.levelManager.selectedLevel.StartLevel();
            anim.SetTrigger("disappear");
            startGameEvent.Invoke();
            this.enabled = false;
        }
    }

    bool CanStartGame()
    {
        if(GameManager.gameManager.inputManager.leftBoatScript.HasTarget && 
            GameManager.gameManager.inputManager.rightBoatScript.HasTarget)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SpritesCircle()
    {
        if (GameManager.gameManager.inputManager.leftBoatScript.HasTarget)
        {
            leftCircle.SetActive(false);
        }
        else
        {
            leftCircle.SetActive(true);
            leftCircle.transform.position = GameManager.gameManager.inputManager.leftBoatScript.fingerOnScreenSprite.transform.position;
        }

        if (GameManager.gameManager.inputManager.rightBoatScript.HasTarget)
        {
            rightCircle.SetActive(false);
        }
        else
        {
            rightCircle.SetActive(true);
            rightCircle.transform.position = GameManager.gameManager.inputManager.rightBoatScript.fingerOnScreenSprite.transform.position;
        }
    }
}
