using Newtonsoft.Json;
using SceneBuilderWpf.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.Bussiness_Logic
{
    public class JsonDocumentSerializer:IDocumentSerializer
    {
        public string Serialize(Scene scenario)
        {
            return JsonConvert.SerializeObject(scenario);
        }
    }
}
