using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


    public class JSONSceneLoader
    {
        public void LoadJsonFileIntoScenePage(string filePath)
        {

            //just testing
            filePath = @"c:\test.json";

            Scene sceneobject;
            using (StreamReader file = System.IO.File.OpenText(filePath))
            {
                string json = file.ReadToEnd();
                sceneobject = JsonConvert.DeserializeObject<Scene>(json);
            }
        }
    } 