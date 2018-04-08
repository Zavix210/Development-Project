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
    public UIController(SimulationController controller) : base(controller)
    {

    }

    public override bool IsMessageRouteValid(int route)
    {
        return true;
    }

    public override void OnInitialize()
    {

    }

    public override void OnReceivedMessage(Message message)
    {

    }

    public void PopulateButtons()
    {

    }
}
