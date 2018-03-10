using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SceneBuilder.ViewModels
{
    public class PageNavigationService: IPageNavigationService
    {
        public Type CurrentPage => ((Frame)Window.Current.Content).CurrentSourcePageType;

        public void Navigate(Type sourcePage)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage);
        }

        public void Navigate(Type sourcePage, object parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, parameter);
        }

        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;
            System.Diagnostics.Debug.Assert(frame != null);
            if (frame.CanGoBack)
                frame.GoBack();
        }
    }
}
