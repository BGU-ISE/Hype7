using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Metric_Manager
{
    class Metric
    {
        private string name;
        private string input;
        private string[] fieldsNameForMetric;

        public Metric(string input)
        {
            this.name = "empty";
            this.input = input; //CalcMetricSum(input);
            fieldsNameForMetric = Regex.Split(input, @"[(|)|+|/|\-|*|^|\.]");
        }
        public Metric(string input, string name)
        {
            this.name = name;
            this.input = input; //CalcMetricSum(input);
            fieldsNameForMetric = Regex.Split(input, @"[(|)|+|/|\-|*|^|\.]");
        }
        public string GetMetric() { return input; }
        public string[] GetFields() { return fieldsNameForMetric; }
        public string GetName() { return name; }
    }
}
