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
    private DecisionResult result;
    private string feedback;

    public DecisionResult Result { get { return result; } }
    public string Feedback { get { return feedback; } }

    public Decision(DecisionResult result, string feedback)
    {
        this.result = result;
        this.feedback = feedback;
    }
}
