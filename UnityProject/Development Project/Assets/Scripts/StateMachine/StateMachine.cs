using System.Collections.Generic;

/// <summary>
/// A state listener will receive messages for state changes
/// </summary>
/// <typeparam name="StateType"></typeparam>
public interface IStateListener<StateType>
{
    void StateChanged(StateType oldState, StateType newState);
}

/// <summary>
/// A generic class which handles state storage and transitions and also messages any listeners
/// when a state changes.
/// </summary>
/// <typeparam name="StateType"></typeparam>
public class StateMachine<StateType>
{
    private StateType state;
    private List<IStateListener<StateType>> listeners;

    public StateType CurrentState { get { return state; } }

    public StateMachine(StateType defaultState)
    {
        state = defaultState;
        listeners = new List<IStateListener<StateType>>();
    }

    /// <summary>
    /// Set the state of the machine.
    /// </summary>
    /// <param name="newState"></param>
    public void SetState(StateType newState)
    {
        NotifyStateChange(state, newState);
        state = newState;
    }

    /// <summary>
    /// Add a listener to the state machine messaging.
    /// </summary>
    /// <param name="listener"></param>
    public void AddListener(IStateListener<StateType> listener)
    {
        if(!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    /// <summary>
    /// Remove a listener from the state machine messaging.
    /// </summary>
    /// <param name="listener"></param>
    public void RemoveListener(IStateListener<StateType> listener)
    {
        listeners.Remove(listener);
    }

    /// <summary>
    /// Notify all listeners of the new and old states from a recent change.
    /// </summary>
    /// <param name="oldState"></param>
    /// <param name="newState"></param>
    private void NotifyStateChange(StateType oldState, StateType newState)
    {
        foreach(IStateListener<StateType> listener in listeners)
        {
            listener.StateChanged(oldState, newState);
        }
    }
}
