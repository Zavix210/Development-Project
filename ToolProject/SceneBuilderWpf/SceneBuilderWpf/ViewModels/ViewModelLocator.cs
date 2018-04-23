using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using SceneBuilderWpf.ViewModels;
using System.Windows.Controls;

namespace SceneBuilderWpf.ViewModels
{
    public sealed class ViewModelLocator
    {

        public ViewModelLocator()
        {
            var container = new ContainerBuilder();

            container.RegisterInstance(App.MainFrame).As<Frame>();

            container.RegisterType<ScenarioStorer>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            container.RegisterType<PageNavigationService>();
            container.RegisterType<MainPageViewModel>();
            container.RegisterType<ScenePageViewModel>();
            container.RegisterType<MainViewModel>();
            container.RegisterType<DescisionPageViewModel>();
            container.RegisterType<MainPage>();
            container.RegisterType<ScenePage>();

            container.RegisterInstance<IPageNavigationService>(App.pageNavigation);

            var con = container.Build();
            var cs1 = new AutofacServiceLocator(con);
            ServiceLocator.SetLocatorProvider(() => cs1);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ScenePage SceneView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScenePage>();
            }
        }

        public MainPageViewModel MainPageModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }

        public ScenePageViewModel ScenePageModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScenePageViewModel>();
            }
        }

        public DescisionPageViewModel DescicionPageModel
        {
            get => ServiceLocator.Current.GetInstance<DescisionPageViewModel>();
        }

    }
}
