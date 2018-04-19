using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimulationSystem
{
    public class Timeline
    {
        [SerializeField]
        private float currentTime;
        [SerializeField]
        private float duration;
        [SerializeField]
        private bool playing;
        [SerializeField]
        private bool paused;

        private List<ITimelineAction> actions;
        private List<ITimelineListener> listeners;

        public bool IsPlaying { get { return playing; } }

        public Timeline()
        {
            actions = new List<ITimelineAction>();
            listeners = new List<ITimelineListener>();
        }

        public List<ITimelineAction> GetRawActions()
        {
            return actions;
        }

        /// <summary>
        /// Add an action to the timeline.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool AddAction(ITimelineAction action)
        {
            int id = action.GetActionID();

            foreach (ITimelineAction a in actions)
            {
                if (a.GetActionID() == id)
                {
                    return false;
                }
            }

            actions.Add(action);
            return true;
        }

        /// <summary>
        /// Remove an action from the timeline.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool RemoveAction(ITimelineAction action)
        {
            return actions.Remove(action);
        }

        /// <summary>
        /// Remove all actions from the timeline.
        /// </summary>
        public void ClearActions()
        {
            actions.Clear();
        }

        /// <summary>
        /// Add a listener for timeline events.
        /// </summary>
        /// <param name="listener"></param>
        /// <returns></returns>
        public bool AddListener(ITimelineListener listener)
        {
            if(!listeners.Contains(listener))
            {
                listeners.Add(listener);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Remove a listener for timeline events.
        /// </summary>
        /// <param name="listener"></param>
        /// <returns></returns>
        public bool RemoveListener(ITimelineListener listener)
        {
            return listeners.Remove(listener);
        }

        /// <summary>
        /// Start the timeline playing with the specified total duration.
        /// </summary>
        /// <param name="duration"></param>
        public void StartTimeline(float duration)
        {
            //if (!playing)
            //{
                this.duration = duration;

                currentTime = 0.0f;
                playing = true;
                paused = false;

            foreach (ITimelineListener listener in listeners)
            {
                listener.OnStart(this);
            }
            //}
        }

        /// <summary>
        /// Stop the timeline playing.
        /// </summary>
        public void StopTimeline()
        {
            if (playing)
            {
                ResetTimeline();

                foreach (ITimelineListener listener in listeners)
                {
                    listener.OnStop(this);
                }
            }
        }

        /// <summary>
        /// Set the paused state of the timeline.
        /// </summary>
        /// <param name="state"></param>
        public void SetPauseState(bool state)
        {
            if (paused != state)
            {
                paused = state;

                if (state) // Paused
                {
                    foreach (ITimelineListener listener in listeners)
                    {
                        listener.OnPause(this);
                    }
                }
                else // Resumed
                {
                    foreach (ITimelineListener listener in listeners)
                    {
                        listener.OnResume(this);
                    }
                }
            }
        }

        /// <summary>
        /// Reset the timeline.
        /// </summary>
        public void ResetTimeline()
        {
            currentTime = 0.0f;
            playing = false;
            paused = false;
            actions.Clear();
        }

        /// <summary>
        /// Query the timeline and provide it a timestep to increment by. Any actions which
        /// have a time between the current and new time will be performed.
        /// </summary>
        /// <param name="timeStep"></param>
        public void QueryTimeline(float timeStep)
        {
            // Ensure the simulation is playing but not paused
            if (playing && !paused)
            {
                float start = currentTime;
                float end = start + timeStep;

                currentTime = end;

                // Find actions to execute
                for (int i = 0; i < actions.Count; i++)
                {
                    ITimelineAction action = actions[i];

                    // Does the action fall within the executed period of time?
                    float time = action.GetTimeOfAction();
                    if (time >= start && time <= end)
                    {
                        action.Execute();
                    }
                }

                // Check for finish
                CheckFinishState();
            }
        }

        /// <summary>
        /// Check if the timeline has finished.
        /// </summary>
        private void CheckFinishState()
        {
            // Check the time to see if its over the duration
            if (currentTime >= duration)
            {
                //currentTime = duration;
                StopTimeline();
            }
        }
    }

    public interface ITimelineListener
    {
        void OnStart(Timeline timeline);
        void OnStop(Timeline timeline);
        void OnPause(Timeline timeline);
        void OnResume(Timeline timeline);
    }
}