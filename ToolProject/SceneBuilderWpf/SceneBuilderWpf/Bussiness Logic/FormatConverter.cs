using SceneBuilderWpf.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.Bussiness_Logic
{
    public class FormatConverter: IFormatConvert
    {
        private readonly IDocumentSerializer _documentSerializer;
        private InputParse _inputParser;

        public FormatConverter()
        {
            _documentSerializer = new JsonDocumentSerializer();
            _inputParser = new JsonInputParser();
        }

        /// <summary>
        /// turn Scenario into a JSON object.  - save 
        /// </summary>
        /// <param name="scenario"></param>
        /// <param name="targetFileName"></param>
        /// <returns></returns>
        public bool ConvertFormat(Scene scenario, string targetFileName)
        {
            var x = _documentSerializer.Serialize(scenario);
            DocumentStorage documentStorage = new JsonStorage();
            documentStorage.PersistDocument(x, targetFileName);


            /// save stuff here 
            return true;
        }

        /// <summary>
        /// Turn a JSON file into A Scenario - load 
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <returns></returns>
        public Scene ConvertFormat(string sourceFileName)
        {
            Scene Scenario;
            using (StreamReader reader = new StreamReader(File.OpenRead(sourceFileName)))
            {
                string json = reader.ReadToEnd();
                Scenario = _inputParser.ParseInput(json);
            }

            return Scenario;
        }
    }
}
