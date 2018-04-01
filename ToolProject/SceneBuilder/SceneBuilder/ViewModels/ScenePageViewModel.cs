using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Windows.Input;

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

        // Allow people to put in there own file path? if so will require checking 
        public ICommand BrowseFile
        {
            get
            {
                return new CommandHandler(async () => await this.OpenFile());
            }
        }

        /// <summary>
        /// Bussiness Logic ? 
        /// </summary>
        /// <returns></returns>
        private async Task OpenFile()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add("*"); // Get a list of all the file types i should accept MP4 etc... 
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                SceneViewModel sceneViewModel = new SceneViewModel();
                sceneViewModel.DisplayText = _currentSceneViewModel.DisplayText;
                sceneViewModel.EmergencyLighting = _currentSceneViewModel.EmergencyLighting;
                sceneViewModel.FileName = file.Path;
                sceneViewModel.SoundVolume = _currentSceneViewModel.SoundVolume;
                sceneViewModel.SceneBrightness = _currentSceneViewModel.SceneBrightness;
                _currentSceneViewModel.FileName = file.Path;
                CurrentSceneViewModel = sceneViewModel;
                var x = _Scenes;
            }
        }

    }
}
