namespace CefSharp.MinimalExample.Wpf
{
    public class LifeSpanHandler : ILifeSpanHandler
    {
        public bool OnBeforePopup(IWebBrowser browser, string sourceUrl, string targetUrl, ref int x, ref int y, ref int width, ref int height)
        {
            return false;
        }

        public void OnBeforeClose(IWebBrowser browser)
        {
            
        }
    }
}
