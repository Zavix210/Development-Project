using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMesh displayText;
    [SerializeField]
    private Color defaultTimeColour;
    [SerializeField]
    private Color expiredTimeColour;
    [SerializeField]
    private Color warningTimeColour;
    [SerializeField]
    private float warningTimeStart;
    [SerializeField]
    private float warningTimeFlickerRate;
    [SerializeField]
    private float warningTimeFlickerHold;

    TimeController timeController;
    float localTime;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Increment local time
        localTime += Time.deltaTime;

        // Calculate the local time flag to determine flicker rates
        bool fFlag = false;
        if (localTime % warningTimeFlickerRate >= warningTimeFlickerHold)
        {
            fFlag = true;
        }

        // Ensure the time controller is initialized
        if (timeController != null)
        {
            float remainingTime = timeController.GetRemainingTime();
            displayText.text = timeController.GetFormattedDisplayTime();

            if(timeController.HasTimeExpired())        // Expired time
            {
                if (fFlag) // Flicker?
                {
                    displayText.color = expiredTimeColour * 0.7f;
                }
                else
                {
                    displayText.color = expiredTimeColour;
                }
            }
            else if(remainingTime <= warningTimeStart) // Warning time
            {
                if (fFlag) // Flicker?
                {
                    displayText.color = warningTimeColour * 0.7f;
                }
                else
                {
                    displayText.color = warningTimeColour;
                }
            }
            else
            {
                displayText.color = defaultTimeColour; // Default time
            }
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
