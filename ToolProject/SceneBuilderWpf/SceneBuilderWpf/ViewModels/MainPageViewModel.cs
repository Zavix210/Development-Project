using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SceneBuilderWpf.Bussiness_Logic;
using SceneBuilderWpf.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SceneBuilderWpf.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ScenarioStorer _scenariostorer;
        public MainPageViewModel(IPageNavigationService pageNavigation, ScenarioStorer scenarioStorer) : base(pageNavigation)
        {
            _scenariostorer = scenarioStorer;
        }

        public ICommand ChangePagCommand
        {
            get
            {
                return new CommandHandler(() => this.ChangePage());
            }
        }

        private void ChangePage()
        {
            pagenav.Navigate<ScenePage>();
        }

        public ICommand LoadJsonFile
        {
            get
            {
                return new CommandHandler(() => LoadJsonFileIntoScenePage());
            }
        }

        public void LoadJsonFileIntoScenePage()
        {
            OpenFileDialog filedia = new OpenFileDialog
            {
                Filter = "JSON files (*.json)| *.json", // change if u want to include more files. 
                Multiselect = false,
                Title = "Load JSON File."
            };
            filedia.ShowDialog();
            if (filedia.CheckFileExists && filedia.CheckPathExists)
            { 
                IFormatConvert format = new FormatConverter();
                Scene scene = format.ConvertFormat(filedia.FileName);
                _scenariostorer.NewScene = true;
                _scenariostorer.Scenerio = scene;
            }
            pagenav.Navigate<ScenePage>();
        }
    }

}
