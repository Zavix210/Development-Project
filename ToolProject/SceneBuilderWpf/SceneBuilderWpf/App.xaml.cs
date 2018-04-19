using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SceneBuilderWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IPageNavigationService pageNavigation;
        public static Frame MainFrame; 
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
           
            MainFrame = mainWindow.MainFrame;
            pageNavigation = new PageNavigationService(mainWindow.MainFrame);

            var x = (ViewModelLocator)Application.Current.Resources["ViewModelLocator"];
            mainWindow.DataContext = x.Main;

            pageNavigation.Navigate<MainPage>();
        }

    }
}
