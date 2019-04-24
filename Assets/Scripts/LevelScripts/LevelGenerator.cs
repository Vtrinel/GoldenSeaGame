using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class LevelGenerator : MonoBehaviour
{
    
    #region Fields

    //[ReorderableList]
    [ValidateInput("IsTimelineRight", "Timeline's length must be equal to spawnGroups's length")]
    public SpawnGroup[] spawnGroups;
    [ReorderableList]
    public float[] timeline;

    private bool IsTimelineRight(SpawnGroup[] _spawnGroups)
    {
        if(_spawnGroups.Length == timeline.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [Space(15)]
    [BoxGroup("Duration values")]
    [Tooltip("La durée du niveau en secondes")]
    [MinValue(10),MaxValue(200)]
    public float levelDuration = 150; //durée du niveau en secondes

    [BoxGroup("Duration values")]
    [Tooltip("Permet d'afficher le temps écoulé depuis le début du niveau")]
    [ReadOnly]
    public float currentDuration; //temps écoulé depuis le début du niveau
    [BoxGroup("Duration values")]
    [Tooltip("Permet d'afficher le temps écoulé depuis le début du niveau")]
    public bool beginOnStart = true; //temps écoulé depuis le début du niveau

    [Space(15)]
    [Tooltip("index de départ des SpawnGroup")]
    public int currentSpawnIndex = 0;

    private bool canSpawn = false; //permet de spawn
    private bool canCount = false; //permet de faire aller le countdown

    [HideInInspector]
    public bool levelCompleted = false; //pemet de savoir si le niveau est fini

    [Header("Level's events")]
    public UnityEvent levelBeginEvent;
    public UnityEvent levelEndEvent;

    #endregion

    void Start()
    {
        if (beginOnStart)
        {
            StartLevel();
        }

    }

    public void StartLevel()
    {
        levelBeginEvent.Invoke();
        canCount = true;
        canSpawn = true;
    }

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (!canCount) return;

        currentDuration += Time.deltaTime; //countdown

        //Check spawn
        if (canSpawn) 
        {
            if (currentDuration >= timeline[currentSpawnIndex])
            {
                StartSpawnGroup(); //Spawn
                IncrementIndex(); //index increment
            }
        }

        //check end level
        if(currentDuration >= levelDuration)
        {
            EndLevel();
            canCount = false;
        }
    }

    void StartSpawnGroup()
    {
        spawnGroups[currentSpawnIndex].startEvent.Invoke(); //start event of current group
        spawnGroups[currentSpawnIndex].SpawnObjects(); //start moving
    }

    void IncrementIndex()
    {
        currentSpawnIndex++;

        if(currentSpawnIndex > spawnGroups.Length - 1)
        {
            canSpawn = false;
            Debug.Log("End of spawn");
        }
    }

    void EndLevel()
    {
        levelEndEvent.Invoke(); //END EVENT
        GameManager.gameManager.FinishLevel(true); //Met fin au niveau
        Debug.Log("END OF LEVEL");
    }

    public void CancelLevel()
    {
        canCount = false;
        canSpawn = false;
    }
    
    /// <summary>
    /// Permet d'avoir la durée approximative du niveau
    /// </summary>
    [ContextMenu("Get level duration")]
    void GetLevelDuration()
    {
        float d = timeline[timeline.Length-1];
        Debug.Log("The level duration is equal to : " + d);
    }

}
