using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace SceneBuilder.ViewModels
{
    public class ScenePageViewModel: BaseViewModel
    {
        private SceneViewModel _currentSceneViewModel;
        readonly ObservableCollection<SceneViewModel> _Scenes = new ObservableCollection<SceneViewModel>();
        
        public ScenePageViewModel()
        {
            _currentSceneViewModel = new SceneViewModel();
            _Scenes.Add(_currentSceneViewModel);
        }

        public SceneViewModel CurrentSceneViewModel
        {
            get => _currentSceneViewModel;
            set
            {
                _currentSceneViewModel = value;
                OnPropertyChanged(nameof(CurrentSceneViewModel));
            }
        }

        public ObservableCollection<SceneViewModel> Scenes => _Scenes;


    }
}
