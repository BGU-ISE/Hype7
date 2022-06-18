using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Manager
{
   public class TikTok_Scraper_Runner : Runner
    {



        public TikTok_Scraper_Runner()
        {
            args = "";
            file_name = "tiktokScraper";
            find_exe_path();
            
        }

 
        
        public override bool run()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = executable_path;
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Arguments = args;
                proc.Start();
                proc.WaitForExit();
                Console.WriteLine("finished tiktok run");
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

    }
}
