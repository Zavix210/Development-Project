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
    public delegate void FadeToBlackFinishedHandler();
    public delegate void FadeToClearFinishedHandler();
    public Image FadeImage;
    public float FadeDuration = 1.5f;
    public AudioSource audioSource;
    public bool _firstTime = true;
    public bool _emergencyLights = false;
    public Material VideoMaterial;
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
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        _videoPlayer = gameObject.GetComponent<VideoPlayer>();
        if(_videoPlayer == null)
        {
            Debug.LogError("Video Player Not Found on Component");
        }
        if (_videoPlayer && _firstTime)
        {
            _videoPlayer.prepareCompleted += PrepareCompleted;
            _videoPlayer.errorReceived += VideoPlayerError;
            _videoPlayer.loopPointReached += FinishedPlaying;
            _videoPlayer.playOnAwake = false;
            //creating render to texture, the dimensions need to be the dimesions of the video file and it should come from the JSON API.
            RenderTexture renderTexture = new RenderTexture(width, height, 0);
            renderTexture.name = "360videoRenderToTexture";

            //Shader shader = Shader.Find("Skybox/PanoramicBeta");
            _videoMaterial = VideoMaterial;
            _videoMaterial.SetTexture("_Tex", renderTexture);

            //rendering the video on the skybox
            RenderSettings.skybox = _videoMaterial;
            _videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            _videoPlayer.targetTexture = renderTexture;
            _videoPlayer.url = url;
            _firstTime = false;
        }
        
        VideoPlayer videoPlayer = _videoPlayer;
        
        videoPlayer.url = url;
        
        videoPlayer.Prepare();
    }

    public void SetVolume(float volume)
    {
        if(volume <= 0.0f)
        {
            audioSource.volume = 0.0f;
            return;
        }
        audioSource.volume = (float) volume / 100;
    }

    public void ResumeVideo()
    {
        if (_videoPlayer == null)
            return;
        _videoPlayer.Play();
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
        if (exposure <= 0.0f)
        {
            _videoMaterial.SetFloat("_Exposure", 0.0f);
            return;
        }
        _videoMaterial.SetFloat("_Exposure", exposure/100);
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
        _videoPlayer.SetTargetAudioSource(0, audioSource);
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        _videoPlayer.EnableAudioTrack(0, true);
        _videoPlayer.controlledAudioTrackCount = 1;

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

    public void StartEmergencyLights()
    {
        _emergencyLights = true;
        IEnumerator coroutine = InnerEmergencyLights();
        StartCoroutine(coroutine);
    }

    public void StopEmergencyLights()
    {
        _emergencyLights = false;
    }

    private IEnumerator InnerEmergencyLights()
    {

        while (_emergencyLights)
        {
            Color startColor = _videoMaterial.GetColor("_Tint");
            Color endColor = new Color(0.3f,0.0f,0.0f,0.5f);
            float t = 0.0f;

            while (t < 1.0f)
            {
                SetColor(Color.Lerp(startColor, endColor, t));
                t += Time.deltaTime / 1.0f;
                yield return new WaitForEndOfFrame();
            }

            startColor = _videoMaterial.GetColor("_Tint");
            endColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            t = 0.0f;

            while (t < 1.0f)
            {
                SetColor(Color.Lerp(startColor, endColor, t));
                t += Time.deltaTime / 1.0f;
                yield return new WaitForEndOfFrame();
            }

        }
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
    //    _videoPlayer.EnableAudioTrack(0, true);
    //}

    //private void playNextVideoTest()
    //{
    //    PlayVideo(_videoFilePath2, _videoWidth, _videoHeight);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        StopEmergencyLights();
    //    }
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        StartEmergencyLights();
    //    }
    //}



}
