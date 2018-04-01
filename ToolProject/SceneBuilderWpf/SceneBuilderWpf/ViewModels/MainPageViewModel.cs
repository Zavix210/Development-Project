using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SceneBuilderWpf.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(IPageNavigationService pageNavigation) : base(pageNavigation)
        {
        }

        public ICommand ChangePagCommand
        {
            get
            {
                return new CommandHandler(() => this.ChangePage());
            }
        }


        private void ChangePage()
        {
            pagenav.Navigate<ScenePage>();
        }
    }
}
