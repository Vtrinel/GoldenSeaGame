using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] ParticleSystem impactVfx;
    [SerializeField] ParticleSystem smokeFx;

    public void PlayImpactVFX(Vector3 _positionToPlayImpact)
    {
        impactVfx.transform.position = _positionToPlayImpact;
        impactVfx.Play(true);
    }

    public void PlaySmokeVFX(Vector3 _positionToPlayImpact)
    {
        smokeFx.transform.position = _positionToPlayImpact;
        smokeFx.Play(true);
    }


}
