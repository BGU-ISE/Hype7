using System;
using System.Collections.Generic;
using System.Text;

namespace Hype7
{
    public class VideoInfo
    {
        private string[] Data;
        private string id;
        private DateTime date;
        private double score;
        private int serialDate;

        public VideoInfo()
        {
            this.Data = new string[0];
            this.id = "";
        }
        public VideoInfo(int fieldNumber)
        {
            this.Data = new string[fieldNumber];
            this.id = "";
        }
        public VideoInfo(string[] data, DateTime date)
        {
            this.Data = data;
            this.id = data[0];
            this.date = date;
        }
        public VideoInfo(string[] data, string id)
        {
            this.Data = data;
            this.id = id;
        }
        public string GetFieldByIndex(int index)
        {
            return Data[index];
        }
        public List<string> GetHashtag(int index)
        {
            string hash = Data[index];
            hash = hash.Replace("[", string.Empty);
            hash = hash.Replace(" ", string.Empty);
            hash = hash.Replace("]", string.Empty);
            //hash = hash.Replace("\"", string.Empty);
            string[] arr = hash.Split("_");
            List<string> ans = new List<string>();
            for (int i=1; i<arr.Length; i++)
            {
                string[] temp = arr[i].Split(";");
                ans.Add(temp[0]);
            }
            return ans;
        }
        public string GetID() { return id; }
        public int GetSerialDate() { return serialDate; }
        public void setSerialDate(int num) { this.serialDate = num; }
        public DateTime GetDate() { return date; }
        public string[] GetData() { return Data; }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return this.id.Equals(((VideoInfo)obj).GetID());
            }
        }
        public void InsertScore(double score)
        {
            this.score = score;
        }
        public double GetScore() { return score; }
        public string Print()
        {
            return "id: " + id + "\nscore: " + score + "\n";
        }
        public string PrintAll()
        {
            return "id: " + id + "\nplayCount: " + Data[1] + "\nshareCount: " + Data[2]+"\n";
        }
    }
}
