using UnityEngine;
using System.Collections;

/// <summary>
/// A utility enum which centralises the message routes for specific application messages allowing for
/// a more maintainable messaging system. The messaging system functions independently of this enum.
/// </summary>
public enum MessageDestination
{
    SIMULATION_LOADED, // The simulation is loading
    SIMULATION_START, // The simulation has started
    SIMULATION_PAUSE, // The simulation has been paused
    SIMULATION_RESUME, // The simulation has been resumed
    SIMULATION_END, // The simulation has ended

    DECISION_CHANGE, // A decision has been made (some change has occurred)
    DECISION_UI_CHOICE, // The UI was pressed for a decision button

    TIMELINE_FINISH, // The timeline has reached the end

    TIMER_EXPIRED, // The timer has finished

    SCENE_CHANGE, // A scene has changed

    DECISION_START, // Decision selection has started
    DECISION_CHOICE_MADE, // A choice was selected
    DECISION_END, // Decision selection has ended

    MENU_START_PRESSED, // Menu start button has been pressed
}