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
    public partial class ModelMetricForm : Form
    {
        public List<ModelPrediction> ModelPredictions { get; set; }
        public DataGridView dataGridView { get; set; }

        public class ModelPrediction
        {
            public string URL { get; set; }
            public float HypeScore { get; set; }
            public float PredictedViewsForNextWeek { get; set; }
           
            public ModelPrediction( string url, float hypeScore, float predicted)
            {
                this.URL = url;
                this.HypeScore = hypeScore;
                PredictedViewsForNextWeek = predicted;
            }
        }

        public ModelMetricForm()
        {
            InitializeComponent();
            this.ModelPredictions = DAL.GetModelPredictions("YoutubeModel", "model1score" ,10);
            dataGridView1.DataSource = this.ModelPredictions;
            dataGridView1.Columns[2].HeaderCell.Value = "Predicted views for the following week";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            List<string> socialMedia = new List<string> { "Youtube", "Tiktok" };
            socialMediaCombBox.DataSource = socialMedia;
            socialMediaCombBox.SelectedItem = null;
            socialMediaCombBox.SelectedText = "--Select--";

            List<string> orderBy = new List<string> { "Model score", "Predicted views" };
            OrderByComboBox.DataSource = orderBy;
            OrderByComboBox.SelectedItem = null;
            OrderByComboBox.SelectedText = "--Select--";
        }
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            string socialMediaDB = "";
            string orderBY = "";
            if (socialMediaCombBox.Text == "Youtube") { socialMediaDB = "YoutubeModel"; }
            else { socialMediaDB = "TiktokModel"; }

            if (OrderByComboBox.Text == "Model score") { orderBY = "model1score"; }
            else { orderBY = "denormalize_score"; }
            
            if(Int32.TryParse(limitTxtBox.Text, out int res))
            {
                this.ModelPredictions = DAL.GetModelPredictions(socialMediaDB, orderBY, res);
                dataGridView1.DataSource = this.ModelPredictions;
                InputErrorLbl.Text = "";
            }
            else
            {
                InputErrorLbl.Text = "Error: Illegal insertion in limit";
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadGraphBtn_Click(object sender, EventArgs e)
        {
           
            Form form = new Forms.DataModelChartForm(dataGridView, this.ModelPredictions);
            form.Show();
        }

        private void socialMediaCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OrderByComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
