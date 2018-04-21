using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Input;

namespace SceneBuilderWpf.ViewModels
{

    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IPageNavigationService pageNavigation) : base(pageNavigation)
        {

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
