using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    public NetDetection[] netDetection;

    public void DisableAllNets()
    {
        for (int i = 0; i < netDetection.Length; i++)
        {
            netDetection[i].enabled = false;
        }
    }
}
