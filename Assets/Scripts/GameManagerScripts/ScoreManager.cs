using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ScoreManager : MonoBehaviour
{
    [ReadOnly]
    public float playerGold = 0;
    [ReadOnly]
    public float playerLevelGoldAtEnd;
    [ReadOnly]
    public float playerGoldAtBegin;
    [ReadOnly]
    public float playerGoldAtEnd;

    public float PlayerGold
    {
        get
        {
            return playerGold;
        }
        set
        {
            playerGold = value;
            //SAVE Gold value
        }
    }

    private void Awake()
    {
        playerGold = playerGold; //Load player gold from saved data
        playerGoldAtBegin = playerGold;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConvertScoreToGold()
    {
        float playerScore = GameManager.gameManager.playerCale.caleValue; //get score
        playerLevelGoldAtEnd = playerScore;
        playerGoldAtEnd = playerGold + playerScore; //set new gold value
        PlayerGold = playerGoldAtEnd;
    }
}
