using SceneBuilderWpf.Bussiness_Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SceneBuilderWpf.ViewModels
{
    public class ScenePageViewModel : BaseViewModel
    {
        private int _tabindex = 0;
        private IFormatConvert formatConvert = new FormatConverter();
        private IndivdualSceneViewModel _currentSceneViewModel;
        private IndivdualSceneViewModel _currentComboScene;
        readonly ObservableCollection<IndivdualSceneViewModel> _Scenes = new ObservableCollection<IndivdualSceneViewModel>();
        public ObservableCollection<IndivdualSceneViewModel> Scenes => _Scenes;

        /// <summary>
        /// Current Scene selected in datagrid. 
        /// </summary>
        public IndivdualSceneViewModel CurrentScene
        {
            get => _currentSceneViewModel;
            set
            {
                _currentSceneViewModel = value;
                OnPropertyChanged(nameof(CurrentScene));
            }
        }

        /// <summary>
        /// The Item selected in the ComboBox.
        /// </summary>
        public IndivdualSceneViewModel CurrentComboScene
        {
            get => _currentComboScene;
            set
            {
                _currentComboScene = value;
                OnPropertyChanged(nameof(CurrentComboScene));
            }
        }

        public ScenePageViewModel(IPageNavigationService pageNavigation) : base(pageNavigation)
        {
            CurrentScene = new IndivdualSceneViewModel(pageNavigation);
            CurrentComboScene = CurrentScene;
        }

        public int TabIndex
        {
            get => _tabindex;
            set
            {
                _tabindex = value;
                OnPropertyChanged(nameof(TabIndex));
            }
        }

        public ICommand DesicionPage
        {
            get
            {
                return new CommandHandler(() => this.DecsionIndexChange());
            }
        }

        public void DecsionIndexChange()
        {
            if (_tabindex == 0)
                TabIndex++;
            else
                TabIndex--;
        }

        public ICommand AddNewScene
        {
            get
            {
                return new CommandHandler(() => this.NewScene());
            }
        }

        private void NewScene()
        {
            CurrentScene = new IndivdualSceneViewModel(pagenav);
            Scenes.Add(CurrentScene);
        }

        public ICommand AddToScenario
        {
            get
            {
                return new CommandHandler(() => this.AddScenario());
            }
        }

        private void AddScenario()
        {
            Scenes.Add(CurrentScene);
            CurrentScene = new IndivdualSceneViewModel(pagenav);
        }

        public ICommand SerliazeSave
        {
            get
            {
                return new CommandHandler(() => this.SerilazeAndSave());
            }
        }

        private void SerilazeAndSave()
        {
            formatConvert.ConvertFormat(CurrentScene.Scene, "TestSerliaze");
        }
    }
}
