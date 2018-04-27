using System.Collections.Generic;

namespace SceneBuilderWpf.DataModels
{
    public class Scene
    {
        public Scene()
        {
            GeneralSettings = new Settings();
            DecisionList = new List<Decision>();
        }

        public int Identifer { get; set;}

        public string SceneFile { get; set; }
        /// <summary>
        /// In Milliseconds!
        /// </summary>
        public double SceneLength { get; set; }

        public Settings GeneralSettings { get; set; }

        public List<Decision> DecisionList { get; set; }

    }

}
