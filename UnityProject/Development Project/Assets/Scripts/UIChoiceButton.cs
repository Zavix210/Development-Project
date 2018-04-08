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
    [SerializeField]
    private string buttonText = "DEFAULT_TEXT";
    [SerializeField]
    private TextMesh textMesh = null;
    [SerializeField]
    private BoxCollider collisionBox = null;
    [SerializeField]
    private Selectable selectable = null;
    [SerializeField]
    private float textThickness = 0.2f;

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
        buttonText = text;
        textMesh.text = text;

        // Recalculate the collision box
        CalculateBoxForText();
    }

    /// <summary>
    /// Calculate the size of a collision box to fit the text contents of the text mesh.
    /// </summary>
    private void CalculateBoxForText()
    {
        Renderer renderer = textMesh.GetComponent<Renderer>();
        Bounds rBounds = renderer.bounds;
        Vector3 rSize = rBounds.size;

        // Calculate the required size to fit the collision box around the text contents
        Vector3 fScale = textMesh.transform.localScale;
        Vector3 fSize = new Vector3(rSize.x * (1.0f / fScale.x), rSize.y * (1.0f / fScale.y), textThickness);

        // Apply the box scale values
        collisionBox.size = fSize;
        collisionBox.center = Vector3.zero;
    }

    private void Awake()
    {

    }

    // Use this for initialization
    private void Start ()
    {
		
	}

    // Update is called once per frame
    private void Update ()
    {
        // TEMP
        SetButtonText(buttonText);

        // Is the selectable currently selected?
        if (selectable.Selected)
        {
            holdTime += Time.deltaTime;
            CheckHoldTime();
        }
        else // Not pressed, reset the hold time
        {
            holdTime = 0.0f;
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
