using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class FishValue
{
    public float initialValue = 10;

    /// <summary>
    /// Permet de générer les valeurs
    /// </summary>
    public void GenerateValue()
    {
        bronzeValue = initialValue * 0.5f;
        silverValue = initialValue * 1.0f;
        goldValue = initialValue * 2.0f;
    }

    public float GetValue(GoldenFish.FishType type)
    {
        float value;

        switch (type)
        {
            case GoldenFish.FishType.Bronze:
                value = bronzeValue;
                break;
            case GoldenFish.FishType.Silver:
                value = silverValue;
                break;
            case GoldenFish.FishType.Gold:
                value = goldValue;
                break;
            default:
                value = silverValue;
                break;
        }

        return value;
    }

    float bronzeValue;
    float silverValue;
    float goldValue;
}
