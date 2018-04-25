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
        return route == (int)MessageDestination.SCENE_CHANGE || route == (int)MessageDestination.SIMULATION_PAUSE || route == (int)MessageDestination.SIMULATION_RESUME;
    }

    public override void OnInitialize()
    {
        // Basically the Awake method.
    }

    public override void OnReceivedMessage(Message message)
    {
        // A scene was changed (next video?)
        if (message.Route == (int)MessageDestination.SCENE_CHANGE)
        {
            // Is the scene valid?
            if (message.Identifier == "VALID")
            {
                // Get the scene node from the message
                SimulationScene scene = (SimulationScene)message.Data;

                // Try to get the scene attribute containing the video URL
                string url; //filePath
                if (scene.GetAttribute("VIDEO_URL", out url))
                {
                    // Right now the video is being loaded dynamically. There is only one video instance in the memory.(The video that is currently playing)
                    int width = 3840;
                    int height = 1920;
                    _videoPlayer.PlayVideo(url, width, height);
                }
                else // Failure, log a message
                {
                    Debug.LogWarning("Failed to get 'VIDEO_URL' attribute from scene node");
                }
            }
        }
        else if (message.Route == (int)MessageDestination.SIMULATION_PAUSE) // Pause Video
        {
            _videoPlayer.PauseVideo();
        }
        else if (message.Route == (int)MessageDestination.SIMULATION_RESUME) // Resume Video
        {
            _videoPlayer.ResumeVideo();
        }
    }
}
