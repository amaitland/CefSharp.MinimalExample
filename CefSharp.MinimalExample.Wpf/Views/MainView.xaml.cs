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

            var resourceHandlerFactory = (DefaultResourceHandlerFactory)BrowserControl.ResourceHandlerFactory;

            resourceHandlerFactory.RegisterHandler("http://cefsharp.test/string.html", ResourceHandler.FromString(@"<!DOCTYPE html>
                                    <html>
                                    <head>

                                    </head>
                                    <body>
                                        <div>Welcome to CefSharp!</div>
                                        <script type='text/javascript'>
    
                                            function testFunction()
                                            {
                                                alert('test');
                                            }
                                        </script>
                                    </body>
                                    </html>", ".html"));

            BrowserControl.FrameLoadEnd += OnFrameLoadEnd;
        }

        private static void OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            //Only execute script for the main frame
            if (e.Frame.IsMain)
            {
                //The extension methods actually invoke scripts on the main frame (I strongly recommend executing code in an anonymous closure).
                e.Frame.EvaluateScriptAsync("(function(){ testFunction(); })();");
            }
        }
    }
}
