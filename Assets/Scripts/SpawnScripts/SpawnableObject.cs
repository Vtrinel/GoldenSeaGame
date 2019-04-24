using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    public virtual void Spawn(Vector3 spawnPosition)
    {
        gameObject.SetActive(true); //Set active
        transform.position = spawnPosition; //Set position
    }
}
