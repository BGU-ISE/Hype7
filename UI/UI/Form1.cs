using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace UI
{
    public partial class MenuBtn : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
               int nLeftRect,
               int nTopRect,
               int nRightRect,
               int nBottomRect,
               int nWidthEllipse,
               int nHeightEllipse

         );

        //Fields
        private Button currentButton;
        private Form activeForm;

        public MenuBtn()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            pnlNav.Height = btnHomepage.Height;
            pnlNav.Top = btnHomepage.Top;
            pnlNav.Left = btnHomepage.Left;
            btnHomepage.BackColor = Color.FromArgb(46, 51, 73);

        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    currentButton = (Button)btnSender;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
       
        private void Reset()
        {
            lblTitle.Text = "HOME";
            currentButton = null;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnMetrics_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnMetrics.Height;
            pnlNav.Top = btnMetrics.Top;
            pnlNav.Left = btnMetrics.Left;
            btnMetrics.BackColor = Color.FromArgb(46, 51, 73);
            OpenChildForm(new Form2(), sender);
        }

        private void MenuBtn_Load(object sender, EventArgs e)
        {

        }

        private void BtnHomepage_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnHomepage.Height;
            pnlNav.Top = btnHomepage.Top;
            pnlNav.Left = btnHomepage.Left;
            btnHomepage.BackColor = Color.FromArgb(46, 51, 73);
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnSettings.Height;
            pnlNav.Top = btnSettings.Top;
            pnlNav.Left = btnSettings.Left;
            btnSettings.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHomepage_Leave(object sender, EventArgs e)
        {
            btnHomepage.BackColor = Color.FromArgb(24, 30, 54);

        }

        private void btnMetrics_Leave(object sender, EventArgs e)
        {
            btnMetrics.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnSettings_Leave(object sender, EventArgs e)
        {
            btnSettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnHashtagsMetric_Leave(object sender, EventArgs e)
        {
            btnHashtagsMetric.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnNumericML_Leave(object sender, EventArgs e)
        {
            btnNumericML.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnCustomMetric_Leave(object sender, EventArgs e)
        {
            btnCustomMetric.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new Form2();
            form.Show();
        }

        private void btnHashtagsMetric_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnHashtagsMetric.Height;
            pnlNav.Top = btnHashtagsMetric.Top;
            pnlNav.Left = btnHashtagsMetric.Left;
            btnHashtagsMetric.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnNumericML_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnNumericML.Height;
            pnlNav.Top = btnNumericML.Top;
            pnlNav.Left = btnNumericML.Left;
            btnNumericML.BackColor = Color.FromArgb(46, 51, 73);

        }

        private void btnCustomMetric_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnCustomMetric.Height;
            pnlNav.Top = btnCustomMetric.Top;
            pnlNav.Left = btnCustomMetric.Left;
            btnCustomMetric.BackColor = Color.FromArgb(46, 51, 73);
        }
    }
}
