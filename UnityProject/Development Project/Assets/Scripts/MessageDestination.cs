using UnityEngine;
using System.Collections;

public enum MessageDestination
{
    DECISION_CHANGE = 37, // Change of decision
    SIMULATION_ROUTE = 1, // Core simulation messages, loading, starting, ending, etc.
}