using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            string input_folder = args[0];
            string output_folder = args[1];

            int i = Directory.GetFiles(input_folder).Length;
            string s = "" +i;
            string full_out = output_folder + "/out.txt";
            File.WriteAllText(full_out, s);
            Console.Beep();
            System.Threading.Thread.Sleep(10000);

        }
    }
}
