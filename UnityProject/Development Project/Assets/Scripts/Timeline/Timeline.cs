using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimulationSystem
{
    public interface ITimelineListener
    {
        void OnStart(Timeline timeline);
        void OnStop(Timeline timeline);
        void OnPause(Timeline timeline);
        void OnResume(Timeline timeline);
    }

    public class Timeline
    {
        private float currentTime;
        private float duration;
        private bool playing;
        private bool paused;

        private List<ITimelineAction> actions;
        private List<ITimelineListener> listeners;

        public bool IsPlaying { get { return playing; } }

        public Timeline()
        {
            actions = new List<ITimelineAction>();
            listeners = new List<ITimelineListener>();
        }

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

        public bool RemoveAction(ITimelineAction action)
        {
            return actions.Remove(action);
        }

        public void ClearActions()
        {
            actions.Clear();
        }

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

        public bool RemoveListener(ITimelineListener listener)
        {
            return listeners.Remove(listener);
        }

        public void Start(float duration)
        {
            if (!playing)
            {
                Reset();
                playing = true;

                foreach (ITimelineListener listener in listeners)
                {
                    listener.OnStart(this);
                }
            }
        }

        public void Stop()
        {
            if (playing)
            {
                Reset();

                foreach (ITimelineListener listener in listeners)
                {
                    listener.OnStop(this);
                }
            }
        }

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

        public void Reset()
        {
            currentTime = 0.0f;
            playing = false;
            paused = false;
        }

        public void QueryTimeline(float timeStep)
        {
            float start = currentTime;
            float end = start + timeStep;

            // Find actions to execute
            foreach (ITimelineAction action in actions)
            {
                // Does the action fall within the executed period of time?
                float time = action.GetTimeOfAction();
                if (time >= start && time <= end)
                {
                    if (!action.HasPlayed())
                    {
                        action.Execute(this);
                    }
                }
            }

            currentTime = end;
        }

        private void CheckFinishState()
        {
            // Check the time to see if its over the duration
            if (currentTime >= duration)
            {
                currentTime = duration;
                Stop();
            }
        }
    }
}