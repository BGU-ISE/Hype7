using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper_Manager
{
    public class hashtag
    {
        List<string> names;
        public hashtag(string hashtags)
        {
            names = new List<string>();
            foreach (var item in hashtags.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                if (item.Length < 6)
                    break;
                string blyat = item.Substring(0, 5);
                if (item.Substring(0, 5).Equals("name:"))
                    names.Add(item.Substring(5));

            }
        }

        
        public override string ToString()
        {
            string ans = "[";
            bool init = true;
            foreach (var name in names)
            {
                if (!init)
                    ans += "; " + name;
                else
                {
                    ans += name;
                    init = false;
                }
            }
            ans += "]";
            return ans;
        }
    }
}