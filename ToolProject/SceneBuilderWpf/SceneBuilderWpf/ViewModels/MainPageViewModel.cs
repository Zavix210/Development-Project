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
        public MainPageViewModel(IPageNavigationService pageNavigation) : base(pageNavigation)
        {
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
            OpenFileDialog filedia = new OpenFileDialog();
            filedia.Filter = "JSON files (*.json)| *.json"; // change if u want to include more files. 
            filedia.Multiselect = false;
            filedia.Title = "Load JSON File.";
            filedia.ShowDialog();
            if (filedia.CheckFileExists && filedia.CheckPathExists)
            {

                IFormatConvert format = new FormatConverter();
                format.ConvertFormat(filedia.FileName);



            }
        }
    }

}
