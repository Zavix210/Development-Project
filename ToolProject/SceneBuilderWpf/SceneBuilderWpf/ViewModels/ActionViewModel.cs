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
        private DataModels.Action FireAction;
        private DataModels.Action SmokeAction; 

        public ActionViewModel(IPageNavigationService pageNavigation, List<DataModels.Action> actionElements) : base(pageNavigation)
        {
            _actionElements = actionElements;
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

        public float FireAngle
        {
            get
            {
                return FireAction.Angle;
            }
            set
            {
                FireAction.Angle = value;
                OnPropertyChanged(nameof(FireAngle));                
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

        public float SmokeAngle
        {

            get
            {
                return SmokeAction.Angle;
            }
            set
            {
                SmokeAction.Angle = value;
                OnPropertyChanged(nameof(SmokeAngle));
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
                    FireAngle = action.Angle;
                    FireIntensity = action.Intensity;
                    break;
                case Actions.Smoke:
                    SmokeAngle = action.Angle;
                    SmokeIntensity = action.Intensity;
                    break;
            }
        }
        
    }
}
