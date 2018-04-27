using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class AudioController : SimulationComponentBase
{
    private Store<ManagedSource> activeSources;
    private Pool<ManagedSource> sourcePool;
    private ManagedSource sourcePrefab;

    public AudioController(SimulationController controller) : base(controller)
    {
        sourcePrefab = Resources.Load<ManagedSource>("AudioSourcePrefab");
        activeSources = new Store<ManagedSource>();
        sourcePool = new Pool<ManagedSource>(CreateNewAudioSource, OnSourceStored, OnSourceReleased);
    }

    private void OnSourceReleased(ManagedSource obj)
    {
        activeSources.Add(obj);
    }

    private void OnSourceStored(ManagedSource obj)
    {
        activeSources.Remove(obj);
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
        if (message.Route == (int)MessageDestination.SCENE_CHANGE)
        {
            if (message.Identifier == "VALID")
            {
                SimulationScene scene = (SimulationScene)message.Data;

                string volumeStr; //filePath
                int volume = 0;
                if (scene.GetAttribute("GENERAL_SETTINGS_ALARM_VOLUME", out volumeStr))
                {
                    volume = Int32.Parse(volumeStr);
                    if (volume <= 0)
                        return;
                }
                string filePath;
                if (scene.GetAttribute("GENERAL_SETTINGS_ALARM_FILE", out filePath))
                {
                    // Add a prefix to fix bad file names
                    string url = Application.dataPath + @"/JsonScene/" + filePath;
                    string filePrefix = @"file://";
                    url = filePrefix + url;

                    Debug.Log(url);
                    AudioClip clip;
                    if (AudioLoader.LoadAudioClipBlocking(url, out clip))
                    {
                        PlayClip(Vector3.zero, clip, (float) volume/100, true);
                    }
                    else
                    {
                        Debug.LogError("FAILED TO PLAY CLIP FROM URL " + url);
                    }

                }

            }
        }
    }

    public ManagedSource PlayClip(Vector3 position, AudioClip clip,float volume, bool looped)
    {     
        // Create the source and start it playing
        ManagedSource source = sourcePool.Get();
        source.Play(position, clip,volume, looped);
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

    /// <summary>
    /// 
    /// </summary>
    public void StopAllSources()
    {
        List<ManagedSource> sources = activeSources.GetRawList();
        int len = sources.Count - 1;

        // Iterate and stop all sources
        for(int i = len; i >= 0; i--)
        {
            ManagedSource source = sources[i];
            source.Stop();
        }
    }
}
