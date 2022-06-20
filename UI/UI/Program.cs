using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            args = new string[1];
            args[0] = "C:\\Users\\Almogi\\Desktop\\githubtry\\Project\\Hype7\\System Manager\\System Manager\\System Manager\\Data";
            DAL.SetUpDB(args);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Hype7());
        }
    }
}
