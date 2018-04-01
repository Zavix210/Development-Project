using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.DataModels
{
    public class ScenceChoice
    {
        /// <summary>
        /// The desicion made.
        /// <summary/>
        public string Decision { get; set; }

        /// <summary>
        /// Feedback of the desicion made.
        /// <summary/>
        public string Feedback { get; set; }

        /// <summary>
        /// The next scene that your going to.
        /// <summary/>
        public Scene Whereyougo { get; set; }
    }

}
