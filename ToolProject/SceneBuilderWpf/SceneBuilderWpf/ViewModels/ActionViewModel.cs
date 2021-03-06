﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneBuilderWpf.DataModels;


namespace SceneBuilderWpf.ViewModels
{
    public class ActionViewModel:BaseViewModel 
    {

        private List<DataModels.Action> _actionElements;
        private DataModels.Action FireAction;
        private DataModels.Action SmokeAction;
        private DataModels.Action Extingshuer; 
        private bool _extinghuser; 

        public ActionViewModel(IPageNavigationService pageNavigation, List<DataModels.Action> actionElements) : base(pageNavigation)
        {
            _actionElements = actionElements;
            FireAction = new DataModels.Action() { ActionEnum = Actions.Fire };
            SmokeAction = new DataModels.Action() { ActionEnum = Actions.Smoke };
            Extingshuer = new DataModels.Action() { ActionEnum = Actions.Extingishuer };
        }

        public float FireIntensity
        {
            get
            {
                return FireAction.Intensity;
            }
            set
            {
                FireAction.Intensity = value;
                AddAction(value, FireAction);
                OnPropertyChanged(nameof(FireIntensity));
            }
        }

        public float FireAngleX
        {
            get
            {
                return FireAction.X;
            }
            set
            {
                FireAction.X = value;
                OnPropertyChanged(nameof(FireAngleX));
            }
        }

        public float FireAngleY
        {
            get
            {
                return FireAction.Y;
            }
            set
            {
                FireAction.Y = value;
                OnPropertyChanged(nameof(FireAngleY));
            }
        }

        public float FireAngleZ
        {
            get
            {
                return FireAction.Z;
            }
            set
            {
                FireAction.Z = value;
                OnPropertyChanged(nameof(FireAngleZ));                
            }
        }

        public float SmokeIntensity
        {
            get
            {
               return SmokeAction.Intensity;
            }
            set
            {
                SmokeAction.Intensity = value;
                AddAction(value, SmokeAction);
                OnPropertyChanged(nameof(SmokeIntensity));
            }
        }

        public float SmokeAngleX
        {
            get
            {
                return SmokeAction.X;
            }
            set
            {
                SmokeAction.X = value;
                OnPropertyChanged(nameof(SmokeAngleX));
            }
        }

        public float SmokeAngleY
        {

            get
            {
                return SmokeAction.Y;
            }
            set
            {
                SmokeAction.Y = value;
                OnPropertyChanged(nameof(SmokeAngleY));
            }
        }

        public float SmokeAngleZ
        {

            get
            {
                return SmokeAction.Z;
            }
            set
            {
                SmokeAction.Z = value;
                OnPropertyChanged(nameof(SmokeAngleZ));
            }
        }

        public bool FireExtinghusher
        {
            get
            {
                return _extinghuser;
            }
            set
            {
                _extinghuser = value;

                if (value)
                {
                    Extingshuer.Intensity = 100;
                    AddAction(100, Extingshuer);
                }
                else
                    AddAction(0, Extingshuer);

                OnPropertyChanged(nameof(FireExtinghusher));
            }
        }

        private void AddAction(float intensityvalue, DataModels.Action action )
        {
            var actionele = _actionElements.FirstOrDefault(x => x.ActionEnum == action.ActionEnum);
            if(intensityvalue > 0 && actionele == null)
            {
                _actionElements.Add(action);
                return;
            }
            if( actionele != null)
            {
                if (intensityvalue > 0)
                {
                    actionele.Intensity = intensityvalue;
                    return;
                }
                if (intensityvalue == 0)
                {
                    _actionElements.Remove(actionele);
                    return;
                }
            }
        }

        public void LoadAction(DataModels.Action action)
        {
            switch(action.ActionEnum)
            {
                case Actions.Fire:
                    FireAngleX = action.X;
                    FireAngleY = action.Y;
                    FireAngleZ = action.Z;
                    FireIntensity = action.Intensity;
                    break;
                case Actions.Smoke:
                    SmokeAngleX = action.X;
                    SmokeAngleY = action.Y;
                    SmokeAngleZ = action.Z;
                    SmokeIntensity = action.Intensity;
                    break;
                case Actions.Extingishuer:
                    Extingshuer = action;
                    if (action.Intensity == 0)
                        FireExtinghusher = false;
                    else
                        FireExtinghusher = true;
                    break;
            }
        }
        
    }
}
