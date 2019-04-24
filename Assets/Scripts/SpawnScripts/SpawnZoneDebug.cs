using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneDebug : MonoBehaviour
{
    public Transform start, end;
    public Color lineColor = Color.yellow;

    private void OnDrawGizmos()
    {
        Vector3 direction = end.position - start.position;
        float distance = Vector3.Distance(start.position, end.position);
        Debug.DrawRay(start.position, direction, lineColor);
    }
}
