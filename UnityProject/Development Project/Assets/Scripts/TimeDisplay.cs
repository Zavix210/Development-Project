using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMesh displayText;

    TimeController timeController;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Ensure the time controller is initialized
		if(timeController != null)
        {
            displayText.text = timeController.GetFormattedDisplayTime();
        }
	}

    private void DisplayTime(float time)
    {
        string formattedTime = string.Format("{0:00}", time);
        displayText.text = formattedTime;
    }

    public void Initialize(TimeController timeController)
    {
        this.timeController = timeController;
    }
}
