using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI.Models;
using static UI.NumericMetricForm;

namespace UI.Forms
{
    public partial class Chart : Form
    {
        public DataGridView DataGridView { get; set; }
        public List<MetricData> MetricDataList { get; set; }
        public Chart(DataGridView dataGridView, List<MetricData> metricDataList)
        {
            InitializeComponent();
            DataGridView = dataGridView;
            this.MetricDataList = metricDataList;
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void Chart_Load(object sender, EventArgs e)
        {
            
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Day Number",
                Labels = new [] { "1", "2", "3", "4", "5", "6", "7"}
            }) ;
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Growth",
                LabelFormatter = v => v.ToString("")
            });
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            foreach (MetricData data in MetricDataList)
            {
                List<double> values = new List<double> { data.Score1, data.Score2, data.Score3, data.Score4, data.Score5, data.Score6, data.Score7 };
                series.Add(new LineSeries() { Title = data.ID, Values = new ChartValues<double>(values) });
                cartesianChart1.Series = series;
            }
            cartesianChart1.Series = series;
            //SeriesCollection series = new SeriesCollection();
            //var years = from o in DataGridView.DataSource as List<VideoModel>
            //           select new {  };
            //List<double> values = new List<double> { 1,2,3,4,5,6,7};

        }
    }
}
