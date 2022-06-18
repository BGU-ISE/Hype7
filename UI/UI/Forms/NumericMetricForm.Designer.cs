
namespace UI
{
    partial class NumericMetricForm
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
            System.Windows.Forms.Panel panel1;
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.InputErrorLbl = new System.Windows.Forms.Label();
            this.GraphButton = new System.Windows.Forms.Button();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.LimitTextBox = new System.Windows.Forms.TextBox();
            this.Limit = new System.Windows.Forms.Label();
            this.OrderByComboBox = new System.Windows.Forms.ComboBox();
            this.OrderByLabel = new System.Windows.Forms.Label();
            this.MetricLabel = new System.Windows.Forms.Label();
            this.MetricComboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1143, 515);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            panel1.Controls.Add(this.InputErrorLbl);
            panel1.Controls.Add(this.GraphButton);
            panel1.Controls.Add(this.SubmitButton);
            panel1.Controls.Add(this.LimitTextBox);
            panel1.Controls.Add(this.Limit);
            panel1.Controls.Add(this.OrderByComboBox);
            panel1.Controls.Add(this.OrderByLabel);
            panel1.Controls.Add(this.MetricLabel);
            panel1.Controls.Add(this.MetricComboBox);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1143, 235);
            panel1.TabIndex = 1;
            // 
            // InputErrorLbl
            // 
            this.InputErrorLbl.AutoSize = true;
            this.InputErrorLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.InputErrorLbl.Location = new System.Drawing.Point(431, 128);
            this.InputErrorLbl.Name = "InputErrorLbl";
            this.InputErrorLbl.Size = new System.Drawing.Size(0, 25);
            this.InputErrorLbl.TabIndex = 8;
            // 
            // GraphButton
            // 
            this.GraphButton.AutoSize = true;
            this.GraphButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.GraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GraphButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GraphButton.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.GraphButton.Location = new System.Drawing.Point(588, 167);
            this.GraphButton.Name = "GraphButton";
            this.GraphButton.Size = new System.Drawing.Size(120, 35);
            this.GraphButton.TabIndex = 7;
            this.GraphButton.Text = "Load Graph";
            this.GraphButton.UseVisualStyleBackColor = false;
            this.GraphButton.Click += new System.EventHandler(this.LoadGraph_Click_1);
            // 
            // SubmitButton
            // 
            this.SubmitButton.AutoSize = true;
            this.SubmitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.SubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SubmitButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SubmitButton.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.SubmitButton.Location = new System.Drawing.Point(343, 167);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(112, 35);
            this.SubmitButton.TabIndex = 6;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = false;
            this.SubmitButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // LimitTextBox
            // 
            this.LimitTextBox.Location = new System.Drawing.Point(962, 62);
            this.LimitTextBox.Name = "LimitTextBox";
            this.LimitTextBox.Size = new System.Drawing.Size(150, 31);
            this.LimitTextBox.TabIndex = 5;
            this.LimitTextBox.TextChanged += new System.EventHandler(this.LimitTextBox_TextChanged);
            // 
            // Limit
            // 
            this.Limit.AutoSize = true;
            this.Limit.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Limit.Location = new System.Drawing.Point(798, 62);
            this.Limit.Name = "Limit";
            this.Limit.Size = new System.Drawing.Size(158, 25);
            this.Limit.TabIndex = 4;
            this.Limit.Text = "Number of Vidoes";
            // 
            // OrderByComboBox
            // 
            this.OrderByComboBox.FormattingEnabled = true;
            this.OrderByComboBox.Location = new System.Drawing.Point(577, 59);
            this.OrderByComboBox.Name = "OrderByComboBox";
            this.OrderByComboBox.Size = new System.Drawing.Size(182, 33);
            this.OrderByComboBox.TabIndex = 3;
            this.OrderByComboBox.SelectedIndexChanged += new System.EventHandler(this.OrderByComboBox_SelectedIndexChanged);
            // 
            // OrderByLabel
            // 
            this.OrderByLabel.AutoSize = true;
            this.OrderByLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.OrderByLabel.Location = new System.Drawing.Point(489, 62);
            this.OrderByLabel.Name = "OrderByLabel";
            this.OrderByLabel.Size = new System.Drawing.Size(82, 25);
            this.OrderByLabel.TabIndex = 2;
            this.OrderByLabel.Text = "Order By";
            // 
            // MetricLabel
            // 
            this.MetricLabel.AutoSize = true;
            this.MetricLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.MetricLabel.Location = new System.Drawing.Point(29, 62);
            this.MetricLabel.Name = "MetricLabel";
            this.MetricLabel.Size = new System.Drawing.Size(126, 25);
            this.MetricLabel.TabIndex = 1;
            this.MetricLabel.Text = "Choose metric";
            // 
            // MetricComboBox
            // 
            this.MetricComboBox.FormattingEnabled = true;
            this.MetricComboBox.Location = new System.Drawing.Point(173, 59);
            this.MetricComboBox.Name = "MetricComboBox";
            this.MetricComboBox.Size = new System.Drawing.Size(266, 33);
            this.MetricComboBox.TabIndex = 0;
            this.MetricComboBox.SelectedIndexChanged += new System.EventHandler(this.MetricComboBox_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 235);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1143, 515);
            this.panel2.TabIndex = 2;
            // 
            // NumericMetricForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 750);
            this.Controls.Add(this.panel2);
            this.Controls.Add(panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NumericMetricForm";
            this.Text = "Metric";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MetricName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Metric;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slope;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label MetricLabel;
        private System.Windows.Forms.ComboBox MetricComboBox;
        private System.Windows.Forms.Label OrderByLabel;
        private System.Windows.Forms.ComboBox OrderByComboBox;
        private System.Windows.Forms.TextBox LimitTextBox;
        private System.Windows.Forms.Label Limit;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button GraphButton;
        private System.Windows.Forms.Label InputErrorLbl;
    }
}