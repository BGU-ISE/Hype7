using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper_Manager
{
    public class RecordsFile
    {
        public string inputPath { get; set; }
        public string outputPath { get; set; }
        string settingsPath { get; set; }
        public List<Record> records = new List<Record>();
        string origin { get; set; }
        Dictionary<int, int> swap_order { get; set; }
       public  Dictionary<string, string> settings_dict { get; set; }
        public List<string> output_names { get; set; }
        public List<string> values_names { get; set; }
        public string[] output_names_arr { get { return output_names.ToArray(); } set { } }

        public RecordsFile(string settings_path, string input_path, string output_path)
        {
            this.settingsPath = settings_path;
            this.inputPath = input_path;
            this.outputPath = output_path;
            this.values_names = new List<string>();
            load_settings();

        }

        
        private void load_settings()
        {
            this.settings_dict = new Dictionary<string, string>();
            List<string> values_order = new List<string>();

            using (var reader = new StreamReader(this.settingsPath))
            {

                if (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(";;");
                    var pairs = new List<string[]>();
                    foreach (string pair in values)
                    {
                        settings_dict.Add(pair.Split(";")[0], pair.Split(";")[1]);
                        values_order.Add(pair.Split(";")[1]);
                    }

                }
                if (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    this.origin = line;
                }
            }
            this.output_names = values_order;
        }


        private void eval_values_name(List<string> lines)
        {
            string line = lines[0];
            bool is_str = false;
            bool is_list = false;
            string current_cell = "";
            foreach (char c in line)
            {
                switch (c)
                {
                    case (char)34:
                        is_str = !is_str;

                        break;
                    case '[':
                        is_list = true;
                        break;
                    case ']':
                        if (is_list)
                            is_list = false;
                        else
                            current_cell += c;
                        break;
                    case ',':
                        if (is_list && !is_str)
                            current_cell += '_';
                        else
                            if (is_list || is_str)
                            current_cell += c;
                        else
                        {
                            this.values_names.Add(current_cell);
                            current_cell = "";
                        }
                        break;
                    default:
                        current_cell += c;
                        break;
                }
            }
            this.values_names.Add(current_cell);
        }

        private void eval_swap_order()
        {
            this.swap_order = new Dictionary<int, int>();
            for (int i = 0; i < this.values_names.Count; i++)
            {

                string val_name = this.values_names[i];
                if (this.settings_dict.ContainsKey(val_name))
                {
                    int pos = this.output_names.IndexOf(settings_dict[val_name]);
                    if (pos != -1)
                    {
                        swap_order.Add(i, pos);
                    }
                }
            }
        }


        private List<string> read_file()
        {
            List<string> lines = new List<string>();
            string file_content = "";
<<<<<<< HEAD
            bool is_quote = false;
=======
            bool isQoute = false;
>>>>>>> yotubescraper
            using (var reader = new StreamReader(this.inputPath))
            {

                while (!reader.EndOfStream)
                {
                    char c = (char)reader.Read();
   
                    if(c == ((char)34))
                        isQoute = !isQoute ;

                    if (c == '@')
                        continue;
<<<<<<< HEAD
                    is_quote = is_quote ^ c == ((char)34);
                    if (c == ((char)13) && !reader.EndOfStream && !is_quote)
=======
                    if (c == ((char)13) && !reader.EndOfStream && !isQoute)
>>>>>>> yotubescraper
                    {
                        char c2 = (char)reader.Read();
                        if (c2 == ((char)10))
                        {
                            lines.Add(file_content);
                            file_content = "";
                        }
                        else
                            file_content += c + c2;
                    }
                    else
                        file_content += c;
                }
                if(file_content != "")
                    lines.Add(file_content);
            }
            return lines;
        }
        public void loadFile()
        {
            List<string> lines=  read_file();
            Console.WriteLine("lines: " + lines.Count);
            eval_values_name(lines);
            eval_swap_order();
            lines.RemoveAt(0); //remove the values name
         
            foreach (var line in lines)
            {
                Record new_rec = new Record(line, values_names, origin);
                new_rec.setSwapOrder(swap_order, output_names_arr);
                new_rec.reformat_hashtags();
                this.records.Add(new_rec);
            }

        }


        public void saveRecords()
        {
            using (var w = new StreamWriter(this.outputPath))
            {
                string values_names_str = "";
                foreach (var item in this.output_names)
                {
                    if (values_names_str == "")
                        values_names_str += item;
                    else
                        values_names_str += ", " + item;
                }
                w.WriteLine(values_names_str);
                w.Flush();
                foreach (var record in this.records)
                {
                    w.WriteLine(record.ToString());
                    w.Flush();
                }
            }

        }

    }
}
