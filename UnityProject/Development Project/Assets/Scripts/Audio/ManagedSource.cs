using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ManagedAudioSourceState
{
    Idle,
    Playing
}

public enum AudioState
{
    Finished,
    Started
}

public class AudioEventArgs : EventArgs
{
    private AudioState audioState;
    public AudioState AudioState { get { return audioState; } }

    public AudioEventArgs(AudioState audioState)
    {
        this.audioState = audioState;
    }
}

public class ManagedSource : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    private ManagedAudioSourceState state;

    public EventHandler<AudioEventArgs> clipFinishedHandler;
    public EventHandler<AudioEventArgs> clipStartedHandler;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Currently playing?
        if (state == ManagedAudioSourceState.Playing)
        {
            // Has the source stopped? (Finished?)
            if (!source.isPlaying)
            {
                Stop();
            }
        }
	}

    public void Stop()
    {
        state = ManagedAudioSourceState.Idle;

        source.Stop();
        OnStopped();
    }

    public void Play(Vector3 position, AudioClip clip, bool looped)
    {
        state = ManagedAudioSourceState.Playing;

        source.transform.position = position;
        source.clip = clip;
        source.loop = looped;

        source.Play();

        OnPlayed();
    }

    private void OnPlayed()
    {
        if(clipStartedHandler != null)
        {
            clipStartedHandler.Invoke(this, new AudioEventArgs(AudioState.Started));
        }
    }

    private void OnStopped()
    {
        if (clipFinishedHandler != null)
        {
            clipFinishedHandler.Invoke(this, new AudioEventArgs(AudioState.Finished));
        }
    }
}
