using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        public App()
        {
            CefSharpSettings.WcfEnabled = false;

            Cef.Initialize(new CefSettings());
        }
    }
}
