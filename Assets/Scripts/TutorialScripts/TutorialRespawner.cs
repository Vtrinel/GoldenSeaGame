using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRespawner : MonoBehaviour
{
    public Transform respawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ICatchObject>() != null)
        {
            Vector3 respawnPos = new Vector3(other.transform.position.x, respawnPosition.position.y, other.transform.position.z);

            other.transform.position = respawnPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, respawnPosition.position);
    }
}
