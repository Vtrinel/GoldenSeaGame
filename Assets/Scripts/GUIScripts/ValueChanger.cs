using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;

public class ValueChanger : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float targetValue;
    public float currentValue;
    public float smoothDuration;

    private float refValue;
    [ReadOnly]
    public bool isIncreasing;

    void Start()
    {
        UpdateText(0);
    }


    void Update()
    {
        if (isIncreasing)
        {
            Increaser();
        }    
    }

    [Button("Test Increaser")]
    public void Increaser()
    {
        isIncreasing = true;
        currentValue = Mathf.SmoothDamp(currentValue, targetValue, ref refValue, smoothDuration);
        UpdateText(currentValue);

        
        bool stopIncreaser = RoundedValuesAreEqual(currentValue, targetValue);

        if (stopIncreaser)
        {
            isIncreasing = false;
        }
    }

    bool RoundedValuesAreEqual(float _a, float _b)
    {
        _a = Mathf.Round(_a);
        _b = Mathf.Round(_b);

        if(_a == _b)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateText(float _newValue)
    {
        _newValue = Mathf.RoundToInt(_newValue);
        text.text = _newValue.ToString();
    }
}
