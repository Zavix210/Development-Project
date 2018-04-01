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
            throw new NotImplementedException();
        }

        public override string[] GetDirectorysData(string Directory)
        {
            throw new NotImplementedException();
        }

        public override void PersistDocument(string serializedScene, string targetFileName) // Save the Json Object to a file 
        {
            using (StreamWriter file = File.CreateText(string.Format(@"C:\Temp\{0}.json ", targetFileName)))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, serializedScene);
            }
        }
    }
}
