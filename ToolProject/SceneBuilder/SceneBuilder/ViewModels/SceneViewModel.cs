using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using Windows.Storage.Pickers;
using Windows.Storage;
using Telerik.Data.Core;
using SceneBuilder.DataModels;
using System.Collections;
using System.ComponentModel;
using Telerik.Core;

namespace SceneBuilder.ViewModels
{
    public class SceneViewModel: ViewModelBase, ISupportEntityValidation
    {
        /// <summary>
        /// What will be displayed to the user.
        /// </summary>
        private string _fileName;
        private Scene Scene;
        private Settings SceneSettings;

        public SceneViewModel()
        {
            Scene = new Scene();
            SceneSettings =  Scene.GeneralSettings;
        }

        /// <summary>
        /// Getter and Setter For Binding.
        /// Telerik ignore So doesn't appear in data grid. 
        /// </summary>
        [Display(Header = "Scene File :")]
        public string FileName
        {
            get => _fileName;
            set
            {
                Scene.SceneFile = value;
                _fileName = Path.GetFileName(value);
                OnPropertyChanged(nameof(FileName));     
            }
        }


        [Display(Header = "Scene Brightness :")]
        public int SceneBrightness
        {
            get => SceneSettings.SceneBrightness;
            set
            {
                SceneSettings.SceneBrightness = value;
                OnPropertyChanged(nameof(SceneBrightness));
            }
        }

        [Display(Header = "Sound Volume :")]
        public int SoundVolume
        {
            get => SceneSettings.SoundVolume;
            set
            {
                SceneSettings.SoundVolume = value;
                OnPropertyChanged(nameof(SoundVolume));
            }
        }

        [Display(Header = "Emergency Lighting :")]
        public int EmergencyLighting
        {
            get => SceneSettings.EmergLight;
            set
            {
                SceneSettings.EmergLight = value;
                OnPropertyChanged(nameof(EmergencyLighting));
            }
        }

        [Display(Header = "Introduction Text :", PlaceholderText ="Intial scene text displayed at the start.")]
        public string DisplayText
        {
            get => SceneSettings.Text;
            set
            {
                SceneSettings.Text = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public bool HasErrors
        {
            get
            {
                return this.errors.Count > 0;
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public Task ValidatePropertyAsync(Entity entity, string propertyName)
        {
            if (propertyName.Equals("EmergencyLighting") || propertyName.Equals("SoundVolume") || propertyName.Equals("SceneBrightness"))
            {
                var property = entity.GetEntityProperty(propertyName);
                double value;
                Double.TryParse(property.PropertyValue.ToString(), out value);

                if (value < 0 )
                {
                    
                    this.errors[propertyName] = new List<string>() {string.Format("{0} under 0 is not allowed!", property.Label) };
                }
                else if( value> 100)
                    this.errors[propertyName] = new List<string>() { string.Format("{0} over 100 is not allowed!", property.Label) };
                else
                {
                    this.errors[propertyName] = new List<string>();
                }

                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else if (propertyName.Equals("Name"))
            {
                var name = entity.GetEntityProperty(propertyName).PropertyValue.ToString();
                if (String.IsNullOrEmpty(name))
                {
                    this.errors[propertyName] = new List<string>() { "The Name cannot be empty!" };
                }
                else
                {
                    this.errors[propertyName] = new List<string>();
                }

                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }


            return null;
        }

        
        public IEnumerable GetErrors(string propertyName)
        {
            if (this.errors.ContainsKey(propertyName))
            {
                return this.errors[propertyName];
            }

            return new List<string>();
        }
    


    }
}