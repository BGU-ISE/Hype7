
private static void zzz()
{
    Dictionary<string, string> settings_dict = new Dictionary<string, string>();
    List<string> values_order = new List<string>();
    string name;
    using (var reader = new StreamReader("../../../settings.txt"))
    {




        if (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(";;");
            var pairs = new List<string[]>();
            foreach (string pair in values)
            {
                settings_dict.Add(pair.Split(";")[0], pair.Split(";")[1]);
                values_order.Add(pair.Split(";")[0]);
            }

        }
        if (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            name = line;
        }
    }




    string file_content = "";
    List<string> lines = new List<string>();
    using (var reader = new StreamReader("../../../test.csv"))
    {

        while (!reader.EndOfStream)
        {
            char c = (char)reader.Read();
            if (c == ((char)13) && !reader.EndOfStream)
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
    }







    List<List<string>> boxes = new List<List<string>>();

    foreach (string line in lines)
    {
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
        boxes.Add(box);
    }

    List<string> values_names = boxes[0];
    boxes.RemoveAt(0);
    Dictionary<int, int> swap_order = new Dictionary<int, int>();
    for (int i = 0; i < values_names.Count; i++)
    {

        string val_name = values_names[i];
        int pos = values_order.IndexOf(val_name);
        if (pos != -1)
        {
            swap_order.Add(i, pos);
        }
    }
    List<List<string>> adjusted_boxes = new List<List<string>>();
    adjusted_boxes.Add(values_order);

    foreach (var line in boxes)
    {

        string[] line_arr = new string[swap_order.Count];
        foreach (int original_loc in swap_order.Keys)
        {
            int new_loc = swap_order[original_loc];
            line_arr[new_loc] = line[original_loc];
            if (values_order[new_loc] == "hashtags")
            {
                line_arr[new_loc] = new hashtag(line_arr[new_loc]).ToString();
            }

        }

        adjusted_boxes.Add(new List<string>(line_arr));
    }


    using (var w = new StreamWriter("../../../test2.csv"))
    {
        foreach (var line_boxes in adjusted_boxes)
        {
            string line = "";
            foreach (var item in line_boxes)
            {
                line += item + ",";

            }
            w.WriteLine(line);
            w.Flush();
        }
    }


    Console.WriteLine("done");
}
    }
}




//var tempp = line.Split("\"");
//var temp = line.Split(new string[] { "[", "]" }, StringSplitOptions.None);
//var temp2pointo = new List<string>();
//int counter = 0;
//foreach (var item in temp)
//{
//    if (counter == 0)
//    {
//        temp2pointo.AddRange(item.Split(','));
//        if (temp2pointo[temp2pointo.Count - 1] == "")
//            temp2pointo.RemoveAt(temp2pointo.Count - 1);
//    }
//    else
//    {
//        if (counter % 2 == 1)
//            temp2pointo.Add(item);

//        else
//            temp2pointo.AddRange(item.Split(',', StringSplitOptions.RemoveEmptyEntries));
//    }
//    counter++;
//}
//boxes.Add(temp2pointo);