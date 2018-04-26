using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using SceneBuilderWpf.DataModels;
using System.Collections;

public class ParticlePlayer : MonoBehaviour
{

    public UnityEngine.Object FoamParticle;
    public UnityEngine.Object FireParticle;
    public UnityEngine.Object SmokeParticle;

    List<UnityEngine.Object> _particleInstance = new List<UnityEngine.Object>();
    public void CreateParticle(Actions action, Vector3 particlePos, float intensity, float time)
    {
        switch (action)
        {
            case Actions.Fire:
                _particleInstance.Add(Instantiate(FireParticle, particlePos,Quaternion.identity));
            break;
            case Actions.Smoke:
                _particleInstance.Add(Instantiate(SmokeParticle, particlePos, Quaternion.identity));
            break;
            case Actions.Timer:
                var particle = Instantiate(FoamParticle, particlePos, Quaternion.Euler(new Vector3(24.8f,100.14f,0.0f)));
                
                _particleInstance.Add(particle);
                IEnumerator coroutine = StartTimedParticle(particle,time);
                StartCoroutine(coroutine);
            break;
        }
    }

    private IEnumerator StartTimedParticle(UnityEngine.Object particle,float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(particle);
    }
    public void ClearParticle()
    {
        foreach(var particle in _particleInstance)
        {
            Destroy(particle);
        }

    }


}

