﻿namespace UI.Forms
{
    partial class SettingsForm
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
            this.AddMetricBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FormulaTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DeleteMetricComboBox = new System.Windows.Forms.ComboBox();
            this.RemoveMetricBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MetricNameTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SocialMediaBtn = new System.Windows.Forms.Button();
            this.SocialMediaComboBox = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddMetricBtn
            // 
            this.AddMetricBtn.AutoSize = true;
            this.AddMetricBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.AddMetricBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddMetricBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddMetricBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.AddMetricBtn.Location = new System.Drawing.Point(946, 79);
            this.AddMetricBtn.Name = "AddMetricBtn";
            this.AddMetricBtn.Size = new System.Drawing.Size(112, 35);
            this.AddMetricBtn.TabIndex = 0;
            this.AddMetricBtn.Text = "Add";
            this.AddMetricBtn.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label1.Location = new System.Drawing.Point(29, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Add Metric";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FormulaTxtBox
            // 
            this.FormulaTxtBox.Location = new System.Drawing.Point(536, 79);
            this.FormulaTxtBox.Name = "FormulaTxtBox";
            this.FormulaTxtBox.Size = new System.Drawing.Size(370, 31);
            this.FormulaTxtBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(29, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Delete Metric";
            // 
            // DeleteMetricComboBox
            // 
            this.DeleteMetricComboBox.FormattingEnabled = true;
            this.DeleteMetricComboBox.Location = new System.Drawing.Point(280, 228);
            this.DeleteMetricComboBox.Name = "DeleteMetricComboBox";
            this.DeleteMetricComboBox.Size = new System.Drawing.Size(582, 33);
            this.DeleteMetricComboBox.TabIndex = 4;
            this.DeleteMetricComboBox.SelectedIndexChanged += new System.EventHandler(this.deleteMetricComboBox_SelectedIndexChanged);
            // 
            // RemoveMetricBtn
            // 
            this.RemoveMetricBtn.AutoSize = true;
            this.RemoveMetricBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.RemoveMetricBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveMetricBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RemoveMetricBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.RemoveMetricBtn.Location = new System.Drawing.Point(946, 229);
            this.RemoveMetricBtn.Name = "RemoveMetricBtn";
            this.RemoveMetricBtn.Size = new System.Drawing.Size(112, 35);
            this.RemoveMetricBtn.TabIndex = 5;
            this.RemoveMetricBtn.Text = "Delete";
            this.RemoveMetricBtn.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MetricNameTxtBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.FormulaTxtBox);
            this.panel1.Controls.Add(this.AddMetricBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1128, 150);
            this.panel1.TabIndex = 6;
            // 
            // MetricNameTxtBox
            // 
            this.MetricNameTxtBox.Location = new System.Drawing.Point(178, 82);
            this.MetricNameTxtBox.Name = "MetricNameTxtBox";
            this.MetricNameTxtBox.Size = new System.Drawing.Size(183, 31);
            this.MetricNameTxtBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label4.Location = new System.Drawing.Point(427, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Formula";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label3.Location = new System.Drawing.Point(29, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Metric Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label5.Location = new System.Drawing.Point(30, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "Choose metric to delete";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label6.Location = new System.Drawing.Point(30, 344);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "Social Media";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label7.Location = new System.Drawing.Point(30, 395);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 25);
            this.label7.TabIndex = 8;
            this.label7.Text = "Choose social media";
            // 
            // SocialMediaBtn
            // 
            this.SocialMediaBtn.AutoSize = true;
            this.SocialMediaBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.SocialMediaBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SocialMediaBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SocialMediaBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.SocialMediaBtn.Location = new System.Drawing.Point(946, 387);
            this.SocialMediaBtn.Name = "SocialMediaBtn";
            this.SocialMediaBtn.Size = new System.Drawing.Size(112, 35);
            this.SocialMediaBtn.TabIndex = 9;
            this.SocialMediaBtn.Text = "Submit";
            this.SocialMediaBtn.UseVisualStyleBackColor = false;
            // 
            // SocialMediaComboBox
            // 
            this.SocialMediaComboBox.FormattingEnabled = true;
            this.SocialMediaComboBox.Location = new System.Drawing.Point(280, 387);
            this.SocialMediaComboBox.Name = "SocialMediaComboBox";
            this.SocialMediaComboBox.Size = new System.Drawing.Size(393, 33);
            this.SocialMediaComboBox.TabIndex = 10;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(1128, 578);
            this.Controls.Add(this.SocialMediaComboBox);
            this.Controls.Add(this.SocialMediaBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RemoveMetricBtn);
            this.Controls.Add(this.DeleteMetricComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddMetricBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FormulaTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox DeleteMetricComboBox;
        private System.Windows.Forms.Button RemoveMetricBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox MetricNameTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button SocialMediaBtn;
        private System.Windows.Forms.ComboBox SocialMediaComboBox;
    }
}