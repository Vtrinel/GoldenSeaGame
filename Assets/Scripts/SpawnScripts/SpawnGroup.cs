using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class SpawnGroup : MonoBehaviour
{
    
    public List<SpawnableObject> spawnableObjects;

    [Header("Events")]
    public UnityEvent startEvent;
    public UnityEvent endEvent;


    public void SpawnObjects()
    {
        for (int i = 0; i < spawnableObjects.Count; i++)
        {
            spawnableObjects[i].Spawn(spawnableObjects[i].transform.position);
        }
    }
}
