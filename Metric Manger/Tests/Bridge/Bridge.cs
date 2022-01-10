using Hyp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Bridge
{
    interface Bridge
    {
        public void Setup(string pathFolder, string ignoreHashPath);
        public List<VideoInfo> ReadFromCSV(string name, DateTime date);
        public List<VideoInfo> GetResultByFieldAndTime(string fieldName, int resultCount, string fieldReturn, string time);
        public List<VideoInfo> GetResultByMetricAndTime(string metric, int resultCount, string fieldReturn, string time);
        public double GetPlayCountPerDay(string id, string time);
        public List<Hashtag> GetTopHashtag(int resultCount, string time);
        public DateTime ConvertPathToDatetime(string time);


    }
}
