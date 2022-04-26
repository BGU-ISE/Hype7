using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper_Manager
{
    public class Record
    {
        public string origin;
        public string[] values;
        public string[] values_names;
        string[] values_original;
        string[] values_names_original;
        Dictionary<int, int> swap_order;

        public Record(IEnumerable<string> values, IEnumerable<string> values_names, string origin)
        {
            this.values = values.ToArray();
            this.values_names = values_names.ToArray();
            this.origin = origin;
        }

        public Record(string line, IEnumerable<string> values_names, string origin)
        {
            //this.values = values.Split(',').ToArray();
            this.values_names = values_names.ToArray();
            this.origin = origin;
            List<string> box = new List<string>();
            bool is_str = false;
            bool is_list = false;
            string current_cell = "";
            foreach (char c in line)
            {
                switch (c)
                {
                    case (char)34:
                        is_str = !is_str;
                        current_cell += c;

                        break;
                    case '[':
                        if(is_str)
                            current_cell += c;
                        else
                            is_list = true;
                        break;
                    case ']':
                        if (is_str)
                            current_cell += c;
                        else
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
                            box.Add(current_cell);
                            current_cell = "";
                        }
                        break;
                    default:
                        current_cell += c;
                        break;
                }
            }
            box.Add(current_cell);
            this.values = box.ToArray();
            return;
         //   Record i;
           // if (box.Count != 44)
             //   i = new Record(line, values_names, origin);
                
        }

            
        public void setSwapOrder(Dictionary<int, int> swap_order, string[] new_names)
        {
            this.swap_order = swap_order;
            this.values_original = this.values;
            this.values_names_original = this.values_names;
            this.values = new string[swap_order.Count];
            this.values_names = new_names;//new string[swap_order.Count];
            foreach (int original_loc in swap_order.Keys)
            {
                int new_loc = swap_order[original_loc];
                this.values[new_loc] = this.values_original[original_loc];
               // this.values_names[new_loc] = this.values_names_original[original_loc];
            }
        }


        public void reformat_hashtags()
        {
            if(this.origin == "tiktok")
                for (int i = 0; i < this.values_names.Length; i++)
                {
                    if(this.values_names[i].Equals("hashtags"))
                    {
                        this.values[i] = new hashtag(this.values[i]).ToString();
                    }
                }
        }




        public override string ToString()
        {
            string line = "";
            foreach (var item in this.values)
            {
                if (line == "")
                    line +=  ""+item+"";
                else
                    line += "," + item+"";

            }
            return line;
        }

    }
}
