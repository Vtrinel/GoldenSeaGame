using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelGenerator[] levelGenerators;
    public LevelGenerator selectedLevel;

    private void Awake()
    {
        SelectOneLevel();
    }

    public void SelectOneLevel()
    {
        int ran = Random.Range(0, levelGenerators.Length);
        selectedLevel = levelGenerators[ran];

        foreach (LevelGenerator level in levelGenerators)
        {
            if (level != selectedLevel)
            {
                level.enabled = false;
            }
        }
    }
}
