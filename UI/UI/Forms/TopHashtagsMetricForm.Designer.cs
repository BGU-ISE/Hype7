
namespace UI.Forms
{
    partial class TopHashtagsMetricForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.InputErrorLbl = new System.Windows.Forms.Label();
            this.LoadGraphBtn = new System.Windows.Forms.Button();
            this.SubmitBtn = new System.Windows.Forms.Button();
            this.OrderByComboBox = new System.Windows.Forms.ComboBox();
            this.limitTxtBox = new System.Windows.Forms.TextBox();
            this.OrderLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.InputErrorLbl);
            this.panel1.Controls.Add(this.LoadGraphBtn);
            this.panel1.Controls.Add(this.SubmitBtn);
            this.panel1.Controls.Add(this.OrderByComboBox);
            this.panel1.Controls.Add(this.limitTxtBox);
            this.panel1.Controls.Add(this.OrderLbl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 337);
            this.panel1.TabIndex = 0;
            // 
            // InputErrorLbl
            // 
            this.InputErrorLbl.AutoSize = true;
            this.InputErrorLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.InputErrorLbl.Location = new System.Drawing.Point(290, 88);
            this.InputErrorLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.InputErrorLbl.Name = "InputErrorLbl";
            this.InputErrorLbl.Size = new System.Drawing.Size(0, 15);
            this.InputErrorLbl.TabIndex = 6;
            // 
            // LoadGraphBtn
            // 
            this.LoadGraphBtn.AutoSize = true;
            this.LoadGraphBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.LoadGraphBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoadGraphBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LoadGraphBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.LoadGraphBtn.Location = new System.Drawing.Point(391, 114);
            this.LoadGraphBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LoadGraphBtn.Name = "LoadGraphBtn";
            this.LoadGraphBtn.Size = new System.Drawing.Size(84, 25);
            this.LoadGraphBtn.TabIndex = 5;
            this.LoadGraphBtn.Text = "Load Graph";
            this.LoadGraphBtn.UseVisualStyleBackColor = false;
            this.LoadGraphBtn.Click += new System.EventHandler(this.LoadGraphBtn_Click);
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.AutoSize = true;
            this.SubmitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.SubmitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SubmitBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SubmitBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.SubmitBtn.Location = new System.Drawing.Point(221, 113);
            this.SubmitBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(101, 25);
            this.SubmitBtn.TabIndex = 4;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.UseVisualStyleBackColor = false;
            this.SubmitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // OrderByComboBox
            // 
            this.OrderByComboBox.FormattingEnabled = true;
            this.OrderByComboBox.Location = new System.Drawing.Point(510, 56);
            this.OrderByComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OrderByComboBox.Name = "OrderByComboBox";
            this.OrderByComboBox.Size = new System.Drawing.Size(133, 23);
            this.OrderByComboBox.TabIndex = 3;
            // 
            // limitTxtBox
            // 
            this.limitTxtBox.Location = new System.Drawing.Point(338, 56);
            this.limitTxtBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.limitTxtBox.Name = "limitTxtBox";
            this.limitTxtBox.Size = new System.Drawing.Size(110, 23);
            this.limitTxtBox.TabIndex = 2;
            this.limitTxtBox.TextChanged += new System.EventHandler(this.limitTxtBox_TextChanged);
            // 
            // OrderLbl
            // 
            this.OrderLbl.AutoSize = true;
            this.OrderLbl.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.OrderLbl.Location = new System.Drawing.Point(544, 27);
            this.OrderLbl.Name = "OrderLbl";
            this.OrderLbl.Size = new System.Drawing.Size(53, 15);
            this.OrderLbl.TabIndex = 1;
            this.OrderLbl.Text = "Order By";
            this.OrderLbl.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(324, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of top hashtags";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 140);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(700, 197);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(700, 197);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(149, 56);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(169, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Social media";
            // 
            // TopHashtagsMetricForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 337);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TopHashtagsMetricForm";
            this.Text = "TopHashtagsMetric";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label OrderLbl;
        private System.Windows.Forms.Button SubmitBtn;
        private System.Windows.Forms.ComboBox OrderByComboBox;
        private System.Windows.Forms.TextBox limitTxtBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button LoadGraphBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label InputErrorLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}