using System.Collections.Generic;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms
{
    public class ListCookieVisitor : ICookieVisitor
    {
        private readonly List<Cookie> cookies = new List<Cookie>(); 
        private readonly TaskCompletionSource<List<Cookie>> source = new TaskCompletionSource<List<Cookie>>();

        public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            cookies.Add(cookie);

            if (count == (total - 1))
            {
                source.SetResult(cookies);
            }

            return true;
        }

        public Task<List<Cookie>> Task
        {
            get { return source.Task; }
        }
    }
}
