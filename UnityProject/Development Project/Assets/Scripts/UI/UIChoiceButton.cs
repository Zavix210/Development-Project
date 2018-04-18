using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimulationSystem;

public class UIChoiceButton : MonoBehaviour
{
    [SerializeField]
    private float timeUtilPressed = 1.0f;

    [SerializeField]
    private Selectable selectable = null;

    [SerializeField]
    private AdjustableText adjustableText = null;

    private float holdTime = 0.0f;
    private int selectionChoice;

    /// <summary>
    /// Set the selection choice value which represents the decision value for the decision making system.
    /// </summary>
    /// <param name="selectionChoice"></param>
    public void SetSelectionChoice(int selectionChoice)
    {
        this.selectionChoice = selectionChoice;
    }

    /// <summary>
    /// Set the time it takes for a button to become held.
    /// </summary>
    /// <param name="hold"></param>
    public void SetButtonHold(float hold)
    {
        timeUtilPressed = hold;
    }

    /// <summary>
    /// Set the button text, apply the text to the 3D mesh and recalculate the collision box around the text.
    /// </summary>
    /// <param name="text"></param>
    public void SetButtonText(string text)
    {
        adjustableText.SetText(text);
    }

    private void Awake()
    {
        SetButtonText("");
    }

    // Update is called once per frame
    private void Update ()
    {
        // Is the selectable currently selected?
        if (selectable.Selected)
        {
            holdTime += Time.deltaTime;
            CheckHoldTime();

            adjustableText.Highlight(true);
        }
        else // Not pressed, reset the hold time
        {
            holdTime = 0.0f;

            adjustableText.Highlight(false);
        }
	}

    /// <summary>
    /// Perform a check to see if the button has been held down for long enough, if so, a message is passed
    /// to the simulation to choose the next decision.
    /// </summary>
    private void CheckHoldTime()
    {
        // The button is now pressed
        if(holdTime >= timeUtilPressed)
        {
            Debug.Log("CHOOSING " + selectionChoice);
            // Create the message to be passed
            Message message = new Message((int)MessageDestination.DECISION_UI_CHOICE, "", selectionChoice);

            // Propagate the message over the simulation controller
            SimulationController simController = Simulation.Instance.Controller;
            simController.PropagateMessage(message);

            // Reset the hold time to prevent constant pressing
            holdTime = 0.0f;
        }
    }
}
