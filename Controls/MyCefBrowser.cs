using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp;
using CefSharp.WinForms;

namespace Controls
{
    public partial class MyCefBrowser : UserControl
    {
        public ChromiumWebBrowser chromeBrowser;

        public MyCefBrowser()
        {
            InitializeComponent();
            InitializeChromium();
        }
        #region shit cef
        public void InitializeChromium()
        {


            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //MySchemeHandler mySchemeHandler = new MySchemeHandler();


            var settings = new CefSettings();
            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "app",     // Make sure any call to "app://local/blargh.html" trigger MySchemeHandler!
                //SchemeHandlerFactory = mySchemeHandler,
                IsSecure = true     //treated with the same security rules as those applied to "https" URLs
            });
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            CefSharp.Cef.Initialize(settings);



            //Perform dependency check to make sure all relevant resources are in our output directory.
            //Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            // Initialize cef with the provided settings
            // Create a browser component

            // Now instead use http://ourcodeworld.com as URL we'll use the "page" variable to load our local resource
            String page = string.Format(@"{0}\html\init.html", Application.StartupPath);

            chromeBrowser = new ChromiumWebBrowser("http://ourcodeworld.com");
            //chromeBrowser = new ChromiumWebBrowser(page);


            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;



            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromeBrowser.BrowserSettings = browserSettings;



            chromeBrowser.LoadingStateChanged += ChromeBrowser_LoadingStateChanged;
            chromeBrowser.FrameLoadEnd += ChromeBrowser_FrameLoadEnd;
            chromeBrowser.ConsoleMessage += ChromeBrowser_ConsoleMessage;
            chromeBrowser.LoadError += ChromeBrowser_LoadError;
            chromeBrowser.IsBrowserInitializedChanged += ChromeBrowser_IsBrowserInitializedChanged;
            // Add it to the form and fill it to the form window.
            //chromeBrowser.LoadHtml(page);

        }

        private void ChromeBrowser_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            //chromeBrowser.ShowDevTools();
        }

        private void ChromeBrowser_LoadError(object sender, LoadErrorEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ChromeBrowser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            //MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
            //throw new NotImplementedException();
        }

        private void ChromeBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ChromeBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
        #endregion
    }
}
