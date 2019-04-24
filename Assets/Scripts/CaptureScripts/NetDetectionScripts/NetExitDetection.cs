using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetExitDetection : NetDetection
{
    public override void Detection()
    {
        for (int i = 0; i < linecasts.Length; i++)
        {
            if (linecasts[i].IsTouchingSomething())
            {

                if (linecasts[i].catchObjects.Count > 0)
                {
                    for (int j = 0; j < linecasts[i].catchObjects.Count; j++)
                    {
                        linecasts[i].catchObjects[j].InsideNet(false); //Play catch function via interface
                    }

                    //Reset et clear la list
                    linecasts[i].catchObjects.Clear();
                    linecasts[i].catchObjects = new List<ICatchObject>();
                }
            }
        }
    }
}
