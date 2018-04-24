using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.DataModels
{
    public class Decision
    {
        public Decision()
        {
            Choice = new List<ScenceChoice>();
        }

        public string Question;

        public float time; 

        public List<ScenceChoice> Choice { get; set; }
    }

}
