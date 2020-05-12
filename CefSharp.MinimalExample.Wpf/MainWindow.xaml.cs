using System.Reflection;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        private bool _firstTime = true;

        public MainWindow()
        {
            InitializeComponent();

            //Propertys should be assigned immediately after InitializeComponent
            //for simplicity
            Browser.RequestHandler = new MinimalExampleHandler();
            Browser.FrameLoadEnd += OnBrowserFrameLoadEnd;
        }

        private async void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (_firstTime)
            {
                var executingAssembly = Assembly.GetExecutingAssembly();
                var resourcePath = "CefSharp.MinimalExample.Wpf" + ".web.index.html";

                using (var stream = executingAssembly.GetManifestResourceStream(resourcePath))
                using (var streamReader = new System.IO.StreamReader(stream))
                {
                    var html = streamReader.ReadToEnd();

                    //https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/Data_URIs
                    //The cross origin request from the data uri to httpbin.org is not allowing the auth request
                    //It's possible this is a bug in CEF or it's a change in the Chromium CORS policy.
                    //Either way it's easy to workaround

                    //Load our fake demo url
                    //The Browser.LoadHtml(string, bool) overload uses a data uri 
                    //This overload registers a resource which is used to load our fake url.
                    //http://cefsharp.github.io/api/79.1.x/html/M_CefSharp_WebBrowserExtensions_LoadHtml_3.htm
                    Browser.LoadHtml(html, "http://httpbin.org/cefsharpdemo", System.Text.Encoding.UTF8, oneTimeUse: true);
                }

                _firstTime = false;
            }
            else
            {
                if (e.Frame.IsMain)
                {
                    var result = await e.Frame.EvaluateScriptAsync($"httpGet('http://httpbin.org/basic-auth/undefined/undefined?accept=json')");
                }
            }
        }

    }
}
