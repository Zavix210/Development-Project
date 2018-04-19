using UnityEngine;
using System.Collections.Generic;
using SimulationSystem;
using System;

public class ButtonData
{
    private int choiceIndex;
    private string textContent;
}

public class UIController : SimulationComponentBase
{
    private Pool<UIChoiceButton> choiceButtonPool;
    private UIChoiceButton prefab;
    private CircularUI circularUI;
    private UIFitter uiFitter;

    private List<int> decisionChoices;
    private List<UIChoiceButton> activeButtons;

    public UIController(SimulationController controller) : base(controller)
    {
        prefab = Resources.Load<UIChoiceButton>("UIChoiceButton");
        circularUI = GameObject.FindObjectOfType<CircularUI>();
        uiFitter = GameObject.FindObjectOfType<UIFitter>();

        choiceButtonPool = new Pool<UIChoiceButton>(CreateButtonInstance, ButtonStored, ButtonReleased);

        decisionChoices = new List<int>();
        activeButtons = new List<UIChoiceButton>();
    }

    public override bool IsMessageRouteValid(int route)
    {
        return true;
    }

    public override void OnInitialize()
    {
        // ----------------------------------
        // TEMPORARY CODE
        // ----------------------------------

        //float m1 = 1.0f;
        //int num = 4;
        //float half = num * m1 * 0.5f;

        //for (int i = 0; i < num; i++)
        //{
        //    UIChoiceButton button = choiceButtonPool.Get();
        //    uiFitter.AddItem(button);

        //    //button.transform.position = new Vector3(-half + (i + 1) * m1, 0.5f, 4.0f);
        //    button.SetButtonText("Decision " + (i + 1));
        //}

        // ----------------------------------
        // TEMPORARY CODE
        // ----------------------------------
    }

    public override void OnReceivedMessage(Message message)
    {
        switch (message.Route)
        {
            case (int)MessageDestination.SCENE_CHANGE:
                {
                    // Was the decision a valid decision?
                    if (message.Identifier == "VALID")
                    {
                        SimulationScene scene = (SimulationScene)message.Data;

                        // Create the UI buttons
                        CreateButtonsForDecision(scene);
                    }
                }
                break;
        }
    }

    public void PlaceButton(int decisionChoice, string decisionText)
    {
        UIChoiceButton button = choiceButtonPool.Get();
        activeButtons.Add(button);

        // Populate data entries
        button.SetSelectionChoice(decisionChoice);
        button.SetButtonText(decisionText);

        uiFitter.AddItem(button);
    }

    /// <summary>
    /// Create the buttons which represent the choices of the specified decision
    /// </summary>
    /// <param name="decision"></param>
    private void CreateButtonsForDecision(SimulationScene scene)
    {
        // Get the routes
        scene.GetRoutes(decisionChoices);

        // Store all active buttons
        StoreActiveButtons();

        // Create UI choices
        foreach(int i in decisionChoices)
        {
            // Try to get the decision for the choice index
            SimulationScene iScene;
            if (scene.GetSceneFromRoute(i, out iScene))
            {
                string decisionText = "DEFAULT BUTTON\nTEST";// iScene.GetDisplayTitle();
                PlaceButton(i, decisionText);

                //// Try to get the current override title or if not, the next title.
                //string decisionText;
                //if(decision.GetAttribute("OVERRIDE_TITLE", out decisionText) || iDecision.GetAttribute("TITLE", out decisionText))
                //{
                //    PlaceButton(i, decisionText);
                //}
                //else // Failed to get title
                //{
                //    Debug.LogWarning("Failed to get title from decision with ID: " + i);
                //}
            }
            else // Failed to get decision
            {
                Debug.LogWarning("Failed to get decision from ID: " + i);
            }
        }
    }

    private void StoreActiveButtons()
    {
        foreach(UIChoiceButton button in activeButtons)
        {
            choiceButtonPool.Put(button);
            uiFitter.RemoveItem(button);
        }

        activeButtons.Clear();
    }

    /// <summary>
    /// Called when a new button needs to be created for the pool.
    /// </summary>
    /// <returns></returns>
    private UIChoiceButton CreateButtonInstance()
    {
        UIChoiceButton buttonInstance = GameObject.Instantiate<UIChoiceButton>(prefab);

        // TODO: Init stuff here...

        return buttonInstance;
    }

    /// <summary>
    /// Called whenever a button is released from the pool.
    /// </summary>
    /// <param name="button"></param>
    private void ButtonReleased(UIChoiceButton button)
    {
        // Enable released buttons.
        button.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called whenever a button is stored in the pool.
    /// </summary>
    /// <param name="button"></param>
    private void ButtonStored(UIChoiceButton button)
    {
        // Reset the button and disable it.
        button.SetButtonText("");
        button.gameObject.SetActive(false);
    }
}