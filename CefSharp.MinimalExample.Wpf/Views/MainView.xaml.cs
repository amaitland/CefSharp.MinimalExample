using System.Windows.Controls;
using CefSharp.MinimalExample.Wpf.ViewModels;

namespace CefSharp.MinimalExample.Wpf.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            const string html = "<html><body><h2>Hi There!</h2></body></html>";
            Browser.LoadHtml(html, "http://test/html");
        }
    }
}
