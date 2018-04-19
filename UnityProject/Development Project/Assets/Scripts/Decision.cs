using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionSet
{
    private float time;
    private Store<Decision> decisions;

    public float Time { get { return time; } set { time = value; } }

    public DecisionSet()
    {
        decisions = new Store<Decision>();
    }

    public List<Decision> GetDecisions()
    {
        return decisions.GetRawList();
    }

    public bool AddDecision(Decision decision)
    {
        return decisions.Add(decision);
    }

    public bool RemoveDecision(Decision decision)
    {
        return decisions.Add(decision);
    }
}

public enum DecisionResult
{
    SUCCESS,
    FAIL
}

public class Decision
{
    private static int NextID = 0;

    private DecisionResult result;
    private string displayText;
    private string feedback;
    private int identifier;

    public DecisionResult Result { get { return result; } }
    public string DisplayText { get { return displayText; } }
    public string Feedback { get { return feedback; } }
    public int Identifier { get { return identifier; } }

    public Decision(DecisionResult result, string displayText, string feedback)
    {
        this.result = result;
        this.displayText = displayText;
        this.feedback = feedback;

        // Set the ID value to the next available ID
        identifier = NextID++;
    }
}
