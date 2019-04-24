using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetLinecast
{
    public Transform startPoint, endPoint; //point de départ et d'arrivé du raycast
    [HideInInspector]
    public LayerMask targetLayer;
    public List<ICatchObject> catchObjects = new List<ICatchObject>();

    public bool IsTouchingSomething()
    {
        
        Vector3 raycastDirection = endPoint.position - startPoint.position;
        float raycastDistance = Vector2.Distance(startPoint.position, endPoint.position);
        RaycastHit[] touchedObjects = Physics.RaycastAll(startPoint.position, raycastDirection, raycastDistance, targetLayer,QueryTriggerInteraction.Collide);

        //check if there's any collision
        bool collision = false;
        if (touchedObjects.Length > 0) collision = true;

        if (collision)
        {
            //Clear IcatchObject list
            catchObjects.Clear();
            catchObjects = new List<ICatchObject>();

            for (int i = 0; i < touchedObjects.Length; i++)
            {
                ICatchObject catchObject = touchedObjects[i].transform.GetComponent<ICatchObject>(); //Get capturable object

                if(catchObject != null)
                    catchObjects.Add(catchObject);
            }
        }
        

        return collision;
    }
}


public class NetDetection : MonoBehaviour
{

    [Header("Raycast detection")]
    public NetLinecast[] linecasts; //configuration des raycast
    [Tooltip("Les layer de collision détectable par les raycast")]
    public LayerMask sensitiveLayers;


    private void Start()
    {
        LineCastInitialize();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Detection();
    }


    /// <summary>
    /// Permet d'initialiser tous les LayerMask de chaque raycast
    /// </summary>
    void LineCastInitialize()
    {
        for (int i = 0; i < linecasts.Length; i++)
        {
            linecasts[i].targetLayer = sensitiveLayers;
        }
    }

    /// <summary>
    /// Fonction qui détecte et joue la fonction de capture des objet attrapable
    /// </summary>
    public virtual void Detection()
    {
        for (int i = 0; i < linecasts.Length; i++)
        {
            if (linecasts[i].IsTouchingSomething())
            {
                Debug.Log("Touched somehting !");

                if (linecasts[i].catchObjects.Count > 0)
                {
                    for (int j = 0; j < linecasts[i].catchObjects.Count; j++)
                    {
                        linecasts[i].catchObjects[j].Catch(); //Play catch function via interface
                    }

                    //Reset et clear la list
                    linecasts[i].catchObjects.Clear();
                    linecasts[i].catchObjects = new List<ICatchObject>();
                }
            }
        }
    }


}
