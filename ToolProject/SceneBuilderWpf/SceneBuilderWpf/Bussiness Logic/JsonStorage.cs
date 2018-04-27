using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.Bussiness_Logic
{
    public class JsonStorage : DocumentStorage
    {

        public override string GetData(string filename) // Retrive the Json object which has already been saved. 
        {
            string jsondata;
            using (StreamReader reader = new StreamReader(File.OpenRead(filename)))
            {
                jsondata = reader.ReadToEnd();
            }
            return jsondata;
        }

        public override string[] GetDirectorysData(string Directory)
        {
            throw new NotImplementedException();
        }

        public override void PersistDocument(string serializedScene,string locationfile ,string targetFileName) // Save the Json Object to a file 
        {
            //@"../../../../../UnityProject/Development Project/Assets/JsonScene/{0}.json"
            using (StreamWriter file = File.CreateText(string.Format(locationfile +"\\{0}", targetFileName)))
            {
                file.WriteLine(serializedScene);
            }
        }
    }
}
