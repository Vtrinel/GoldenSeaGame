using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFish : GoldenFish
{
    [Header("Flying fish")]
    public GameObject shadow;

    public override void Catch()
    {
        base.Catch();
        shadow.SetActive(false);
    }
}
