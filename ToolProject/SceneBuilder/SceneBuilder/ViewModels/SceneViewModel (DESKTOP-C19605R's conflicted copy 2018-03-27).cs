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
        private string _filePath;
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
        [Ignore]
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

        // Allow people to put in there own file path? if so will require checking 
        public ICommand BrowseFile
        {
            get
            {
                return new CommandHandler(async () => await this.OpenFile());
            }
        }

        /// <summary>
        /// Bussiness Logic ? 
        /// </summary>
        /// <returns></returns>
        private async Task OpenFile()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add("*"); // Get a list of all the file types i should accept MP4 etc... 
            StorageFile file =  await picker.PickSingleFileAsync();
            if(file != null)
            {
                FileName = file.Path;
            }
        }

        [Display(Header = "Scene Brightness :")]
        public int SceneBrightness
        {
            get => SceneSettings.SceneBrightness;
            set
            {
                if (value <= 100 && value >= 0)
                {
                    SceneSettings.SceneBrightness = value;
                    OnPropertyChanged(nameof(SceneBrightness));
                }
                else
                {
                    SceneSettings.SceneBrightness = 0;
                    OnPropertyChanged(nameof(SceneBrightness));

                }
            }
        }

        [Display(Header = "Sound Volume :")]
        public int SoundVolume
        {
            get => SceneSettings.SoundVolume;
            set => SceneSettings.SoundVolume = value;
        }

        [Display(Header = "Emergency Lighting :")]
        public int EmergencyLighting
        {
            get => SceneSettings.SoundVolume;
            set => SceneSettings.SoundVolume = value;
        }

        [Display(Header = "Introduction Text :")]
        public string DisplayText
        {
            get => SceneSettings.Text;
            set => SceneSettings.Text = value;
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public bool HasErrors
        {
            get
            {
                return this.errors.Count > 0;
            }
        }

        public Task ValidatePropertyAsync(Entity entity, string propertyName)
        {
            if (propertyName.Equals("Age"))
            {
                var property = entity.GetEntityProperty(propertyName);
                double value;
                Double.TryParse(property.PropertyValue.ToString(), out value);

                if (value < 18)
                {
                    this.errors[propertyName] = new List<string>() { "Age under 18 is not allowed!" };
                }
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
