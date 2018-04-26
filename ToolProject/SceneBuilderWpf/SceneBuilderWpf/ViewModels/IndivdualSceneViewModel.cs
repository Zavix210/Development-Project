using Microsoft.Win32;
using SceneBuilderWpf.DataModels;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System.Collections.Generic;
using SceneBuilderWpf.Bussiness_Logic;
using System.Reflection;

namespace SceneBuilderWpf.ViewModels
{
    public class IndivdualSceneViewModel : BaseViewModel
    {
        public Scene scene;
        private Settings SceneSettings;
        private string _fileName;
        public int SceneID;
        public string ParentId = "";
        private Decision _decision;
        private ActionViewModel _viewModelAction;

        readonly ObservableCollection<DecisionHolder> _descisionHolder = new ObservableCollection<DecisionHolder>();
        public ObservableCollection<DecisionHolder> DescisionHolder => _descisionHolder;

        private DecisionHolder _currentDecisionHolder;
        public DecisionHolder CurrentDecisionHolder
        {
            get => _currentDecisionHolder;
            set
            {
                _currentDecisionHolder = value;
                OnPropertyChanged(nameof(CurrentDecisionHolder));
            }
        }

        public IndivdualSceneViewModel(IPageNavigationService pageNavigation, int sceneid, Scene scene_) : base(pageNavigation)
        {
            scene = scene_;
            SceneID = sceneid;
            SceneSettings = scene.GeneralSettings;
            ViewModelAction = new ActionViewModel(pageNavigation, scene.GeneralSettings.ActionElements);

            _decision = new Decision();
            CurrentDecisionHolder = new DecisionHolder(pageNavigation, _decision, SceneId);
            DescisionHolder.Add(CurrentDecisionHolder);
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
                Multiselect = false,
                Filter = "Image files (*.mp4, *.m4v, *.mov) |*.mp4;*.m4v;*.mov;"
            };
            if (openFileDialog.ShowDialog() == true && File.Exists(openFileDialog.FileName))
            {
                FileName = openFileDialog.FileName; //Set textblock to filename. chossen
            }

        }

        public ICommand AddaDecision
        {
            get
            {
                return new CommandHandler(() => this.AddSingleDecsion());
            }
        }

        public void AddSingleDecsion()
        {        
            scene.DecisionList.Add(_decision);
            _decision = new Decision();
            CurrentDecisionHolder = new DecisionHolder(pagenav, _decision, SceneId);
            DescisionHolder.Add(CurrentDecisionHolder);
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
                scene.SceneFile = Path.GetFileName(value); ;
                scene.SceneLength = ShellCommandtoFindLength.Filelength(value);
                _fileName = Path.GetFileName(value);
                if(!File.Exists("../../../../../UnityProject/Development Project/Assets/JsonScene/" + _fileName))
                    File.Copy(value, "../../../../../UnityProject/Development Project/Assets/JsonScene/"+_fileName);
                //"../../../../../UnityProject/Development Project/Assets/JsonScene"
                DisplayString = "";
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

        public void LoadInScene()
        {
            foreach(var x in scene.GeneralSettings.ActionElements)
            {
                _viewModelAction.LoadAction(x);
            }

        }

        public string DisplayString
        {
            get
            {
                return SceneID.ToString() + ": " + FileName;
            }
            set
            {
                OnPropertyChanged(nameof(DisplayString));
            }
        }
    }
}
