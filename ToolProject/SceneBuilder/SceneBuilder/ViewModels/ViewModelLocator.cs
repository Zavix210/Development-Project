using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilder.ViewModels
{
    public sealed class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var container = DependencyContainer.Self;

            // register view models

            container.RegisterType<MainPageViewModel>();
            container.RegisterType<ScenePageViewModel>();
            container.RegisterType<DescisionPageViewModel>();


            // navigation service

            var navigationService = new PageNavigationService();

            container.RegisterType<MainPage>();
            container.RegisterType<ScenePage>();
            container.RegisterType<DescisionPage>();

            container.RegisterInstance<IPageNavigationService>(navigationService);
        }


        public MainPageViewModel MainPageModel
        {
            get => DependencyContainer.Self.Resolve<MainPageViewModel>();
        }

        public ScenePageViewModel ScenePageModel
        {
            get => DependencyContainer.Self.Resolve<ScenePageViewModel>();
        }

        public DescisionPageViewModel DescicionPageModel
        {
            get => DependencyContainer.Self.Resolve<DescisionPageViewModel>();
        }

    }
}
