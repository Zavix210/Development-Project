using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class Scene
    {
        /// <summary>
        /// List of Files for this particular scene 
        /// <summary/>
        File SceneFiles { get; set; }
        
        /// <summary>
        /// All the general settings for the scene.
        /// <summary/>
        Settings GeneralSettings { get; set; }
        
        /// <summary>
        /// All the general settings for the scene.
        /// <summary/>
        List<ScenceChoice> Choice { get; set; }

        /// last scenec  is List<Choice>.Count </Choice>
    }
    public class File
    {
        /// <summary>
        /// The video file path.
        /// <summary/>
        public string Video { get; set; }
        

    }
    public class Settings
    {
        /// <summary>
        /// The Scene Brightness Level.
        /// <summary/>
        public int SceneBrightness { get; set; }
        
        /// <summary>
        /// The Emergency Brightness Level.
        /// <summary/>
        public int EmergLight { get; set; }
        
        /// <summary>
        /// The Sound Volume Level.
        /// <summary/>
        public int SoundVolume { get; set; }
        
        /// <summary>
        /// The Text Displayed at the begining of a scene.
        /// <summary/>
        public string Text { get; set; }
        
        /// <summary>
        /// The Actions that could possibly be implemented. 
        /// <summary/>
        public List<Action> Elements { get; set; }
    }

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

    public enum Action
    {
        Smoke, 
        Fire, 
        Timer, 
        Extingishuer
    };

