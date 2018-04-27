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


#if !Debug
        private string directorystring = @"../../../../../UnityProject/Development Project/Assets/JsonScene";

#endif

#if Debug
             private string directorystring = @"../unitybuildtest/Build_Data/JsonScene";
#endif

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
            SceneId = sceneid;
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
                //Use relative file path here!!!
                var x = "";
                #if Debug
                    Directory.CreateDirectory(directorystring);
                    string filename = Path.GetFileName(openFileDialog.FileName);
                    if (!File.Exists(directorystring+ "/" + filename))
                        File.Copy(openFileDialog.FileName, directorystring + "/" + Path.GetFileName(openFileDialog.FileName));
                    FileName = openFileDialog.FileName; 
                    
                    TimeOfVideo = ShellCommandtoFindLength.Filelength(directorystring + "/" + _fileName);
                #endif
                #if !Debug
                    Directory.CreateDirectory(directorystring);
                    string filename = Path.GetFileName(openFileDialog.FileName);
                    if (!File.Exists(directorystring + "/" + filename))
                        File.Copy(openFileDialog.FileName, directorystring + "/" + Path.GetFileName(openFileDialog.FileName));
                    FileName = openFileDialog.FileName; //Set textblock to filename. chossen

                    //use relative file path + value
                    //"../../../../../UnityProject/Development Project/Assets/JsonScene"
                    TimeOfVideo = ShellCommandtoFindLength.Filelength(@"../../../../../UnityProject/Development Project/Assets/JsonScene/" + "//" + _fileName);
                #endif
            }


        }

        public ICommand BrowseAudio
        {
            get
            {
                return new CommandHandler(() => this.BrosweAudioFile());
            }
        }

        public void BrosweAudioFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), // Dis-allow MultiSelect and set default patht to there Special folder.
                Multiselect = false,
                Filter = "Image files (*.wav) |*.wav;"
            };
            if (openFileDialog.ShowDialog() == true && File.Exists(openFileDialog.FileName))
            {

                #if Debug
                    Directory.CreateDirectory(directorystring
                    string filename = Path.GetFileName(openFileDialog.FileName);
                    if (!File.Exists(directorystring + / + filename))
                        File.Copy(Path.GetFileName(openFileDialog.FileName), directorystring + "/" + filename);
                    AlarmPath = openFileDialog.FileName; 
                #endif
                #if !Debug
                    Directory.CreateDirectory(directorystring);
                    string filename = Path.GetFileName(openFileDialog.FileName);
                    if (!File.Exists(directorystring + "/" + filename))
                        File.Copy(openFileDialog.FileName, directorystring + "/" + Path.GetFileName(openFileDialog.FileName));
                    AlarmPath = openFileDialog.FileName; //Set textblock to filename. chossen
                #endif
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
            set
            {
                SceneID = value;
                scene.Identifer = value;
            }
        }

        public double TimeOfVideo
        {
            get
            {
                return scene.SceneLength;
            }
            set
            {
                scene.SceneLength = value;
                OnPropertyChanged(nameof(TimeOfVideo));
            }
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                scene.SceneFile = Path.GetFileName(value); 
                _fileName = Path.GetFileName(value);

                

                DisplayString = "";
                OnPropertyChanged(nameof(FileName));
            }
        }

        public string AlarmPath
        {
            get
            {
                return scene.GeneralSettings.AlarmSoundPath;
            }
            set
            {
                scene.GeneralSettings.AlarmSoundPath = Path.GetFileName(value);

                //create relative file path here!!!

                OnPropertyChanged(nameof(AlarmPath));
            }
        }
    
        public int AlarmVolume
        {
            get
            {
                return scene.GeneralSettings.AlarmVolume;
            }
            set
            {
                scene.GeneralSettings.AlarmVolume = value;
                OnPropertyChanged(nameof(AlarmVolume));
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
