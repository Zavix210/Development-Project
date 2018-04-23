using Microsoft.Win32;
using SceneBuilderWpf.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace SceneBuilderWpf.ViewModels
{
    public class IndivdualSceneViewModel : BaseViewModel
    {
        public Scene Scene;
        private Settings SceneSettings;
        private string _fileName;
        public int SceneID;
        public string ParentId = "";

        
        readonly ObservableCollection<DescisionPageViewModel> _descision = new ObservableCollection<DescisionPageViewModel>();
        public ObservableCollection<DescisionPageViewModel> Descision => _descision;
        private ScenceChoice _scenceChoice = new ScenceChoice();
        private DescisionPageViewModel _currentDescisionViewModel;
        private ActionViewModel _viewModelAction;


        public DescisionPageViewModel CurrentDescision
        {
            get => _currentDescisionViewModel;
            set
            {
                _currentDescisionViewModel = value;
                OnPropertyChanged(nameof(CurrentDescision));
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
                CurrentDescision.NextScene.ParentId = SceneID.ToString();
                _scenceChoice.Whereyougo = CurrentDescision.NextScene.Scene;
            }

            Scene.Choice.Add(_scenceChoice);

            _scenceChoice = new ScenceChoice();


            CurrentDescision = new DescisionPageViewModel(pagenav, _scenceChoice, SceneID);
            Descision.Add(CurrentDescision);

        }

        public IndivdualSceneViewModel(IPageNavigationService pageNavigation, int sceneid) : base(pageNavigation)
        {
           
            Scene = new Scene();

            SceneID = sceneid;
            SceneSettings = Scene.GeneralSettings;
            ViewModelAction = new ActionViewModel(pageNavigation, Scene.GeneralSettings.ActionElements);
            CurrentDescision = new DescisionPageViewModel(pageNavigation, _scenceChoice, SceneID);
            Descision.Add(CurrentDescision);

        }

        public ICommand BrowseCommand
        {
            get
            {
                return new CommandHandler(() => this.BrosweFile());
            }
        }

        public void BrosweFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), // Dis-allow MultiSelect and set default patht to there Special folder.
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == true && File.Exists(openFileDialog.FileName))
            {
                FileName = openFileDialog.FileName; //Set textblock to filename. chossen
            }

        }

        public int SceneId
        {
            get
            {
                return SceneID;
            }
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                Scene.SceneFile = value;
                _fileName = Path.GetFileName(value);
                OnPropertyChanged(nameof(FileName));
            }
        }

        public int SceneBrightness
        {
            get => SceneSettings.SceneBrightness;
            set
            {
                SceneSettings.SceneBrightness = value;
                OnPropertyChanged(nameof(SceneBrightness));
            }
        }  

        public int SoundVolume
        {
            get => SceneSettings.SoundVolume;
            set
            {
                SceneSettings.SoundVolume = value;
                OnPropertyChanged(nameof(SoundVolume));
            }
        }

        public int EmergencyLighting
        {
            get => SceneSettings.EmergLight;
            set
            {
                SceneSettings.EmergLight = value;
                OnPropertyChanged(nameof(EmergencyLighting));
            }
        }

        public string QuestionText
        {
            get => SceneSettings.QuestionText;
            set
            {
                SceneSettings.QuestionText = value;
                OnPropertyChanged(nameof(QuestionText));
            }
        }

        public string IntroducitonText
        {
            get => SceneSettings.Text;
            set
            {
                SceneSettings.Text = value;
                OnPropertyChanged(nameof(IntroducitonText));
            }
        }

        public ActionViewModel ViewModelAction
        {
            get
            {
                return _viewModelAction;
            }
            set
            {
                _viewModelAction = value;
                OnPropertyChanged(nameof(ViewModelAction));
            }
        }

        /// <summary>
        /// What the Combobox will show. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SceneID.ToString()  + ": " + FileName;
        }
    }
}
