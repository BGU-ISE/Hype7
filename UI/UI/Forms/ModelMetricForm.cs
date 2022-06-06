using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI.Forms
{
    public partial class ModelMetricForm : Form
    {
        public List<ModelPrediction> ModelPredictions { get; set; }
        public class ModelPrediction
        {
            private string ID { get; set; }
            public float HypeScore { get; set; }
           
            public ModelPrediction( string id, float hypeScore)
            {
                this.ID = id;
                this.HypeScore = hypeScore;
            }
        }

        public ModelMetricForm()
        {
            InitializeComponent();
            this.ModelPredictions = DAL.GetModelPredictions( "ModelHypeScore",  10);
            dataGridView1.DataSource = this.ModelPredictions;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
