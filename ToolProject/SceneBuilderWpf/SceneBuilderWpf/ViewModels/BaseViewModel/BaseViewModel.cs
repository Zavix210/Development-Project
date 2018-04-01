using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SceneBuilderWpf.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel(IPageNavigationService page)
        {
            pagenav = page;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IPageNavigationService pagenav;
    }
}
