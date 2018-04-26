using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;

public class UIMenuButton : MonoBehaviour
{
    [SerializeField]
    private Selectable selectable;
    [SerializeField]
    private AdjustableText adjustableText;
    [SerializeField]
    private float timeUtilPressed = 1.0f;
    private float holdTime = 0.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (selectable != null)
        {
            if (selectable.Selected)
            {
                adjustableText.Highlight(true);

                holdTime += Time.deltaTime;
                if(holdTime >= timeUtilPressed)
                {
                    Pressed();
                }
            }
            else
            {
                adjustableText.Highlight(false);
                holdTime = 0.0f;
            }
        }
	}

    private void Pressed()
    {
        holdTime = 0.0f;

        // Fetch the controller
        Simulation sim = Simulation.Instance;
        SimulationController controller = sim.Controller;

        // Propagate the message
        Message message = new Message((int)MessageDestination.MENU_START_PRESSED, "", this);
        controller.PropagateMessage(message);
    }

    public void SetText(string text)
    {
        adjustableText.SetText(text);
    }
}