using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Manager
{
    class Youtube_Scraper_Runner : Runner
    {


        public Youtube_Scraper_Runner(string directory = "youtube")
        {

            this.directory = directory;
            args = this.fullDir;
            file_name = "YoutubeScraper";
            find_exe_path();

        }



        public override bool run()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = executable_path;
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.ArgumentList.Add(args);
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
