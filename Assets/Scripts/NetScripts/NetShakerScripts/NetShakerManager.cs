using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class NetShakerManager : MonoBehaviour
{
    public List<NetShaker> netShakers;
    [Slider(0.01f, 1.0f)]
    public float shakeDecrement = 0.2f;
    public float shakeTimeOffset = 0.1f;

    [Header("Debug")]
    public int debugValue;

    List<NetShaker> underList = new List<NetShaker>();
    List<NetShaker> aboveList = new List<NetShaker>();

    [Button]
    void ShakeTest()
    {
        
        Shake(debugValue);
    }

    NetShaker GetUnder(int _initialShakeIndex)
    {
        int targetIndex = _initialShakeIndex - 1;

        if (targetIndex > 0)
        {
            return netShakers[targetIndex];
        }
        else
        {
            return null;
        }

    }

    List<NetShaker> GetAllUnder(int _initialShakeIndex)
    {
        List<NetShaker> shakers = new List<NetShaker>();

        int targetIndex = _initialShakeIndex - 1;

        for (int i = targetIndex; i >= 0; i--)
        {
            shakers.Add(netShakers[i]);
        }

        return shakers;
    }

    List<NetShaker> GetAllAbove(int _initialShakeIndex)
    {
        List<NetShaker> shakers = new List<NetShaker>();

        int targetIndex = _initialShakeIndex + 1;

        for (int i = targetIndex; i < netShakers.Count; i++)
        {
            shakers.Add(netShakers[i]);
        }

        return shakers;
    }

    /// <summary>
    /// Permet de déclencher le shaking a l'endroit approprié
    /// </summary>
    /// <param name="_capturedObject"></param>
    /// <param name="_shakeAmplitude"></param>
    public void StartShaking(Transform _capturedObject, float _shakeAmplitude = 1)
    {
        //Calculate all distances
        List<float> distances = new List<float>();

        for (int i = 0; i < netShakers.Count; i++)
        {
            float distance = Vector2.Distance(_capturedObject.position, netShakers[i].transform.position);
            distances.Add(distance);
        }

        //Get smallest distance
        int smallestDistanceIndex = distances.IndexOf(distances.Min());

        //Start Shake
        Shake(smallestDistanceIndex,_shakeAmplitude);
    }

    /// <summary>
    /// Trigger le net shaking
    /// </summary>
    /// <param name="_startShakerIndex"></param>
    /// <param name="_shakeAmplitudePercent"></param>
    public void Shake(int _startShakerIndex,float _shakeAmplitudePercent = 1)
    {
        //update lists
        underList = GetAllUnder(_startShakerIndex);
        aboveList = GetAllAbove(_startShakerIndex);

        netShakers[_startShakerIndex].Shake(netShakers[_startShakerIndex].shakeAmplitude * _shakeAmplitudePercent);

        StartCoroutine(ShakeAList(underList, shakeTimeOffset, true,_shakeAmplitudePercent));
        StartCoroutine(ShakeAList(aboveList, shakeTimeOffset, true,_shakeAmplitudePercent));
    }

    /// <summary>
    /// permet de shake une liste de net shakers avec un effet de dissipation
    /// </summary>
    /// <param name="_shakers"></param>
    /// <param name="_timeOffset"></param>
    /// <param name="_waitBeforeFirstShake"></param>
    /// <param name="_shakeAmplitude"></param>
    /// <returns></returns>
    public IEnumerator ShakeAList(List<NetShaker> _shakers, float _timeOffset, bool _waitBeforeFirstShake, float _shakeAmplitude = 1)
    {
        if (_waitBeforeFirstShake)
        {
            yield return new WaitForSeconds(_timeOffset);
        }

        for (int i = 0; i < _shakers.Count; i++)
        {
            float decrement = shakeDecrement * (i + 1);
            _shakers[i].Shake(_shakers[i].shakeAmplitude * _shakeAmplitude - decrement);
            yield return new WaitForSeconds(_timeOffset);
        }
    }
}
