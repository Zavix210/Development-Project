using UnityEngine;
using System.Collections;
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

    public UIController(SimulationController controller) : base(controller)
    {
        prefab = Resources.Load<UIChoiceButton>("UIChoiceButton");
        circularUI = GameObject.FindObjectOfType<CircularUI>();
        uiFitter = GameObject.FindObjectOfType<UIFitter>();

        choiceButtonPool = new Pool<UIChoiceButton>(CreateButtonInstance, ButtonStored, ButtonReleased);
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

        float m1 = 1.0f;
        int num = 4;
        float half = num * m1 * 0.5f;

        for (int i = 0; i < num; i++)
        {
            UIChoiceButton button = choiceButtonPool.Get();
            uiFitter.AddItem(button);

            //button.transform.position = new Vector3(-half + (i + 1) * m1, 0.5f, 4.0f);
            button.SetButtonText("Decision " + (i + 1));
        }

        // ----------------------------------
        // TEMPORARY CODE
        // ----------------------------------
    }

    public override void OnReceivedMessage(Message message)
    {

    }

    public void PlaceButton()
    {

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