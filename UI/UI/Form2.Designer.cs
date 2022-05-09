
namespace UI
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MetricName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Metric = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Slope = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MetricName,
            this.Metric,
            this.Slope});
            this.dataGridView1.Location = new System.Drawing.Point(121, 108);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(647, 234);
            this.dataGridView1.TabIndex = 0;
            // 
            // MetricName
            // 
            this.MetricName.HeaderText = "Metric Name";
            this.MetricName.Name = "MetricName";
            // 
            // Metric
            // 
            this.Metric.HeaderText = "Metric";
            this.Metric.Name = "Metric";
            // 
            // Slope
            // 
            this.Slope.HeaderText = "Slope";
            this.Slope.Name = "Slope";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MetricName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Metric;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slope;
    }
}