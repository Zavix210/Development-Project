using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SceneBuilderWpf
{
    public interface IPageNavigationService
    {
        Frame CurrentPage { get; }

        void Navigate<T>(object parm = null);

        void Navigate(Type page, object parameter);

        void GoBack();

        void GoForward();
    }
}
