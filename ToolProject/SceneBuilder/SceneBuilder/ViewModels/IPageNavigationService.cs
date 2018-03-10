using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilder.ViewModels
{
    public interface IPageNavigationService
    {
        Type CurrentPage { get; }

        void Navigate(Type page);

        void Navigate(Type page, object parameter);

        void GoBack();
    }

}
