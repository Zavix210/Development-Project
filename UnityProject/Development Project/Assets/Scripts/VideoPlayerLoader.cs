﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;


public class VideoPlayerLoader : MonoBehaviour {

    VideoPlayer _videoPlayer;
    Material _videoMaterial;
    public delegate void FinishedVideoHandler();
    public delegate void FadeToBlackFinishedHandler();
    public delegate void FadeToClearFinishedHandler();
    public Image FadeImage;
    public float FadeDuration = 1.5f;
    /// <summary>
    /// Invoked when the video finishes executing. It will remain paused until another video is asked to be played.
    /// </summary>
    public event FinishedVideoHandler finishedPlayingCurrentVideo;

    public event FadeToBlackFinishedHandler FadeToBlackFinished;

    public event FadeToClearFinishedHandler FadeToClearFinished;

    /// <summary>
    /// Use this method to play the videos that will be rendered on the skybox.
    /// </summary>
    /// <param name="url">File path of the video, be careful with permissions on the folders.</param>
    /// <param name="width">Video Width</param>
    /// <param name="height">Video Height</param>
    public void PlayVideo(string url,int width,int height)
    {
        if(!_videoPlayer)
        {
            _videoPlayer = gameObject.AddComponent<VideoPlayer>();
            _videoPlayer.prepareCompleted += PrepareCompleted;
            _videoPlayer.errorReceived += VideoPlayerError;
            _videoPlayer.loopPointReached += FinishedPlaying;
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

    /// <summary>
    /// Tints the video with a specific color.
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
    {
        _videoMaterial.SetColor("_Tint", color);
    }

    /// <summary>
    /// Changes the exposure of the video making it bighter/darker
    /// </summary>
    /// <param name="exposure"></param>
    public void SetExposure(float exposure)
    {
        _videoMaterial.SetFloat("_Exposure", exposure);
    }

    /// <summary>
    /// Pauses the video
    /// </summary>
    public void PauseVideo()
    {
        _videoPlayer.Pause();
    }


    private void VideoPlayerError(VideoPlayer source, string message)
    {
        Debug.LogError("Error playing video: " + message);
    }

    private void PrepareCompleted(VideoPlayer source)
    {
        source.Play();
    }

    private void FinishedPlaying(VideoPlayer source)
    {
        _videoPlayer.Pause();
        finishedPlayingCurrentVideo.Invoke();
    }

    public void FadeToBlack()
    {
        IEnumerator coroutine = InnerFadeToBlack();
        StartCoroutine(coroutine);

    }

    private IEnumerator InnerFadeToBlack()
    {
        Color startColor = FadeImage.color;
        Color endColor = Color.black;
        float t = 0.0f;

        while (t < 1.0f)
        {
            FadeImage.color = Color.Lerp(startColor, endColor, t);
            t += Time.deltaTime / FadeDuration;
            yield return new WaitForEndOfFrame();
        }
        if(FadeToBlackFinished != null)
            FadeToBlackFinished.Invoke();
    }

    public void FadeToClear()
    {
        IEnumerator coroutine = InnerFadeToClear();
        StartCoroutine(coroutine);

    }

    private IEnumerator InnerFadeToClear()
    {
        Color startColor = FadeImage.color;
        Color endColor = Color.clear;
        float t = 0.0f;

        while (t < 1.0f)
        {
            FadeImage.color = Color.Lerp(startColor, endColor, t);
            t += Time.deltaTime / FadeDuration;
            yield return new WaitForEndOfFrame();
        }

        if(FadeToClearFinished != null)
            FadeToClearFinished.Invoke();
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
