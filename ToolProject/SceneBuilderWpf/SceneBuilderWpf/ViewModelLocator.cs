using SceneBuilderWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SceneBuilderWpf
{
    public sealed class ViewModelLocator
    {

        public ViewModelLocator()
        {
            var container = DependencyContainer.Self;

            container.RegisterInstance<Frame>(App.MainFrame);

            // register view models
            container.RegisterType<PageNavigationService>();
            container.RegisterType<MainPageViewModel>();
            container.RegisterType<ScenePageViewModel>();
            container.RegisterType<DescisionPageViewModel>();
            container.RegisterType<MainViewModel>();
            container.RegisterType<MainPage>();
            container.RegisterType<ScenePage>();
           
            container.RegisterInstance<IPageNavigationService>(App.pageNavigation);
            // container.RegisterType<DescisionPageViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return DependencyContainer.Self.Resolve<MainViewModel>();
            }
        }

        public ScenePage SceneView
        {
            get
            {
                return DependencyContainer.Self.Resolve<ScenePage>();
            }
        }

        public MainPageViewModel MainPageModel
        {
            get
            {
                return DependencyContainer.Self.Resolve<MainPageViewModel>();
            }
        }

        public ScenePageViewModel ScenePageModel
        {
            get
            {
                return DependencyContainer.Self.Resolve<ScenePageViewModel>();
            }
        }

        public DescisionPageViewModel DescicionPageModel
        {
            get => DependencyContainer.Self.Resolve<DescisionPageViewModel>();
        }
    }
}
