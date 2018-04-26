using System;
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
        private List<Assets> Assetslist;
        private DataModels.Action FireAction;
        private DataModels.Action SmokeAction;
        private bool _extinghuser; 

        public ActionViewModel(IPageNavigationService pageNavigation, List<DataModels.Action> actionElements, List<Assets> assetslist) : base(pageNavigation)
        {
            _actionElements = actionElements;
            Assetslist = assetslist;
            FireAction = new DataModels.Action() { ActionEnum = Actions.Fire };
            SmokeAction = new DataModels.Action() { ActionEnum = Actions.Smoke };
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
                    Assetslist.Add(Assets.Extingishuer);
                else
                    Assetslist.Remove(Assets.Extingishuer);
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
            }
        }
        
    }
}
