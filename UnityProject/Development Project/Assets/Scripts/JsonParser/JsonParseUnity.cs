using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SceneBuilderWpf.DataModels;

    public static class JsonParseUnity
    {
        public static Scene LoadJsonFileIntoScenePage(string filePath)
        {
            Scene sceneobject;
            using (StreamReader file = System.IO.File.OpenText(filePath))
            {
                string json = file.ReadToEnd();
                sceneobject = JsonConvert.DeserializeObject<Scene>(json);
            }
            return sceneobject;
        }
    } 