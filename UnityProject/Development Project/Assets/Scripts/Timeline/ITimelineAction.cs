using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimulationSystem
{
    public interface ITimelineAction
    {
        /// <summary>
        /// Execute the timeline action.
        /// </summary>
        void Execute();
        /// <summary>
        /// Reset the state of the timeline action.
        /// </summary>
        void Reset();
        /// <summary>
        /// Get the time which the action should be played.
        /// </summary>
        /// <returns></returns>
        float GetTimeOfAction();
        /// <summary>
        /// Get the unique ID value of the action.
        /// </summary>
        /// <returns></returns>
        int GetActionID();
        /// <summary>
        /// Check if the action has been played yet.
        /// </summary>
        /// <returns></returns>
        bool HasPlayed();
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