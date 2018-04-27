using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;

/// <summary>
/// This class is a temporary class for testing some simple features of the simulation
/// </summary>
public class Test : MonoBehaviour {

    [SerializeField]
    private AudioClip clip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        string someTestPath = "C:/";
        AudioTest2(someTestPath);
	}

    private void AudioTest1()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Simulation sim = Simulation.Instance;
            AudioController ac = sim.Controller.GetSimulationComponent<AudioController>();
            ac.PlayClip(Vector3.zero, clip,1, true);
        }
    }

    private void AudioTest2(string testPath)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Load the clip
            AudioClip loadedClip;
            AudioLoader.LoadAudioClipBlocking(testPath, out loadedClip);

            // Push the clip
            Simulation sim = Simulation.Instance;
            AudioController ac = sim.Controller.GetSimulationComponent<AudioController>();
            ac.PlayClip(Vector3.zero, loadedClip,1, true);
        }
    }
}
