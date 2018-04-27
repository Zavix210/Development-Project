using SceneBuilderWpf.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SceneBuilderWpf.ViewModels
{
    public class DecisionHolder:BaseViewModel
    {

        readonly ObservableCollection<DescisionPageViewModel> _descision = new ObservableCollection<DescisionPageViewModel>();
        private DescisionPageViewModel _currentDescisionViewModel;
        private Decision _decision;
        private ScenceChoice _scenceChoice;
        private int SceneID;

        public DecisionHolder(IPageNavigationService pageNavigation, Decision decision , int sceneID) : base(pageNavigation)
        {
            SceneID = sceneID;
            _decision = decision;
            _scenceChoice = new ScenceChoice();
            CurrentDescisionViewModel = new DescisionPageViewModel(pageNavigation, _scenceChoice, SceneID);
        }

        public string QuestionText
        {
            get => _decision.Question;
            set
            {
                _decision.Question = value;
                OnPropertyChanged(nameof(QuestionText));
            }
        }

        public float DecisionTime
        {
            get => _decision.DecisionTime;
            set
            {
                _decision.DecisionTime = value;
                OnPropertyChanged(nameof(DecisionTime));
            }
        }

        public ICommand AddDescion
        {
            get
            {
                return new CommandHandler(() => this.AddDecsi());
            }
        }

        public void AddDecsi()
        {

            if (_scenceChoice.Whereyougo == null)
            {
                var x = Xceed.Wpf.Toolkit.MessageBox.Show("This descsion has no direction are you sure you wish to put this decision in?"
                    , "Do you want to contuine?", MessageBoxButton.YesNo);
                if (x != MessageBoxResult.Yes)
                    return;
            }
            else
            {
                _scenceChoice.Whereyougo = CurrentDescisionViewModel.NextScene.scene;
            }
            _decision.Choice.Add(_scenceChoice);
            _scenceChoice = new ScenceChoice();


            Descision.Add(CurrentDescisionViewModel);
            CurrentDescisionViewModel = new DescisionPageViewModel(pagenav, _scenceChoice, SceneID);
        }

        public void LoadDecsi(ScenePageViewModel currentsceneabove)
        {
            foreach (var x in _decision.Choice)
            {
                _scenceChoice = x;
                var cene = new IndivdualSceneViewModel(pagenav, x.Whereyougo.Identifer, x.Whereyougo);
                currentsceneabove.Scenes.Add(cene);
                currentsceneabove.CurrentScene = cene;
                //currentsceneabove.LoadScenes(x.Whereyougo);

                CurrentDescisionViewModel = new DescisionPageViewModel(pagenav, _scenceChoice, SceneID);
                Descision.Add(CurrentDescisionViewModel);
            }
        }

        public ObservableCollection<DescisionPageViewModel> Descision => _descision;

        public DescisionPageViewModel CurrentDescisionViewModel
        {
            get
            {
                return _currentDescisionViewModel;
            }
            set
            {
                _currentDescisionViewModel = value;
                OnPropertyChanged(nameof(CurrentDescisionViewModel));
            }
        }
    }
}
