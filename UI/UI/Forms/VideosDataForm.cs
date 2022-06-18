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
    public partial class VideosDataForm : Form
    {

        public DataGridView dataGridView { get; set; }
        public List<VideoData> videoData { get; set; }
        public class VideoData
        { 
            public string URL { get; set; }
            public string VideoName { get; set; }
            public string AuthroName { get; set; }
            

            public VideoData(string videoName, string url, string authorName)
            {
                this.URL = url;
                this.VideoName = videoName;
                this.AuthroName = authorName;
            }
        }
        public VideosDataForm(List<VideoData> videos)
        {
            InitializeComponent();
            this.videoData = videos;
            dataGridView1.DataSource = this.videoData;
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "URL")
                    {
                        var video_url = dataGridView1.Rows[e.RowIndex].Cells["URL"].Value;
                        OpenUrl(video_url.ToString());
                    }


                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
