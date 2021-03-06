using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Manager
{
    public class Model_Manager_Runner :Runner
    {
        public Model_Manager_Runner(string directory)
        { 
            this.directory = directory;
            args = this.fullDir+"\\DataBase.db";
            file_name = "ModelManager";
            find_exe_path();
        }

        public override bool run()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = executable_path;
                proc.StartInfo.UseShellExecute = true;
                //proc.StartInfo.Arguments = args;
                proc.StartInfo.ArgumentList.Add(args);
                proc.Start();
                proc.WaitForExit();
                Console.WriteLine("finished model manager run");
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
