using UnityEngine;
using System.Collections;

/// <summary>
/// A utility enum which centralises the message routes for specific application messages allowing for
/// a more maintainable messaging system. The messaging system functions independently of this enum.
/// </summary>
public enum MessageDestination
{
    DECISION_CHANGE = 37, // Change of decision
    SIMULATION_ROUTE = 1, // Core simulation messages, loading, starting, ending, etc.
}