using UnityEngine;
using System.Collections;

/// <summary>
/// A utility enum which centralises the message routes for specific application messages allowing for
/// a more maintainable messaging system. The messaging system functions independently of this enum.
/// </summary>
public enum MessageDestination
{
    SIMULATION_LOADING = 1, // The simulation is loading
    SIMULATION_START = 2, // The simulation has started
    SIMULATION_END = 3, // The simulation has ended
    DECISION_CHANGE = 37, // A decision has been made (some change has occurred)
    DECISION_UI_CHOICE = 38, // The UI was pressed for a decision button
}