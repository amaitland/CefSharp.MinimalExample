using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CefSharp.MinimalExample.WinForms
{
    public class RequestHandler : IRequestHandler
    {
        public bool GetAuthCredentials(IWebBrowser browser, bool isProxy, string host, int port, string realm, string scheme, ref string username, ref string password)
        {
            return false;
        }

        public bool OnBeforeBrowse(IWebBrowser browser, IRequest request, bool isRedirect, bool isMainFrame)
        {
            if(request.TransitionType.HasFlag(TransitionType.ForwardBack))
            {
                return true;
            }
            return false;
        }

        public bool OnBeforePluginLoad(IWebBrowser browser, string url, string policyUrl, WebPluginInfo info)
        {
            return false;
        }

        public CefReturnValue OnBeforeResourceLoad(IWebBrowser browser, IRequest request, bool isMainFrame)
        {
            return CefReturnValue.Continue;
        }

        public bool OnCertificateError(IWebBrowser browser, CefErrorCode errorCode, string requestUrl)
        {
            return false;
        }

        public void OnPluginCrashed(IWebBrowser browser, string pluginPath)
        {
            
        }

        public void OnRenderProcessTerminated(IWebBrowser browser, CefTerminationStatus status)
        {
            
        }
    }
}
