using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SceneBuilder.ViewModels
{
    public class MainPageViewModel:BaseViewModel
    {
        public MainPageViewModel()
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
            navservice.Navigate(typeof(ScenePage));
        }
    }
}
