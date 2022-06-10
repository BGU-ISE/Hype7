using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI.Forms
{
    public partial class TopHashtagsMetricForm : Form
    {
        public DataGridView dataGridView { get; set; }
        public List<HashtagModel> Hashtags { get; set; }
        public class HashtagModel
        {
            public string Name { get; set; }
            public float Slope { get; set; }
            public float AverageScore { get; set; }
            public float Score1 { get; set; }
            public float Score2 { get; set; }
            public float Score3 { get; set; }
            public float Score4 { get; set; }
            public float Score5 { get; set; }
            public float Score6 { get; set; }
            public float Score7 { get; set; }

            public HashtagModel(string name,  float slope, float averageScore, float score1, float score2, float score3, float score4, float score5, float score6, float score7)
            {
                this.Name = name;
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
        }
        public TopHashtagsMetricForm()
        {
            InitializeComponent();
            this.Hashtags = DAL.GetHashtags("averageScore", 10);
            dataGridView1.DataSource = this.Hashtags;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            List<string> orderBy = new List<string> { "slope", "averageScore" };
            OrderByComboBox.DataSource = orderBy;
            OrderByComboBox.SelectedItem = null;
            OrderByComboBox.SelectedText = "--Select--";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            this.Hashtags = DAL.GetHashtags(OrderByComboBox.Text, Int32.Parse(limitTxtBox.Text));
            dataGridView1.DataSource = this.Hashtags;

        }

        private void LoadGraphBtn_Click(object sender, EventArgs e)
        {
            Form form = new Forms.HashtagsChartForm(dataGridView, this.Hashtags);
            form.Show();
        }

        private void limitTxtBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
