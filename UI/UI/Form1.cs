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

        public MenuBtn()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            pnlNav.Height = btnHomepage.Height;
            pnlNav.Top = btnHomepage.Top;
            pnlNav.Left = btnHomepage.Left;
            btnHomepage.BackColor = Color.FromArgb(46, 51, 73);

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
    }
}
