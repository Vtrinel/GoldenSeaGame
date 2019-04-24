using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NetDetectionDebug : MonoBehaviour
{

    public NetDetection netDetection;
    private NetLinecast[] netLinecasts;

    [Header("Raycast debug")]
    [SerializeField] Color raycastColor = Color.red;
    [SerializeField] Color raycastPlayColor = Color.green;
    Color drawColor;
    [SerializeField] bool showRaycast = true;
    [SerializeField] KeyCode showRaycastInput = KeyCode.R;

    void Start()
    {
        if(netDetection == null)
        {
            netDetection = GetComponent<NetDetection>();
        }

        netLinecasts = netDetection.linecasts;
    }

    void Update()
    {
        CheckDebugRaycast();
    }

    /// <summary>
    /// Permet de gérer l'affichage ou recevoir un input pour afficher les raycast
    /// </summary>
    void CheckDebugRaycast()
    {
        //COLOR CORRECTION
        if (Application.IsPlaying(this.gameObject))
        {
            drawColor = raycastPlayColor;
        }
        else
        {
            drawColor = raycastColor;
        }

        if (showRaycast)
        {
            DebugRaycast();
        }

        if (Input.GetKeyDown(showRaycastInput))
        {
            DebugRaycast(1.0f);
        }
    }

    /// <summary>
    /// Permet de draw les raycast
    /// </summary>
    void DebugRaycast()
    {
        for (int i = 0; i < netLinecasts.Length; i++)
        {
            Debug.DrawLine(netLinecasts[i].startPoint.position, netLinecasts[i].endPoint.position, drawColor, 0.01f, false);
        }
    }

    /// <summary>
    /// Permet de draw les raycast sur une durée limité
    /// </summary>
    /// <param name="drawDuration"></param>
    void DebugRaycast(float drawDuration)
    {
        for (int i = 0; i < netLinecasts.Length; i++)
        {
            Debug.DrawLine(netLinecasts[i].startPoint.position, netLinecasts[i].endPoint.position, drawColor, drawDuration, false);
        }
    }
}
