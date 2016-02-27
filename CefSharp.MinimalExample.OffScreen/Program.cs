// Copyright © 2010-2016 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using CefSharp.OffScreen;

namespace CefSharp.MinimalExample.OffScreen
{
    public class Program
    {
        private const string TestUrl = "http://www.html-kit.com/tools/cookietester/";

        public static void Main(string[] args)
        {
            Console.WriteLine("This example application will load {0}, take a screenshot, and save it to your desktop.", TestUrl);
            Console.WriteLine();

            // You need to replace this with your own call to Cef.Initialize();
            Cef.Initialize(new CefSettings {CachePath = "cache1"}, false, true);

            MainAsync();

            // We have to wait for something, otherwise the process will exit too soon.
            Console.ReadKey();

            // Clean up Chromium objects.  You need to call this in your application otherwise
            // you will get a crash when closing.
            Cef.Shutdown();
        }

        private static async void MainAsync()
        {
            var browserSettings = new BrowserSettings();
            //Reduce rendering speed to one frame per second so it's easier to take screen shots
            browserSettings.WindowlessFrameRate = 1;

            using (var browser = new ChromiumWebBrowser(TestUrl, browserSettings))
            {
                await LoadPageAsync(browser);

                // Create a new cookie
                await browser.EvaluateScriptAsync("document.getElementsByClassName('zp')[0].submit();");

                //Wait for the page to reload
                await LoadPageAsync(browser);

                // Wait for the screenshot to be taken,
                // if one exists ignore it, wait for a new one to make sure we have the most up to date
                await browser.ScreenshotAsync(true).ContinueWith(DisplayBitmap);
            }
        }

        public static Task LoadPageAsync(IWebBrowser browser)
        {
            var tcs = new TaskCompletionSource<bool>();

            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                //Wait for while page to finish loading not just the first frame
                if (!args.IsLoading)
                {
                    browser.LoadingStateChanged -= handler;
                    tcs.TrySetResult(true);
                }
            };

            browser.LoadingStateChanged += handler;

            return tcs.Task;
        }

        private static void DisplayBitmap(Task<Bitmap> task)
        {
            // Make a file to save it to (e.g. C:\Users\jan\Desktop\CefSharp screenshot.png)
            var screenshotPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CefSharp screenshot" + DateTime.Now.Ticks + ".png");

            Console.WriteLine();
            Console.WriteLine("Screenshot ready. Saving to {0}", screenshotPath);

            var bitmap = task.Result;

            // Save the Bitmap to the path.
            // The image type is auto-detected via the ".png" extension.
            bitmap.Save(screenshotPath);

            // We no longer need the Bitmap.
            // Dispose it to avoid keeping the memory alive.  Especially important in 32-bit applications.
            bitmap.Dispose();

            Console.WriteLine("Screenshot saved.  Launching your default image viewer...");

            // Tell Windows to launch the saved image.
            Process.Start(screenshotPath);

            Console.WriteLine("Image viewer launched.  Press any key to exit.");
        }
    }
}
