using Microsoft.Win32;
using SceneBuilderWpf.Bussiness_Logic;
using SceneBuilderWpf.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace SceneBuilderWpf.ViewModels
{
    public class ScenePageViewModel : BaseViewModel
    {
        private int _tabindex = 0;
        private IFormatConvert formatConvert;
        private int SceneID = 1;
        //private string parentID = "";
        private Scene firstscene;
        private IndivdualSceneViewModel _currentSceneViewModel;
        private IndivdualSceneViewModel _currentComboScene;
        readonly ObservableCollection<IndivdualSceneViewModel> _Scenes = new ObservableCollection<IndivdualSceneViewModel>();
        public ObservableCollection<IndivdualSceneViewModel> Scenes => _Scenes;


        /// <summary>
        /// Current Scene selected in datagrid. 
        /// </summary>
        public IndivdualSceneViewModel CurrentScene
        {
            get => _currentSceneViewModel;
            set
            {
                _currentSceneViewModel = value;
                OnPropertyChanged(nameof(CurrentScene));
            }
        }

        /// <summary>
        /// The Item selected in the ComboBox.
        /// </summary>
        public IndivdualSceneViewModel CurrentComboScene
        {
            get => _currentComboScene;
            set
            {
                _currentComboScene = value;
                OnPropertyChanged(nameof(CurrentComboScene));
            }
        }

        public ScenePageViewModel(IPageNavigationService pageNavigation, ScenarioStorer scenarioStorer, IFormatConvert Formatconvert) : base(pageNavigation)
        {
            Scene currentScene = scenarioStorer.NewScene ? scenarioStorer.Scenerio :  new Scene();
            formatConvert = Formatconvert;
            CurrentScene = new IndivdualSceneViewModel(pageNavigation, SceneID, currentScene);
            firstscene = CurrentScene.scene;
            Scenes.Add(CurrentScene);
            if (scenarioStorer.NewScene == true)
                LoadScenes(CurrentScene);

            CurrentComboScene = CurrentScene;
        }

        public int TabIndex
        {
            get => _tabindex;
            set
            {
                _tabindex = value;
                OnPropertyChanged(nameof(TabIndex));
            }
        }

        public ICommand DesicionPage
        {
            get
            {
                return new CommandHandler(() => this.DecsionIndexChange());
            }
        }

        public void DecsionIndexChange()
        {
            if (_tabindex == 0)
                TabIndex = 1;
            else
                TabIndex = 0;
        }

        public ICommand AddNewScene
        {
            get
            {
                return new CommandHandler(() => this.NewScene());
            }
        }

        private void NewScene()
        {
            SceneID++;
            CurrentScene = new IndivdualSceneViewModel(pagenav, SceneID, new Scene());
            Scenes.Add(CurrentScene);
        }

        public ICommand RunUnity
        {
            get
            {
                return new CommandHandler(() => this.RunSaveSerilaze());
            }
        }

        public void RunSaveSerilaze()
        {
            formatConvert.ConvertFormat(firstscene, @"C:\Temp\unitybuildtest\Build_Data\JsonScene\", "scene");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "C:\\Temp\\unitybuildtest\\Build.exe",
                Arguments = "Scene.Json"
            };
            Process.Start(startInfo);
        }

        public ICommand SerliazeSave
        {
            get
            {
                return new CommandHandler(() => this.SerilazeAndSave());
            }
        }

        private void SerilazeAndSave()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
               // CheckFileExists = true,

                CheckPathExists = true,

                Title = "Save Json Files",
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json"
            };
            if (saveFileDialog.ShowDialog().Value)
            {
                string filname = Path.GetFileName(saveFileDialog.FileName);
                string filloc= Path.GetDirectoryName(saveFileDialog.FileName);
                formatConvert.ConvertFormat(firstscene, filloc, filname);
            }
        }

        public ICommand SceneDiagram
        {
            get
            {
                return new CommandHandler(() => this.UpdateScenePage());
            }
        }

        private void UpdateScenePage()
        {
            Scenario1View = new ObservableCollection<Scene>();
            /*
            try
            {
                foreach (IndivdualSceneViewModel f in Scenes)
                {
                  if(f.ParentId != null )
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    this.DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                listBox1.Items.Add(ex.Message);
            }
            */
        }
        
        public void LoadScenes(IndivdualSceneViewModel sceneViewModel)
        {

            if (SceneID == 1)
            {
                CurrentScene.FileName = firstscene.SceneFile;
            }

            foreach (var x in sceneViewModel.scene.DecisionList)
            {
                DecisionHolder descisionholder= new DecisionHolder(pagenav, x, sceneViewModel.SceneId);
                foreach (var y in x.Choice)
                {
                    IndivdualSceneViewModel viewModel = Scenes.Where(j => j.SceneId == y.Whereyougo.Identifer).FirstOrDefault();
                    if (viewModel == null)
                    {
                        
                        IndivdualSceneViewModel indivdualSceneView = new IndivdualSceneViewModel(pagenav, y.Whereyougo.Identifer, y.Whereyougo)
                        {
                            FileName = y.Whereyougo.SceneFile
                        };

                        Scenes.Add(indivdualSceneView);
                        indivdualSceneView.LoadInScene();

                        LoadScenes(indivdualSceneView);
                        viewModel = indivdualSceneView;
                    }
                    descisionholder.Descision.Add(new DescisionPageViewModel(pagenav, y, y.Whereyougo.Identifer) { NextScene = viewModel });
                }
                sceneViewModel.DescisionHolder.Add(descisionholder);
            }


        }

        ObservableCollection<Scene> Scenario1View { get; set; }

    }
}
