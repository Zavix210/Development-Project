using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimulationSystem
{
    public interface ITimelineAction
    {
        void Execute();
        float GetTimeOfAction();
        int GetActionID();
        bool HasPlayed();
        void Reset();
    }

    public abstract class TimelineActionBase : ITimelineAction
    {
        private static int nextID = 0;

        private float timeOfAction;
        private int actionID;
        private bool hasPlayed;

        public TimelineActionBase()
        {
            actionID = nextID++;
        }

        public int GetActionID()
        {
            return actionID;
        }

        public void SetTimeOfAction(float time)
        {
            timeOfAction = time;
        }

        public float GetTimeOfAction()
        {
            return timeOfAction;
        }

        public bool HasPlayed()
        {
            return hasPlayed;
        }

        public void Reset()
        {
            hasPlayed = false;
        }

        public void Execute()
        {
            if (!hasPlayed)
            {
                hasPlayed = true;
                ExecuteAction();
            }
            else
            {
                Debug.LogWarning("Tried to play an action multiple times");
            }
        }

        public abstract void ExecuteAction();
    }
}