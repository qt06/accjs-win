using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.Web.WebView2.Core;
namespace accjs_win
{
    public partial class MainForm : Form
    {
        private string injectJs;

        public MainForm()
        {
            Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName), EnvironmentVariableTarget.Process);
            InitializeComponent();
            HandleResize();
        }
        private void UpdateTitleWithEvent(string message)
        {
            string currentDocumentTitle = this.webView2Control?.CoreWebView2?.DocumentTitle ?? "Uninitialized";
            this.Text = currentDocumentTitle + " (" + message + ")";
        }

        #region Event Handlers
        private void WebView2Control_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            UpdateTitleWithEvent("NavigationStarting");
        }

        private void WebView2Control_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            UpdateTitleWithEvent("NavigationCompleted");
        }

        private void WebView2Control_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
        }

        private void webView2Control_CoreWebView2Ready(object sender, EventArgs e1)
        {

            this.webView2Control.CoreWebView2.SourceChanged += CoreWebView2_SourceChanged;
            this.webView2Control.CoreWebView2.HistoryChanged += CoreWebView2_HistoryChanged;
            this.webView2Control.CoreWebView2.DocumentTitleChanged += CoreWebView2_DocumentTitleChanged;
            this.webView2Control.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.Image);
            this.LoadPackage();
            UpdateTitleWithEvent("CoreWebView2InitializationCompleted succeeded");
        }

        private void WebView2Control_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateTitleWithEvent($"AcceleratorKeyUp key={e.KeyCode}");
        }

        private void WebView2Control_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateTitleWithEvent($"AcceleratorKeyDown key={e.KeyCode}");
            if (e.KeyCode == Keys.Back)
            {
                webView2Control.CoreWebView2.GoBack();
            }
        }

        private void WebView2Control_AcceleratorKeyPressed(object sender, Microsoft.Web.WebView2.Core.CoreWebView2AcceleratorKeyPressedEventArgs e)
        {

        }

        private void CoreWebView2_HistoryChanged(object sender, object e)
        {
            // No explicit check for webView2Control initialization because the events can only start
            // firing after the CoreWebView2 and its events exist for us to subscribe.

            UpdateTitleWithEvent("HistoryChanged");
        }

        private void CoreWebView2_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            UpdateTitleWithEvent("SourceChanged");
        }

        private void CoreWebView2_DocumentTitleChanged(object sender, object e)
        {
            this.Text = this.webView2Control.CoreWebView2.DocumentTitle;
            UpdateTitleWithEvent("DocumentTitleChanged");
        }
        #endregion

        #region UI event handlers

        private void Form_Resize(object sender, EventArgs e)
        {
            HandleResize();
        }

        #endregion

        private void HandleResize()
        {
            // Resize the webview
            webView2Control.Size = this.ClientSize - new System.Drawing.Size(webView2Control.Location);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
        private void LoadPackage()
        {
            string pkg = Path.Combine(Application.StartupPath, "package.json");
            if (File.Exists(pkg))
            {
                JObject json = JObject.Parse(File.ReadAllText(pkg, Encoding.UTF8));
                this.Text = json["name"].ToString();
                this.webView2Control.Source = new Uri(json["targetUrl"].ToString(), UriKind.RelativeOrAbsolute);
                if (json.ContainsKey("inject"))
                {
                    foreach (var item in (JArray)json["inject"])

                    {
                        this.injectJs += File.ReadAllText(Path.Combine(Application.StartupPath, item.ToString()), Encoding.UTF8);
                    }
                    if (!string.IsNullOrEmpty(this.injectJs))
                    {
                        this.webView2Control.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(this.injectJs);
                    }
                }
            }
        }
    }
}
