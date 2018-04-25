using System.Collections.Generic;

namespace SceneBuilderWpf.DataModels
{
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
        /// The Text Displayed at the begining of a scene. Introudciton like 3rd level of hosptial X wing ... 
        /// <summary/>
        public string Text { get; set; }

        /// <summary>
        /// The Actions that could possibly be implemented. 
        /// <summary/>
        private List<Action> _actionElements = new List<Action>();

        public List<Action> ActionElements
        {
            get
            {
               return _actionElements;
            }
            set
            {
                _actionElements = value;
            }
        }

        private List<Assets> _assetElements = new List<Assets>();

        public List<Assets> AssetElements
        {
            get
            {
                return _assetElements;
            }
            set
            {
                _assetElements = value;
            }
        }
    }
}
