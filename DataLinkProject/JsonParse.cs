using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class MainPageViewModel
    {       
 	public void LoadJsonFileIntoScenePage()
        {
            OpenFileDialog filedia = new OpenFileDialog();
            filedia.Filter = "JSON files (*.json)| *.json"; // change if u want to include more files. 
            filedia.Multiselect = false;
            filedia.Title = "Load JSON File.";
            filedia.ShowDialog();
            if (filedia.CheckFileExists && filedia.CheckPathExists)
            {
                using (StreamReader reader = new StreamReader(filedia.OpenFile()))
                {
                    string json = reader.ReadToEnd();
                    var sceneobject = JsonConvert.DeserializeObject<Scene>(json);
                }
            }
        }
} 