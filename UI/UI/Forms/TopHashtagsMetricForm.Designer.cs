
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
            this.LoadGraphBtn = new System.Windows.Forms.Button();
            this.SubmitBtn = new System.Windows.Forms.Button();
            this.OrderByComboBox = new System.Windows.Forms.ComboBox();
            this.limitTxtBox = new System.Windows.Forms.TextBox();
            this.OrderLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LoadGraphBtn);
            this.panel1.Controls.Add(this.SubmitBtn);
            this.panel1.Controls.Add(this.OrderByComboBox);
            this.panel1.Controls.Add(this.limitTxtBox);
            this.panel1.Controls.Add(this.OrderLbl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 232);
            this.panel1.TabIndex = 0;
            // 
            // LoadGraphBtn
            // 
            this.LoadGraphBtn.Location = new System.Drawing.Point(567, 177);
            this.LoadGraphBtn.Name = "LoadGraphBtn";
            this.LoadGraphBtn.Size = new System.Drawing.Size(112, 34);
            this.LoadGraphBtn.TabIndex = 5;
            this.LoadGraphBtn.Text = "Load Graph";
            this.LoadGraphBtn.UseVisualStyleBackColor = true;
            this.LoadGraphBtn.Click += new System.EventHandler(this.LoadGraphBtn_Click);
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.Location = new System.Drawing.Point(316, 175);
            this.SubmitBtn.Margin = new System.Windows.Forms.Padding(4);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(144, 36);
            this.SubmitBtn.TabIndex = 4;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.UseVisualStyleBackColor = true;
            this.SubmitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // OrderByComboBox
            // 
            this.OrderByComboBox.FormattingEnabled = true;
            this.OrderByComboBox.Location = new System.Drawing.Point(701, 92);
            this.OrderByComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.OrderByComboBox.Name = "OrderByComboBox";
            this.OrderByComboBox.Size = new System.Drawing.Size(188, 33);
            this.OrderByComboBox.TabIndex = 3;
            // 
            // limitTxtBox
            // 
            this.limitTxtBox.Location = new System.Drawing.Point(333, 94);
            this.limitTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.limitTxtBox.Name = "limitTxtBox";
            this.limitTxtBox.Size = new System.Drawing.Size(155, 31);
            this.limitTxtBox.TabIndex = 2;
            this.limitTxtBox.TextChanged += new System.EventHandler(this.limitTxtBox_TextChanged);
            // 
            // OrderLbl
            // 
            this.OrderLbl.AutoSize = true;
            this.OrderLbl.Location = new System.Drawing.Point(597, 97);
            this.OrderLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.OrderLbl.Name = "OrderLbl";
            this.OrderLbl.Size = new System.Drawing.Size(82, 25);
            this.OrderLbl.TabIndex = 1;
            this.OrderLbl.Text = "Order By";
            this.OrderLbl.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of top hashtags";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 233);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 329);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1000, 329);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // TopHashtagsMetricForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 562);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
    }
}