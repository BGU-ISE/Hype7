using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Manager
{
    abstract class Runner
    {
        protected string executable_path;
        protected string args;
        protected string file_name;

        public abstract bool run();

        protected void find_exe_path()
        {
            foreach (var file in Directory.GetFiles(Constants.excutables_folder))
            {
                if (Path.GetFileNameWithoutExtension(file).Equals(file_name))
                {
                    executable_path = file;
                    break;
                }
            }
        }

    }
}
