using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class PlayerCale : MonoBehaviour
{
    //[ReadOnly]
    public float caleVolume; //le volume de poisson dans la cale
    public TextMeshProUGUI scoreText;

    [ReadOnly]
    public float caleValue; //la valeur totale que représente les poisson dans la cale

    [ReorderableList]
    public List<int> cale;
    public int currentCaleIndex; //Le maximum actuel de la cale
    public int initialMaxCaleValue; //le maximum de la cale au début du jeu



    private void Awake()
    {
        initialMaxCaleValue = cale[0];
    }

    public float CalePercent
    {
        get
        {
            float max = cale[currentCaleIndex]; //get max
            float current = caleValue; //get current
            float percent = current / max; //Get percent of completion
            return percent;
        }
    }

    private void Update()
    {
        UpdateScore();
    }
    public void UpdateScore()
    {
        scoreText.text = "Score : " + caleValue;
    }

    public void CollectFish(float _fishSize, float _fishValue)
    {
        caleVolume += _fishSize; //incrémente le volume
        caleValue += _fishValue; //incrémente la valeur de score

        if (caleVolume > cale[currentCaleIndex])
        {
            ResetCale();
            CaleOvervilled();
            CaleDamage();
        }

        GameManager.gameManager.caleUI.UpdateFishStorage();
    }

    public void CaleDamage()
    {
        GameManager.gameManager.timeManager.StartHitSlowMotion(); //Slow motion
        GameManager.gameManager.caleUI.PlayCaleTakeDamageVFX();
        ResetCale();
        currentCaleIndex++;
        GameManager.gameManager.caleUI.UpdateFishStorage();

        if (currentCaleIndex > cale.Count - 1)
        {
            Debug.Log("GAME OVER");
            GameManager.gameManager.FinishLevel(false);
        }

    }

    public void ResetCale()
    {
        caleVolume = 0;
        caleValue = 0;
    }

    private void CaleOvervilled()
    {
        
    }
}
