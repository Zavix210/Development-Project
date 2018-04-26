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

    public UnityEngine.GameObject FoamParticle;
    public UnityEngine.GameObject FireParticle;
    public UnityEngine.GameObject SmokeParticle;

    List<UnityEngine.GameObject> _particleInstance = new List<UnityEngine.GameObject>();
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
            case Actions.Extingishuer:
                var particle = Instantiate(FoamParticle, new Vector3(0.0f, -1.37f, 0.6f), Quaternion.identity);
                particle.transform.parent = Camera.main.transform;
                particle.transform.rotation = Camera.main.transform.rotation;
                particle.transform.position = new Vector3(0.0f, -1.37f, 0.6f);
                _particleInstance.Add(particle);
                IEnumerator coroutine = StartTimedParticle(particle,time);
                StartCoroutine(coroutine);
            break;
        }
    }

    private IEnumerator StartTimedParticle(UnityEngine.GameObject particle,float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(particle);
    }
    public void ClearParticle()
    {
        if (_particleInstance == null)
            return;
        foreach(var particle in _particleInstance)
        {
            Destroy(particle);
        }

    }

    private void Update()
    {

    }


}

