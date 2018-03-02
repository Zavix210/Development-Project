using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevClassSetup
{
    public class Scene
    {
        /// <summary>
        /// List of Files for this particular scene 
        /// <summary/>
        File SceneFiles { get; }
        
        /// <summary>
        /// All the general settings for the scene.
        /// <summary/>
        Settings GeneralSettings { get; }
        
        /// <summary>
        /// All the general settings for the scene.
        /// <summary/>
        List<ScenceChoice> Choice { get; }

        /// last scenec  is List<Choice>.Count </Choice>
    }
    public class File
    {
        /// <summary>
        /// The video file path.
        /// <summary/>
        public string Video { get; }
        

    }
    public class Settings
    {
        /// <summary>
        /// The Scene Brightness Level.
        /// <summary/>
        public int SceneBrightness { get; }
        
        /// <summary>
        /// The Emergency Brightness Level.
        /// <summary/>
        public int EmergLight { get; }
        
        /// <summary>
        /// The Sound Volume Level.
        /// <summary/>
        public int SoundVolume { get; }
        
        /// <summary>
        /// The Text Displayed at the begining of a scene.
        /// <summary/>
        public string Text { get; }
        
        /// <summary>
        /// The Actions that could possibly be implemented. 
        /// <summary/>
        public List<Action> Elements { get; }
    }

    public class ScenceChoice
    {
        /// <summary>
        /// The desicion made.
        /// <summary/>
        public string Decision { get; }
        
        /// <summary>
        /// Feedback of the desicion made.
        /// <summary/>
        public string Feedback { get; }
        
        /// <summary>
        /// The next scene that your going to.
        /// <summary/>
        public Scene Whereyougo { get; }
    }

    public enum Action
    {
        Smoke, 
        Fire, 
        Timer, 
        Extingishuer
    };
}
