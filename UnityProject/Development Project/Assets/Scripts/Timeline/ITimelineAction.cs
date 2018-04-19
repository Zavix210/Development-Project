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
        /// Get the time which the action should be played.
        /// </summary>
        /// <returns></returns>
        float GetTimeOfAction();
        /// <summary>
        /// Get the unique ID value of the action.
        /// </summary>
        /// <returns></returns>
        int GetActionID();
    }

    public abstract class TimelineActionBase : ITimelineAction
    {
        private static int nextID = 0;

        private float timeOfAction;
        private int actionID;

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

        public void Execute()
        {
            ExecuteAction();
        }

        public abstract void ExecuteAction();
    }
}