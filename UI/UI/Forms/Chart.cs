using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UI.Models;

namespace UI.Forms
{
    public partial class Chart : Form
    {
        public DataGridView dataGridView1 { get; set; }
        public Chart(DataGridView dataGridView)
        {
            InitializeComponent();
            dataGridView1 = dataGridView;
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
                Labels = new[] { "1", "2", "3", "4", "5", "6", "7" }
            });
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;

            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
          //  var years = from o in dataGridView1.DataSource as List<VideoModel>
            //             select new { Day1 = o.};
            List<double> values = new List<double> { 1,2,3,4,5,6,7,8};
            series.Add(new LineSeries() { Title = "year", Values = new ChartValues<double>(values) });
            cartesianChart1.Series = series; 
        }
    }
}
