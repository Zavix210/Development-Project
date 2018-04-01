using SceneBuilderWpf.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.ViewModels
{
    public class DescisionPageViewModel : BaseViewModel
    {

        private IndivdualSceneViewModel _nextscene;

        private ScenceChoice scenceChoice;

        public DescisionPageViewModel(IPageNavigationService pageNavigation, ScenceChoice choice) : base(pageNavigation)
        {
            scenceChoice = choice;
        }

        public string Feedback
        {
            get => scenceChoice.Feedback;
            set
            {
                scenceChoice.Feedback = value;
                OnPropertyChanged(nameof(Feedback));
            }
        }

        public string ChoiceText
        {
            get => scenceChoice.Decision;
            set
            {
                scenceChoice.Decision = value;
                OnPropertyChanged(nameof(ChoiceText));
            }
        }
        
        public IndivdualSceneViewModel NextScene
        {
            get => _nextscene;
            set
            {
                
                _nextscene = value;
                scenceChoice.Whereyougo = _nextscene.Scene;
                OnPropertyChanged(nameof(NextScene));
            }
        }

    }
}
