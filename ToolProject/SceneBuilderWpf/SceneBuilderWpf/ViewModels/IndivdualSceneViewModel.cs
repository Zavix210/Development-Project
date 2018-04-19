using Microsoft.Win32;
using SceneBuilderWpf.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private DescisionPageViewModel _currentDescisionViewModel;
        public DescisionPageViewModel CurrentDescision
        {
            get => _currentDescisionViewModel;
            set => _currentDescisionViewModel = value;
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
            Descision.Add(CurrentDescision);
            ScenceChoice scenceChoice = new ScenceChoice();
            Scene.Choice.Add(scenceChoice);
            
            CurrentDescision = new DescisionPageViewModel(pagenav, scenceChoice, SceneID);
        }

        public IndivdualSceneViewModel(IPageNavigationService pageNavigation, int sceneid) : base(pageNavigation)
        {
           
            Scene = new Scene();
            ScenceChoice scenceChoice = new ScenceChoice();
            SceneID = sceneid;
            SceneSettings = Scene.GeneralSettings;
            Scene.Choice.Add(scenceChoice);
            CurrentDescision = new DescisionPageViewModel(pageNavigation, scenceChoice, SceneID);
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

        /// <summary>
        /// What the Combobox will show. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FileName;
        }
    }
}
