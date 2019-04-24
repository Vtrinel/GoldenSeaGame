using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; //instance du gameManager

    [Header("References")]
    public BoatManager boatManager;
    public PlayerCale playerCale;
    public ScoreManager scoreManager;
    public LevelManager levelManager;
    public InputManager inputManager;
    public NetManager netManager;
    public NetShakerManager netShakerManager;
    public TimeManager timeManager;
    public VFXManager fXManager;

    [Header("GUI References")]
    public CaleUI caleUI;
    public ResultWindowManager resultWindowManager;
    public GameOverGUIManager gameOverGUI;

    public float maxScore;

    public bool canDamagePlayer = true;


    private void Awake()
    {
        gameManager = this;
    }

    void Start()
    {

    }

    /// <summary>
    /// Permet de mettre fin au niveau
    /// </summary>
    /// <param name="_playerWin">victoire ou défaite du joueur</param>
    public void FinishLevel(bool _playerWin)
    {
        if (_playerWin)
        {
            scoreManager.ConvertScoreToGold();
            maxScore = PlayerPrefs.GetFloat("MaxScore", 0);
            if (GameManager.gameManager.playerCale.caleValue > maxScore)
            {
                PlayerPrefs.SetFloat("MaxScore", GameManager.gameManager.playerCale.caleValue);
                maxScore = GameManager.gameManager.playerCale.caleValue;
            }
            SetValuesForResult();
            resultWindowManager.StartResult();
        }
        else
        {
            //Stop levels
            levelManager.selectedLevel.CancelLevel();
            //Disable capture scripts
            netManager.DisableAllNets();
            //Animations
            boatManager.boatAnimations.PlaySinkAnimation();

        }
    }

    private void SetValuesForResult()
    {
        //Set all values
        resultWindowManager.levelGoldValue.currentValue = 0;
        resultWindowManager.levelGoldValue.targetValue = scoreManager.playerLevelGoldAtEnd;
        resultWindowManager.totalGoldValue.currentValue = scoreManager.playerGoldAtBegin;
        resultWindowManager.totalGoldValue.targetValue = scoreManager.playerGoldAtEnd;
        resultWindowManager.RecordGoldValue.currentValue = 0;
        resultWindowManager.RecordGoldValue.targetValue = maxScore;

        resultWindowManager.totalGoldValue.UpdateText(scoreManager.playerGoldAtBegin); //Update value on screen
    }

    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Get Current scene
        SceneManager.LoadScene(currentSceneIndex); //reload current scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
