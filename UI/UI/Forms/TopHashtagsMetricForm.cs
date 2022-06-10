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
        public List<HashtagModel> Hashtags { get; set; }
        public class HashtagModel
        {
            private string ID { get; set; }
            private string Name { get; set; }
            public float Slope { get; set; }
            public string URL { get; set; }
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
                this.ID = "";
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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
