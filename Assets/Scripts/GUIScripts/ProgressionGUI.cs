using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class ProgressionGUI : MonoBehaviour
{
    public Image niddleSprite;
    [MinMaxSlider(-360, 360)]
    public Vector2 angleAmplitude;
    [ReadOnly]
    public float minAngle, maxAngle;

    

    void Start()
    {
        SetAngles();
    }

    [Button("Set Angles")]
    private void SetAngles()
    {
        minAngle = angleAmplitude.x;
        maxAngle = angleAmplitude.y;
    }

    [Button("Set Rotation")]
    void SetNiddleRotation()
    {
        //get progress percent
        float progressPercent = GameManager.gameManager.levelManager.selectedLevel.currentDuration / GameManager.gameManager.levelManager.selectedLevel.levelDuration;
        float rot = Mathf.Lerp(minAngle, maxAngle, progressPercent);

        niddleSprite.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, rot));
    }

    // Update is called once per frame
    void Update()
    {
        SetNiddleRotation();
    }
}
