using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimulationSystem;

public class UIChoiceButton : MonoBehaviour
{
    [SerializeField]
    private float holdTime = 0.0f;
    [SerializeField]
    private float timeUtilPressed = 2.0f;

    private Selectable selectable;
    private int selectionChoice;

    /// <summary>
    /// Set the selection choice value which represents the decision value for the decision making system.
    /// </summary>
    /// <param name="selectionChoice"></param>
    public void SetSelectionChoice(int selectionChoice)
    {
        this.selectionChoice = selectionChoice;
    }

    private void Awake()
    {
        selectable = GetComponent<Selectable>();
    }

    // Use this for initialization
    private void Start ()
    {
		
	}

    // Update is called once per frame
    private void Update ()
    {
        // Is the selectable currently selected?
		if(selectable.Selected)
        {
            holdTime += Time.deltaTime;
            CheckHoldTime();
        }
        else
        {
            holdTime = 0.0f;
        }
	}

    private void CheckHoldTime()
    {
        // The button is now pressed
        if(holdTime >= timeUtilPressed)
        {
            // Create the message to be passed
            Message message = new Message((int)MessageDestination.DECISION_CHANGE, "CHOICE", selectionChoice);

            // Propagate the message over the simulation controller
            SimulationController simController = Simulation.Instance.Controller;
            simController.PropagateMessage(message);

            // Reset the hold time to prevent constant pressing
            holdTime = 0.0f;
        }
    }
}
