using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;


public class VideoPlayerLoader : MonoBehaviour {

    VideoPlayer _videoPlayer;

    //This is just for testing, change to your directory file... in the final project this information will come from the JSON API.
    private string _videoFilePath = @"C:\GameProjects\DevProjectTest\Assets\stationary1.mp4";
    private int _videoWidth = 3840;
    private int _videoHeight = 1920;

    void Awake()
    {

    }

    void Start () {
        PlayVideo(_videoFilePath, _videoWidth, _videoHeight);
	}
	
    void PlayVideo(string url,int width,int height)
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        if(!_videoPlayer)
            _videoPlayer = gameObject.AddComponent<VideoPlayer>();
        
        //creating render to texture, the dimensions need to be the dimesions of the video file and it should come from the JSON API.
        RenderTexture renderTexture = new RenderTexture(width, height, 0);
        renderTexture.name = "360videoRenderToTexture";

        Shader shader = Shader.Find("Skybox/PanoramicBeta");
        Material material = new Material(shader);
        material.SetTexture("_Tex", renderTexture);

        //it is possible to change the video properties below to add effects like tint and change lighting
        //material.SetColor("_Tint", new Color(1, 1, 1));
        //material.SetFloat("_Exposure", 0.0f);

        //rendering the video on the skybox
        RenderSettings.skybox = material;

        _videoPlayer.url = url;
        _videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        _videoPlayer.targetTexture = renderTexture;
        _videoPlayer.Play();
    }

	void Update () {
		
	}
}
