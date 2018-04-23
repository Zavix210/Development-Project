using Newtonsoft.Json;
using SceneBuilderWpf.DataModels;

namespace SceneBuilderWpf.Bussiness_Logic
{
    public class JsonDocumentSerializer:IDocumentSerializer
    {
        public string Serialize(Scene scenario)
        {
            return JsonConvert.SerializeObject(scenario, Formatting.Indented);
        }
    }
}
