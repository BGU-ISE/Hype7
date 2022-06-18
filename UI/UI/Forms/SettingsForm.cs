using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DeleteMetricComboBox.DataSource = DAL.GetMetricsNames();
            DeleteMetricComboBox.SelectedItem = null;
            DeleteMetricComboBox.SelectedText = "--Select--";

            //TODO get tables names
            List<string> socialMedia = new List<string> { "DataBaseTiktok", "DataBaseYoutube" };
            SocialMediaComboBox.DataSource = DAL.GetMetricsNames();
            SocialMediaComboBox.SelectedItem = null;
            SocialMediaComboBox.SelectedText = "--Select--";

        }

        private void deleteMetricComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SocialMediaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
