using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColliderBehaviour : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LineCollider[] colliders;
    public float colliderWidth = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Get lineCollider
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            AdjustCollider(colliders[i].collider, lineRenderer, colliders[i].startPosition.position, colliders[i].endPosition.position);
        }
    }

    void AdjustCollider(BoxCollider lineCollider, LineRenderer line, Vector3 startPoint, Vector3 endPoint)
    {
        //SHAPE
        float lineWidth = line.endWidth; // get width from line
        float lineLength = Vector3.Distance(startPoint, endPoint); // get the length
        lineCollider.size = new Vector3(colliderWidth, lineWidth, lineLength); // size of collider is set where Z is length of line

        //POSITION
        Vector3 midPoint = (startPoint + endPoint) / 2; // get midPoint
        lineCollider.transform.position = midPoint; // set collider to the midPoint

        //ROTATION
        Quaternion rotation = Quaternion.LookRotation(startPoint - endPoint, Vector3.forward); //get direction of rotation
        lineCollider.transform.rotation = rotation; //apply rotation
    }
}
