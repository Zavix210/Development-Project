using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevClassSetup
{
    public class Scene
    {
        File SceneFiles { get; }
        Settings GeneralSettings { get; }

        ChoiceElements CanContain  {get;}

        List<ScenceChoice> Choice { get; }

        /// last scenec  is List<Choice>.Count </Choice>
    }
    public class File
    {
        public string Video { get; }
        public string Sound { get; }
    }
    public class Settings
    {
        public int SceneBrightness { get; }
        public int EmergLight { get; }
        public int SoundVolume { get; }
        public string Text { get; }
    }

    public class ScenceChoice
    {
        public string Decision { get; }

        public string Feedback { get; }

        public Scene Whereyougo { get; }

    }

    public class ChoiceElements
{
        public bool Smoke { get; } 
        public bool Fire { get; }
        public bool Timer { get; }
        public bool Extingishuer { get; }

    }
}
