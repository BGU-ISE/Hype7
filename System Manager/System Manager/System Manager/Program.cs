using System;
using System.Diagnostics;
using System.IO;

namespace System_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arggg = { "../../../../../../Data/input", "../../../../../../Data/output" };
            string exe_folder = "../../../executables";
            foreach (var item in Directory.GetFiles(exe_folder))
            {
                Process proc = new Process();
                proc.StartInfo.FileName = item;
                Console.WriteLine(item);
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Arguments = Constants.input_output_folders_args;
                proc.Start();
                proc.WaitForExit();
                Console.WriteLine("doneee");
            }
        }
    }
}
