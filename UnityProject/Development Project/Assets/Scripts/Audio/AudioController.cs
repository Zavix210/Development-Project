using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class AudioController : SimulationComponentBase
{
    private Pool<ManagedSource> sourcePool;
    private ManagedSource sourcePrefab;

    public AudioController(SimulationController controller) : base(controller)
    {
        sourcePrefab = Resources.Load<ManagedSource>("AudioSourcePrefab");

        sourcePool = new Pool<ManagedSource>(CreateNewAudioSource);
    }

    public override bool IsMessageRouteValid(int route)
    {
        return true;
    }

    public override void OnInitialize()
    {

    }

    public override void OnReceivedMessage(Message message)
    {

    }

    public ManagedSource PlayClip(Vector3 position, AudioClip clip, bool looped)
    {
        // Create the source and start it playing
        ManagedSource source = sourcePool.Get();
        source.Play(position, clip, looped);

        return source;
    }

    private ManagedSource CreateNewAudioSource()
    {
        ManagedSource instance = GameObject.Instantiate<ManagedSource>(sourcePrefab);

        // Self-register for events
        instance.clipFinishedHandler += OnClipFinished;

        return instance;
    }

    private void OnClipFinished(object sender, AudioEventArgs e)
    {
        // Store the source in the pool
        ManagedSource source = (ManagedSource)sender;
        sourcePool.Put(source);
    }
}
