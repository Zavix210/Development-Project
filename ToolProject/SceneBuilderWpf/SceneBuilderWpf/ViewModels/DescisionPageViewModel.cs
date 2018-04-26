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
        private int _sceneid; 

        public DescisionPageViewModel(IPageNavigationService pageNavigation, ScenceChoice choice, int SceneID) : base(pageNavigation)
        {
            scenceChoice = choice;
            _sceneid = SceneID;
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

        public int Score
        {
            get
            {
                return scenceChoice.Score;
            }
            set
            {
                scenceChoice.Score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public IndivdualSceneViewModel NextScene
        {
            get => _nextscene;
            set
            {
                _nextscene = value;
                scenceChoice.Whereyougo = _nextscene.scene;
                OnPropertyChanged(nameof(NextScene));
            }
        }

    }
}
