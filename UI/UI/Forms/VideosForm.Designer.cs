namespace UI.Forms
{
    partial class VideosForm
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
            this.IdsList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // IdsList
            // 
            this.IdsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IdsList.FormattingEnabled = true;
            this.IdsList.ItemHeight = 25;
            this.IdsList.Location = new System.Drawing.Point(0, 0);
            this.IdsList.Name = "IdsList";
            this.IdsList.Size = new System.Drawing.Size(800, 450);
            this.IdsList.TabIndex = 0;
            this.IdsList.SelectedIndexChanged += new System.EventHandler(this.IdsList_SelectedIndexChanged);
            this.IdsList.DoubleClick += new System.EventHandler(this.click_ItemList);
            // 
            // VideosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.IdsList);
            this.Name = "VideosForm";
            this.Text = "VideosForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox IdsList;
    }
}