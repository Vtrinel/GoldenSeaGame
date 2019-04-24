using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class CaleUI : MonoBehaviour
{
    public Image fishBar;
    [SerializeField] private Image lostCaleBar;
    [SerializeField] private Image firstMarker;
    [SerializeField] private Image secondMarker;

    public float fillTime = 0.25f;
    private float maxBarValue; //le maximum possible de la cale
    private float fillTargetValue;
    float refFill;

    [Header("VFX")]
    [Range(0.001f,0.5f)]
    public float warningDiff = 0.1f;
    public ParticleSystem almostFullFx;
    public ParticleSystem HighlightFx;

    float firstLimitPercent, secondLimitPercent;

    [SerializeField] ParticleSystem caleDamageFX;
    public Transform position0;
    public Transform position1;
    public Transform position2;

    void Start()
    {
        PlaceMarker();
        UpdateFishStorage();
        
    }

    void Update()
    {
        fishBar.fillAmount = Mathf.SmoothDamp(fishBar.fillAmount, fillTargetValue, ref refFill, fillTime);
        LostCaleBarFill();
        CaleWarning();
    }

    public void UpdateFishStorage()
    {
        fillTargetValue = GameManager.gameManager.playerCale.caleVolume / maxBarValue;
        
    }

    public void TakeDamageUI()
    {

    }

    void CaleWarning()
    {
        switch (GameManager.gameManager.playerCale.currentCaleIndex)
        {
            case 0:
                CheckWarning(1);
                break;
            case 1:
                CheckWarning(firstLimitPercent);
                break;
            case 2:
                CheckWarning(secondLimitPercent);
                break;
            default:
                CheckWarning(1);
                break;
        }
    }

    public void PlayHighlightFX()
    {
        HighlightFx.Play();
    }

    void CheckWarning(float _limit)
    {
        if(fillTargetValue > _limit - warningDiff)
        {
            if (!almostFullFx.isPlaying)
            {
                almostFullFx.Play(true);
            }
        }
        else
        {
            if (almostFullFx.isPlaying)
            {
                almostFullFx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }

    void LostCaleBarFill()
    {
        if(GameManager.gameManager.playerCale.currentCaleIndex > GameManager.gameManager.playerCale.cale.Count - 1)
        {
            lostCaleBar.fillAmount = 1;
        }
        else
        {
            lostCaleBar.fillAmount = 1 - GameManager.gameManager.playerCale.cale[GameManager.gameManager.playerCale.currentCaleIndex] / maxBarValue;
        }

       
    }

    void PlaceMarker()
    {
        maxBarValue = GameManager.gameManager.playerCale.initialMaxCaleValue;

        float firstMarkerPercent = GameManager.gameManager.playerCale.cale[1] / maxBarValue;
        float secondMarkerPercent = GameManager.gameManager.playerCale.cale[2] / maxBarValue;

        firstLimitPercent = firstMarkerPercent;
        secondLimitPercent = secondMarkerPercent;

        float firstY = fishBar.rectTransform.sizeDelta.y * firstMarkerPercent;
        firstMarker.rectTransform.localPosition = new Vector2(firstMarker.rectTransform.localPosition.x, firstY);

        float secondY = fishBar.rectTransform.sizeDelta.y * secondMarkerPercent;
        secondMarker.rectTransform.localPosition = new Vector2(secondMarker.rectTransform.localPosition.x, secondY);

    }

    public Vector3 GetBarTopPosition()
    {
        Vector3[] v = new Vector3[4];
        fishBar.rectTransform.GetWorldCorners(v);

        float height = Vector3.Distance(v[0], v[1]);
        float width = Vector3.Distance(v[0], v[3]);

        Vector3 start = new Vector3(v[0].x + width / 2, v[0].y, v[0].z);
        Vector3 end = new Vector3(v[1].x + width / 2, v[1].y, v[1].z);

        Vector3 targetPosition = Vector3.Lerp(start, end, fishBar.fillAmount);

        return targetPosition;
    }

    public Vector3 GetMaxTopPosition()
    {
        Vector3 pos = Vector3.zero;

        switch (GameManager.gameManager.playerCale.currentCaleIndex)
        {
            case 0:
                pos = position0.position;
                return pos;
            case 1:
                pos = position1.position;
                return pos;
            case 2:
                pos = position2.position;
                return pos;
        }

        return pos;
    }

    public void PlayCaleTakeDamageVFX()
    {
        Vector3 pos = GetMaxTopPosition();
        Vector3 fxPos = new Vector3(caleDamageFX.transform.position.x, pos.y, pos.z);

        caleDamageFX.transform.position = fxPos;
        caleDamageFX.Play(true);
    }
}
