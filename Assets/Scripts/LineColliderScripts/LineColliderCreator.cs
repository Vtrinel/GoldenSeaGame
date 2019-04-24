using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineColliderCreator : MonoBehaviour
{
    public GameObject collidersParent;
    public LineCollider[] lineColliders;
    public LineColliderBehaviour lineColliderBehaviour;

    [Header("Start creation")]
    public bool startCreation = false;

    
    void Update()
    {
        if(startCreation && lineColliders.Length > 0 && lineColliderBehaviour != null)
        {
            AddCollidersToLine(lineColliders);
            AssignLineCollider();
            startCreation = false;
        }
    }

    void AddCollidersToLine(LineCollider[] line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            //create the collider for the line
            BoxCollider boxCollider = new GameObject("LineCollider_" + i).AddComponent<BoxCollider>();

            //set the collider as a child
            boxCollider.transform.parent = collidersParent.transform;

            //add collider to the class
            line[i].collider = boxCollider;

            boxCollider.isTrigger = true; //TEST !!! A ENLEVER LUS TARD !!!
        }
    }

    void AssignLineCollider()
    {
        lineColliderBehaviour.colliders = lineColliders;
    }
}
