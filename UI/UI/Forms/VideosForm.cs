using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace UI.Forms
{
    public partial class VideosForm : Form
    {
        public HashSet<string> Videos { get; set; }
        public VideosForm(HashSet<string> videos)
        {
            InitializeComponent();
            Videos = videos;
            foreach(var v in videos)
            {
                
                IdsList.Items.Add(new Uri(v));
            }
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        private void click_ItemList(object sender, EventArgs e)
        {
            var video_url= IdsList.SelectedItem.ToString();
            OpenUrl(video_url);
        }

        private void IdsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
