using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace UI
{
    public partial class Form2 : Form
    {
        public class MetricData
        {
            public string MetricForm { get; set; }
            public string Name { get; set; }
            public float Slope { get; set; }
            public string ID { get; set; }
            public string URL { get; set; }
            public float AgerageScore { get; set; }
            public string Formula { get; set; }
            public float Score1 { get; set; }
            public float Score2 { get; set; }
            public float Score3 { get; set; }
            public float Score4 { get; set; }
            public float Score5 { get; set; }
            public float Score6 { get; set; }
            public float Score7 { get; set; }
            public MetricData(string metricForm, string name, string id, float slope, float averageScore, string formula, float score1, float score2, float score3, float score4, float score5, float score6, float score7)
            {
                this.MetricForm = metricForm;
                this.Name = name;
                this.ID = id;
                this.Slope = slope;
                this.AgerageScore = averageScore;
                this.Formula = formula;
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
        public Form2()
        {
            InitializeComponent();
            //List<MetricData> lst = new List<MetricData>();
            //lst.Add(new MetricData("m1", "test", "1234586", 12, 14, "12x-2", 1, 2, 3, 4, 5, 6, 7));
            //var t = DAL.GetMetricsNames();
            dataGridView1.DataSource = DAL.GetMetrics("playCount", "slope", 10);
        }
        public void GetData()
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
