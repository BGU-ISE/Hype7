using System;
using System.Collections.Generic;
using System.Text;

namespace Hype7
{
    public class Hashtag
    {
        private string value;
        private int count;

        public Hashtag(string value, int count)
        {
            this.value = value;
            this.count = count;
        }
        public Hashtag(string value)
        {
            this.value = value;
            this.count = 1;
        }
        public Hashtag(Hashtag other)
        {
            this.value = other.GetValue();
            this.count = other.GetCount();
        }
        public int GetCount() { return count; }
        public string GetValue() { return value; }
        public void Add() { this.count++; }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return this.value.Equals(((Hashtag)obj).GetValue());
            }
        }
        public string Print()
        {
            return "hashtag: " + value + " count: " + count +"\n";
        }
    }
}
