using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static UI.Forms.ModelMetricForm;

namespace UI.Forms
{
    public partial class DataModelChartForm : Form
    {
        public DataGridView DataGridView { get; set; }
        public List<ModelPrediction> PredictionsDataList { get; set; }
        public DataModelChartForm(DataGridView dataGridView, List<ModelPrediction> predictionsDataList)
        {
            InitializeComponent();
            DataGridView = dataGridView;
            this.PredictionsDataList = predictionsDataList;
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void Chart_Load(object sender, EventArgs e)
        {
            List<double> values = new List<double>();
            List<string> labels = new List<string>();
            foreach (ModelPrediction data in PredictionsDataList)
            {
                values.Add(data.HypeScore);
                labels.Add(data.URL);
            }
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Videos",
                Labels = labels
            });
            
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Prediction",
                LabelFormatter = v => v.ToString()
            });
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            
            values.Sort();
            series.Add(new LineSeries() { Title = "hype score", Values = new ChartValues<double>(values) });
            //cartesianChart1.Series = series;
            cartesianChart1.Series = series;

        }
    }
}
