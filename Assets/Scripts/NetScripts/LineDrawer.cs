using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineDrawer : MonoBehaviour
{

    public Transform[] transforms;
    public Material lineMaterial;

    private LineRenderer lineRenderer;
    public float numCap = 30, numCorner = 30;


    void Start()
    {
        this.gameObject.AddComponent<LineRenderer>();
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();

        lineRenderer.material = lineMaterial;
        lineRenderer.positionCount = transforms.Length;
        lineRenderer.numCapVertices = 30;
        lineRenderer.numCornerVertices = 30;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < transforms.Length; i++)
        {
            
            lineRenderer.SetPosition(i, transforms[i].position);
        }

        
    }
}
