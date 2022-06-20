
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
            this.InputErrorLbl = new System.Windows.Forms.Label();
            this.GraphButton = new System.Windows.Forms.Button();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.LimitTextBox = new System.Windows.Forms.TextBox();
            this.Limit = new System.Windows.Forms.Label();
            this.OrderByComboBox = new System.Windows.Forms.ComboBox();
            this.OrderByLabel = new System.Windows.Forms.Label();
            this.MetricLabel = new System.Windows.Forms.Label();
            this.MetricComboBox = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            panel1.Controls.Add(this.label1);
            panel1.Controls.Add(this.comboBox1);
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
            panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(800, 141);
            panel1.TabIndex = 1;
            // 
            // InputErrorLbl
            // 
            this.InputErrorLbl.AutoSize = true;
            this.InputErrorLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.InputErrorLbl.Location = new System.Drawing.Point(302, 77);
            this.InputErrorLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.InputErrorLbl.Name = "InputErrorLbl";
            this.InputErrorLbl.Size = new System.Drawing.Size(0, 15);
            this.InputErrorLbl.TabIndex = 8;
            // 
            // GraphButton
            // 
            this.GraphButton.AutoSize = true;
            this.GraphButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.GraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GraphButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GraphButton.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.GraphButton.Location = new System.Drawing.Point(412, 100);
            this.GraphButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GraphButton.Name = "GraphButton";
            this.GraphButton.Size = new System.Drawing.Size(84, 25);
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
            this.SubmitButton.Location = new System.Drawing.Point(240, 100);
            this.SubmitButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(78, 25);
            this.SubmitButton.TabIndex = 6;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = false;
            this.SubmitButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // LimitTextBox
            // 
            this.LimitTextBox.Location = new System.Drawing.Point(632, 47);
            this.LimitTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LimitTextBox.Name = "LimitTextBox";
            this.LimitTextBox.Size = new System.Drawing.Size(106, 23);
            this.LimitTextBox.TabIndex = 5;
            this.LimitTextBox.TextChanged += new System.EventHandler(this.LimitTextBox_TextChanged);
            // 
            // Limit
            // 
            this.Limit.AutoSize = true;
            this.Limit.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Limit.Location = new System.Drawing.Point(632, 21);
            this.Limit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Limit.Name = "Limit";
            this.Limit.Size = new System.Drawing.Size(103, 15);
            this.Limit.TabIndex = 4;
            this.Limit.Text = "Number of Vidoes";
            // 
            // OrderByComboBox
            // 
            this.OrderByComboBox.FormattingEnabled = true;
            this.OrderByComboBox.Location = new System.Drawing.Point(465, 47);
            this.OrderByComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OrderByComboBox.Name = "OrderByComboBox";
            this.OrderByComboBox.Size = new System.Drawing.Size(129, 23);
            this.OrderByComboBox.TabIndex = 3;
            this.OrderByComboBox.SelectedIndexChanged += new System.EventHandler(this.OrderByComboBox_SelectedIndexChanged);
            // 
            // OrderByLabel
            // 
            this.OrderByLabel.AutoSize = true;
            this.OrderByLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.OrderByLabel.Location = new System.Drawing.Point(499, 21);
            this.OrderByLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.OrderByLabel.Name = "OrderByLabel";
            this.OrderByLabel.Size = new System.Drawing.Size(53, 15);
            this.OrderByLabel.TabIndex = 2;
            this.OrderByLabel.Text = "Order By";
            // 
            // MetricLabel
            // 
            this.MetricLabel.AutoSize = true;
            this.MetricLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.MetricLabel.Location = new System.Drawing.Point(284, 21);
            this.MetricLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MetricLabel.Name = "MetricLabel";
            this.MetricLabel.Size = new System.Drawing.Size(84, 15);
            this.MetricLabel.TabIndex = 1;
            this.MetricLabel.Text = "Choose metric";
            // 
            // MetricComboBox
            // 
            this.MetricComboBox.FormattingEnabled = true;
            this.MetricComboBox.Location = new System.Drawing.Point(240, 47);
            this.MetricComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MetricComboBox.Name = "MetricComboBox";
            this.MetricComboBox.Size = new System.Drawing.Size(187, 23);
            this.MetricComboBox.TabIndex = 0;
            this.MetricComboBox.SelectedIndexChanged += new System.EventHandler(this.MetricComboBox_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(800, 309);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 141);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 309);
            this.panel2.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(74, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(100, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Social Media";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // NumericMetricForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(panel1);
            this.Name = "NumericMetricForm";
            this.Text = "Metric";
            this.Load += new System.EventHandler(this.Form2_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}