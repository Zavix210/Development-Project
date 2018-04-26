using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader
{

    public static bool LoadAudioClipBlocking(string filePath, out AudioClip clip)
    {
        clip = null;
        WWW www = new WWW(filePath);
        if (!string.IsNullOrEmpty(www.error)) // Was there no error?
        {
            // Wait for the file to finish loading...
            while(true)
            {
                if(www.isDone)
                {
                    break;
                }

                // Was there an error?
                string err = www.error;
                if(string.IsNullOrEmpty(err))
                {
                    return false;
                }
            }

            // File has finished
            clip = www.GetAudioClip();
            return true;
        }
        else // Error occurred during loading
        {
            Debug.LogError("Failed to load Audio clip: " + www.error);
            return false;
        }   
    }
}
