using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;

public enum MenuState
{
    HIDDEN,
    SHOWING
}

public class MenuController : SimulationComponentBase
{
    private UIMenuButton menuButton;
    private MenuState menuState;
    public MenuState MenuState { get { return menuState; } }

    public MenuController(SimulationController controller) : base(controller)
    {
        menuState = MenuState.HIDDEN;
        menuButton = GameObject.Find("MENU_START_BUTTON").GetComponent<UIMenuButton>();
        menuButton.SetText("Start Simulation");
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
        switch (message.Route)
        {
            case (int)MessageDestination.SIMULATION_END: // The simulation has ended
            case (int)MessageDestination.SIMULATION_LOADED: // The simulation has loaded
                {
                    menuState = MenuState.SHOWING;
                    ShowMenu();
                }
                break;
            case (int)MessageDestination.MENU_START_PRESSED: // The menu start has been pressed
                {

                    menuState = MenuState.HIDDEN;
                    HideMenu();
                }
                break;
        }
    }

    private void ShowMenu()
    {
        menuButton.gameObject.SetActive(true);
    }

    private void HideMenu()
    {
        menuButton.gameObject.SetActive(false);
    }
}
