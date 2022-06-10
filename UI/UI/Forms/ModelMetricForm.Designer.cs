﻿
namespace UI.Forms
{
    partial class ModelMetricForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LoadGraphBtn = new System.Windows.Forms.Button();
            this.SubmitBtn = new System.Windows.Forms.Button();
            this.socialMediaLbl = new System.Windows.Forms.Label();
            this.socialMediaCombBox = new System.Windows.Forms.ComboBox();
            this.limitLabel = new System.Windows.Forms.Label();
            this.limitTxtBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(330, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(432, 371);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 158);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1121, 377);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LoadGraphBtn);
            this.panel2.Controls.Add(this.SubmitBtn);
            this.panel2.Controls.Add(this.socialMediaLbl);
            this.panel2.Controls.Add(this.socialMediaCombBox);
            this.panel2.Controls.Add(this.limitLabel);
            this.panel2.Controls.Add(this.limitTxtBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1121, 152);
            this.panel2.TabIndex = 2;
            // 
            // LoadGraphBtn
            // 
            this.LoadGraphBtn.Location = new System.Drawing.Point(585, 101);
            this.LoadGraphBtn.Name = "LoadGraphBtn";
            this.LoadGraphBtn.Size = new System.Drawing.Size(112, 34);
            this.LoadGraphBtn.TabIndex = 5;
            this.LoadGraphBtn.Text = "Load Graph";
            this.LoadGraphBtn.UseVisualStyleBackColor = true;
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.Location = new System.Drawing.Point(388, 101);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(112, 34);
            this.SubmitBtn.TabIndex = 4;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.UseVisualStyleBackColor = true;
            this.SubmitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // socialMediaLbl
            // 
            this.socialMediaLbl.AutoSize = true;
            this.socialMediaLbl.Location = new System.Drawing.Point(174, 49);
            this.socialMediaLbl.Name = "socialMediaLbl";
            this.socialMediaLbl.Size = new System.Drawing.Size(112, 25);
            this.socialMediaLbl.TabIndex = 3;
            this.socialMediaLbl.Text = "Social Media";
            // 
            // socialMediaCombBox
            // 
            this.socialMediaCombBox.FormattingEnabled = true;
            this.socialMediaCombBox.Location = new System.Drawing.Point(293, 46);
            this.socialMediaCombBox.Name = "socialMediaCombBox";
            this.socialMediaCombBox.Size = new System.Drawing.Size(182, 33);
            this.socialMediaCombBox.TabIndex = 2;
            // 
            // limitLabel
            // 
            this.limitLabel.AutoSize = true;
            this.limitLabel.Location = new System.Drawing.Point(601, 49);
            this.limitLabel.Name = "limitLabel";
            this.limitLabel.Size = new System.Drawing.Size(161, 25);
            this.limitLabel.TabIndex = 1;
            this.limitLabel.Text = "Number Of Videos";
            // 
            // limitTxtBox
            // 
            this.limitTxtBox.Location = new System.Drawing.Point(768, 46);
            this.limitTxtBox.Name = "limitTxtBox";
            this.limitTxtBox.Size = new System.Drawing.Size(150, 31);
            this.limitTxtBox.TabIndex = 0;
            // 
            // ModelMetricForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 535);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ModelMetricForm";
            this.Text = "ModelMetric";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label limitLabel;
        private System.Windows.Forms.TextBox limitTxtBox;
        private System.Windows.Forms.Label socialMediaLbl;
        private System.Windows.Forms.ComboBox socialMediaCombBox;
        private System.Windows.Forms.Button SubmitBtn;
        private System.Windows.Forms.Button LoadGraphBtn;
    }
}