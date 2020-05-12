using CefSharp.Handler;

namespace CefSharp.MinimalExample.Wpf
{
    public class MinimalExampleResourceRequestHandler : ResourceRequestHandler
    {
        protected override ICookieAccessFilter GetCookieAccessFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            //The default is to allow send/save of all cookies
            //You only need this if you need special handling on a per cookie basis
            //Return null otherwise.
            var cookiesAccessFilter = new CookiesAccessFilter();
            return cookiesAccessFilter;
        }

        protected override CefReturnValue OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            //if (!callback.IsDisposed)
            //{
            //    using (callback)
            //    {
            //        callback.Continue(true);
            //        return CefReturnValue.ContinueAsync;
            //    }
            //}
            return base.OnBeforeResourceLoad(chromiumWebBrowser, browser, frame, request, callback);
        }

        protected override IResourceHandler GetResourceHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return null;
        }

    }

    public class CookiesAccessFilter : ICookieAccessFilter
    {
        public bool CanSendCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, Cookie cookie)
        {
            return true;
        }

        public bool CanSaveCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, Cookie cookie)
        {
            return true;
        }
    }
}