using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using SceneBuilderWpf.DataModels;

public class ParticlePlayer : MonoBehaviour
{

    public UnityEngine.Object FoamParticle;
    public UnityEngine.Object FireParticle;
    public UnityEngine.Object SmokeParticle;

    List<UnityEngine.Object> _particleInstance = new List<UnityEngine.Object>();
    public void CreateParticle(Actions action, Vector3 particlePos, float intensity)
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
                _particleInstance.Add(Instantiate(FoamParticle, particlePos, Quaternion.identity));
            break;
        }
    }

    public void ClearParticle()
    {
        foreach(var particle in _particleInstance)
        {
            Destroy(particle);
        }

    }


}

