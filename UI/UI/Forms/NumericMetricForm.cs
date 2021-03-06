using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace UI
{
    public partial class NumericMetricForm : Form
    {
        public DataGridView dataGridView { get; set; }
        public List<MetricData> metricData { get; set; }
        public class MetricData
        {
            private string Name { get; set; }
            public float Slope { get; set; }
            public Uri ID { get; set; }
            private string URL { get; set; }
            public float AverageScore { get; set; }
            public float Score1 { get; set; }
            public float Score2 { get; set; }
            public float Score3 { get; set; }
            public float Score4 { get; set; }
            public float Score5 { get; set; }
            public float Score6 { get; set; }
            public float Score7 { get; set; }

            public MetricData(string name, string id, float slope, float averageScore, float score1, float score2, float score3, float score4, float score5, float score6, float score7)
            {
                this.Name = name;
                this.ID = new Uri("https://www.youtube.com/watch?v=" + id);
                this.Slope = slope;
                this.AverageScore = averageScore;
                this.Score1 = score1;
                this.Score2 = score2;
                this.Score3 = score3;
                this.Score4 = score4;
                this.Score5 = score5;
                this.Score6 = score6;
                this.Score7 = score7;
            }

            public void SetURL(string url)
            {
                this.URL = url;
            }
        }
        public NumericMetricForm()
        {
            
            InitializeComponent();
            this.metricData = DAL.GetMetrics("view_count","averageScore", 10);
            dataGridView1.DataSource = this.metricData;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if(dataGridView1.Columns[e.ColumnIndex].Name == "ID")
                    {
                        var video_url = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value;
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

        private void Form2_Load(object sender, EventArgs e)
        {
            List<string> socialMedia = new List<string> { "Youtube", "Tiktok" };
            comboBox1.DataSource = socialMedia;
            comboBox1.SelectedItem = null;
            comboBox1.SelectedText = "--Select--";

            MetricComboBox.DataSource = DAL.GetMetricsFormula();
            MetricComboBox.SelectedItem = null;
            MetricComboBox.SelectedText = "--Select--";

            List<string> orderBy = new List<string> { "slope", "averageScore" };
            OrderByComboBox.DataSource = orderBy;
            OrderByComboBox.SelectedItem = null;
            OrderByComboBox.SelectedText = "--Select--";
        }

        private void OrderByComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LimitTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(LimitTextBox.Text, out int res))
            {
                string socialMedia = comboBox1.Text;
                DAL.ChangeDBName(socialMedia.ToLower()); // .Substring(0, socialMedia.Length - 5)
                this.metricData = DAL.GetMetrics(MetricComboBox.Text, OrderByComboBox.Text, res);
                dataGridView1.DataSource = this.metricData;
                InputErrorLbl.Text = "";
            }
            else
            {
                InputErrorLbl.Text = "Error: Illegal insertion in limit";
            }
        }

        private void LoadGraph_Click_1(object sender, EventArgs e)
        {
            Form form = new Forms.NumericChartForm(dataGridView, this.metricData);
            form.Show();
        }

        private void MetricComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBox1.Text.Equals(""))
            {
                DAL.ChangeDBName(comboBox1.Text.ToLower());
                MetricComboBox.DataSource = DAL.GetMetricsFormula();
            }
        }
    }
}
