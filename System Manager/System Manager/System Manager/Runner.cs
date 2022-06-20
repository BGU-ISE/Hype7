using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Manager
{
    public abstract class Runner
    {
        protected string executable_path;
        protected string args;
        protected string file_name;
        public string directory;
        protected string fullDir { get { return (new DirectoryInfo(Constants.Data_folder+"/"+ this.directory)).FullName; } private set { } }

        public abstract bool run();

        public virtual void kill()
        {
            throw new Exception("tried killing an innocent runner");
        }

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
