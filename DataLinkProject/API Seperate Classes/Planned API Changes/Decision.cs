using System.Collections.Generic;

namespace SceneBuilderWpf.DataModels
{
    public class Decision
    {
        public Decision()
        {
            Choice = new List<ScenceChoice>();
        }

        public string Question { get; set; }

        public float DecisionTime { get; set; }

        public List<ScenceChoice> Choice { get; set; }
    }

}
