using GalaSoft.MvvmLight;
using Microsoft.Win32;
using SceneBuilderWpf.Bussiness_Logic;
using SceneBuilderWpf.DataModels;
using System.Windows;
using System.Windows.Input;

namespace SceneBuilderWpf.ViewModels
{

    public class MainViewModel : BaseViewModel
    {
        private ScenarioStorer _scenariostorer;
        private IFormatConvert FormatConvert;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IPageNavigationService pageNavigation, IFormatConvert formatConvert ,ScenarioStorer scenarioStorer) : base(pageNavigation)
        {
            FormatConvert = formatConvert;
            _scenariostorer = scenarioStorer;
        }
        
        public ICommand Minmize
        {
            get
            {
                return new CommandHandler(() => this.Min());
            }
        }

        private void Min()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        public ICommand ShutDown
        {
            get
            {
                return new CommandHandler(() => this.ShutDownApp());
            }
        }

        private void ShutDownApp()
        {
            Application.Current.Shutdown();
        }

        public ICommand HomeCommand
        {
            get
            {
                return new CommandHandler(() => this.HomePage());
            }
        }

        private void HomePage()
        {
            _scenariostorer.NewScene = false;
            _scenariostorer.Scenerio = null;
            pagenav.Navigate<MainPage>();
        }

        public ICommand SceneCommand
        {
            get
            {
                return new CommandHandler(() => this.ScenePage());
            }
        }

        private void ScenePage()
        {
            pagenav.Navigate<ScenePage>();
        }

        public ICommand LoadJson
        {
            get
            {
                return new CommandHandler(() => ScenePageLoad());
            }
        }

        private void ScenePageLoad()
        {
            OpenFileDialog filedia = new OpenFileDialog
            {
                Filter = "JSON files (*.json)| *.json", // change if u want to include more files. 
                Multiselect = false,
                Title = "Load JSON File."
            };
            var openbrowser = (bool)filedia.ShowDialog();
            if (filedia.CheckFileExists && filedia.CheckPathExists && openbrowser)
            {
                Scene scene = FormatConvert.ConvertFormat(filedia.FileName);
                _scenariostorer.NewScene = true;
                _scenariostorer.Scenerio = scene;
                pagenav.Navigate<ScenePage>();
            }

        }

        private Visibility _openButtonVisibility = Visibility.Visible;

        public Visibility OpenButtonVisibility
        {
            get
            {
                return _openButtonVisibility;
            }
            set
            {
                _openButtonVisibility = value;
                OnPropertyChanged(nameof(OpenButtonVisibility));
            }
        }

        private Visibility _closeButtonVisibility = Visibility.Collapsed;

        public Visibility CloseButtonVisibility
        {
            get
            {
                return _closeButtonVisibility;
            }
            set
            {
                _closeButtonVisibility = value;
                OnPropertyChanged(nameof(CloseButtonVisibility));
            }
        }

        public ICommand ButtonOpenCommand
        {
            get
            {
                return new CommandHandler(() => ButtonOpenClick());
            }
        }

        public void ButtonOpenClick()
        {
            CloseButtonVisibility = Visibility.Visible;
            OpenButtonVisibility = Visibility.Collapsed;
        }

        public ICommand ButtonCloseCommand
        {
            get
            {
                return new CommandHandler(() => ButtonCloseClick());
            }
        }

        public void ButtonCloseClick()
        {
            OpenButtonVisibility = Visibility.Visible;
            CloseButtonVisibility = Visibility.Collapsed;
        }
    }
}
