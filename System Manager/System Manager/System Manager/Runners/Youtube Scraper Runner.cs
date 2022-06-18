using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Manager
{
   public class Youtube_Scraper_Runner : Runner
    {


        public Youtube_Scraper_Runner()
        {
            args = "";
            file_name = "runner";
            find_exe_path();

        }



        public override bool run()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = executable_path;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
                proc.WaitForExit();
                Console.WriteLine("finished youtube run");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
