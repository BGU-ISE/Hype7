using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Manager
{
    class GUI_Runner : Runner
    {
        private List<string> arg_list;
        private Process process;
        public GUI_Runner()
        {
            file_name = "GUI";
            find_exe_path();
            arg_list = new List<string>();
        }


        public void add_db(string directory_path)
        {
            arg_list.Add(directory_path + "/DataBase.db");
        }

        public void kill()
        {
            this.process.Kill();
        }

        public override bool run()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = executable_path;
                proc.StartInfo.UseShellExecute = true;
                //proc.StartInfo.Arguments = args;
                foreach (string path in arg_list)
                {

                    proc.StartInfo.ArgumentList.Add(path);
                }
                this.process = proc;
                proc.Start();
                Console.WriteLine("launched gui");
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
