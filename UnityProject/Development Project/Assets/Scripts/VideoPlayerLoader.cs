using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

public class VideoPlayerLoader : MonoBehaviour {

    VideoPlayer _videoPlayer;
    Material _videoMaterial;
    public delegate void FinishedVideoHandler();
    public event FinishedVideoHandler finishedPlayingCurrentVideo;

    public void PlayVideo(string url,int width,int height)
    {
        if(!_videoPlayer)
        {
            _videoPlayer = gameObject.AddComponent<VideoPlayer>();
            _videoPlayer.prepareCompleted += prepareCompleted;
            _videoPlayer.errorReceived += videoPlayerError;
            _videoPlayer.playOnAwake = false;
            //creating render to texture, the dimensions need to be the dimesions of the video file and it should come from the JSON API.
            RenderTexture renderTexture = new RenderTexture(width, height, 0);
            renderTexture.name = "360videoRenderToTexture";

            Shader shader = Shader.Find("Skybox/PanoramicBeta");
            _videoMaterial = new Material(shader);
            _videoMaterial.SetTexture("_Tex", renderTexture);

            //rendering the video on the skybox
            RenderSettings.skybox = _videoMaterial;
            _videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            _videoPlayer.targetTexture = renderTexture;
        }
        VideoPlayer videoPlayer = _videoPlayer;
        videoPlayer.url = url;
        videoPlayer.Prepare();
    }

    public void SetColor(Color color)
    {
        _videoMaterial.SetColor("_Tint", color);
    }

    public void SetExposure(float exposure)
    {
        _videoMaterial.SetFloat("_Exposure", exposure);
    }

    public void PauseVideo()
    {
        _videoPlayer.Pause();
    }


    private void videoPlayerError(VideoPlayer source, string message)
    {
        Debug.LogError("Error playing video: " + message);
    }

    private void prepareCompleted(VideoPlayer source)
    {
        source.Play();
        source.loopPointReached += finishedPlaying;
    }

    void finishedPlaying(VideoPlayer source)
    {
        _videoPlayer.Pause();
        finishedPlayingCurrentVideo.Invoke();
    }

    //TEST---------------------
    //private string _videoFilePath = @"C:\GameProjects\DevProjectTest\Assets\stationary1.mp4";
    //private string _videoFilePath2 = @"C:\GameProjects\DevProjectTest\Assets\stationary2.mp4";
    //private int _videoWidth = 3840;
    //private int _videoHeight = 1920;

    //void Awake()
    //{
    //    finishedPlayingCurrentVideo += playNextVideoTest;
    //}

    //void Start()
    //{
    //    PlayVideo(_videoFilePath, _videoWidth, _videoHeight);
    //}

    //private void playNextVideoTest()
    //{
    //    PlayVideo(_videoFilePath2, _videoWidth, _videoHeight);
    //}



}
