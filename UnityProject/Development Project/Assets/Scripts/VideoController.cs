using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class VideoController : SimulationComponentBase
{
    VideoPlayerLoader _videoPlayer;
    public VideoController(SimulationController controller) : base(controller)
    {
        _videoPlayer = GameObject.FindObjectOfType<VideoPlayerLoader>();
        if (!_videoPlayer)
            Debug.LogError("VideoPlayer not found on scene!");

        _videoPlayer.finishedPlayingCurrentVideo += onVideoFinishedPlaying;
    }

    private void onVideoFinishedPlaying()
    {
        Debug.Log("Video Finished Playing.");
        //Video finished playing, send message to show UI decisions?
    }

    public override bool IsMessageRouteValid(int route)
    {
        return route == (int)MessageDestination.DECISION_CHANGE;
    }

    public override void OnInitialize()
    {
        // Basically the Awake method.
    }

    public override void OnReceivedMessage(Message message)
    {
        // A decision was made (next video?)
        if(message.Route == (int)MessageDestination.DECISION_CHANGE)
        {
            
            // Right now the video is being loaded dynamically. There is only one video instance in the memory.(The video that is currently playing)
            string url = ""; //filePath
            int width = 3840;
            int height = 1920;
            _videoPlayer.PlayVideo(url, width, height);
            
        }
    }
}
