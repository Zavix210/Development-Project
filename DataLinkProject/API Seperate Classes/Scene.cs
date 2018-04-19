using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.DataModels
{
    public class Scene
    {
        public Scene()
        {
            GeneralSettings = new Settings();
            Choice = new List<ScenceChoice>();
        }
        public string SceneFile { get; set; }

        public Settings GeneralSettings { get; set; }

        public List<ScenceChoice> Choice { get; set; }
    }

}
