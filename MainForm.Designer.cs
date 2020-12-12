namespace accjs_win
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.webView2Control = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.SuspendLayout();
            // 
            // webView2Control
            // 
            this.webView2Control.CreationProperties = null;
            this.webView2Control.Location = new System.Drawing.Point(10, 10);
            this.webView2Control.Margin = new System.Windows.Forms.Padding(0);
            this.webView2Control.Name = "webView2Control";
            this.webView2Control.Size = new System.Drawing.Size(1900, 1060);
            this.webView2Control.Source = new System.Uri("http://www.zd.hk/", System.UriKind.Absolute);
            this.webView2Control.TabIndex = 0;
            this.webView2Control.Text = "webview";
            this.webView2Control.ZoomFactor = 1D;
            this.webView2Control.CoreWebView2Ready += new System.EventHandler<System.EventArgs>(this.webView2Control_CoreWebView2Ready);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1894, 1009);
            this.Controls.Add(this.webView2Control);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "accjs-win";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView2Control;
    }
}

