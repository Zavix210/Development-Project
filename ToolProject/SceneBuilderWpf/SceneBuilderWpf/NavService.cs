using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SceneBuilderWpf
{
    class PageNavigationService : IPageNavigationService
    {
        private Frame MainFrame;

        public Frame CurrentPage => MainFrame;

        public PageNavigationService(Frame mainFrame)
        {
            this.MainFrame = mainFrame;

        }

        public void Navigate<T>(object parm = null)
        {
            var type = typeof(T);
            Navigate(type, parm);
        }

        public void Navigate(Type sourcePage, object parameter = null)
        {
            var src = DependencyContainer.Self.Resolve(sourcePage);
            MainFrame.Navigate(src, parameter);
        }

        public void GoBack()
        {
            var frame = MainFrame;
            System.Diagnostics.Debug.Assert(frame != null);
            if (frame.CanGoBack)
                frame.GoBack();
        }

        public void GoForward()
        {
            var frame = MainFrame;
            System.Diagnostics.Debug.Assert(frame != null);
            if (frame.CanGoForward)
                frame.GoForward();
        }
    }
}
