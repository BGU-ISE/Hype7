using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Hype7
{
    public class Metric
    {
        private string input;
        private string[] fieldsNameForMetric;

        public Metric(string input)
        {
            this.input = CalcMetricSum(input);
            CalcMetric();
            int i = 0;
        }
        private void CalcMetric()
        {
            fieldsNameForMetric = Regex.Split(input, @"[(|)|+|/|\-|*|^|\.]");
        }
        public double Eval(VideoInfo a)
        {
            string metric = this.input;
            for (int i = 0; i < fieldsNameForMetric.Length; i++)
            {
                if (fieldsNameForMetric[i].Length > 0 && Regex.Matches(fieldsNameForMetric[i], @"[a-zA-Z]").Count > 0)
                {
                    if (fieldsNameForMetric[i].Contains("_"))
                    {
                        var arr = fieldsNameForMetric[i].Split("_");
                        string res = "";
                        if (arr[0].Equals("playCountPerDay"))
                        {
                            res = ""+SystemManager.GetPlayCountPerDay(a.GetID(), SystemManager.GetDateByIndex(int.Parse(arr[1])));
                        }
                        else
                        {
                            res = SystemManager.GetFieldPerDay(a.GetID(), arr[0], SystemManager.GetDateByIndex(int.Parse(arr[1])));
                        }
                        metric = metric.Replace(fieldsNameForMetric[i], res);
                    }
                    else
                        metric = metric.Replace(fieldsNameForMetric[i], a.GetFieldByIndex(SystemManager.GetIndexByFieldName(fieldsNameForMetric[i])));                }
            }
            double ans = StringToFormula.Eval(metric);
            a.InsertScore(ans);
            return ans;
        }
        private string CalcMetricSum(string metricNew)
        {//"Sum_i_1_6(playCount_i*((0.5)^i))"
            while (metricNew.Contains("Sum"))
            {
                var temp = metricNew.IndexOf("Sum");
                var temp1 = metricNew.Substring(temp + 3, metricNew.Length - (temp + 3));
                var temp2 = temp1.IndexOf("(");
                var temp3 = temp1.Substring(0, temp2);
                var arr = temp3.Split("_");
                string iName = arr[1];
                int start = int.Parse(arr[2]);
                int end = int.Parse(arr[3]);
                //string start_prev_metric = metricNew.Substring(0, temp);

                var metric_remove_sum = temp1.Substring(temp2, temp1.Length - (temp2));
                string preBody = StringToFormula.GetBody(metric_remove_sum);
                string body = CalcMetricSum("("+ preBody + ")");
                metric_remove_sum = metric_remove_sum.Replace(preBody, body);
                int index_i = body.IndexOf("_" + iName);
                string[] varPerI = body.Split("_" + iName);
                string resultBody = "";
                string oldBody = body;
                List<string> tokens = StringToFormula.getTokensString(body);
                for(int i=start; i<=end; i++)
                {
                    string newBody = oldBody;
                    foreach (var token in tokens)
                    {
                        if (token.Contains("_" + iName))
                        {
                            string[] str1 = token.Split("_"+iName);
                            newBody = newBody.Replace(token, str1[0] + "_" +i);
                        }
                        if (token.Equals(iName))
                        {
                            newBody = newBody.Replace(token, i+"");
                        }
                    }
                    if (resultBody.Length == 0)
                        resultBody = "(" + newBody + ")";
                    else
                        resultBody = resultBody + "+(" + newBody + ")";
                }
                metricNew = metric_remove_sum.Substring(body.Length+2, metric_remove_sum.Length - (body.Length+2));
                metricNew = resultBody + metricNew;


            }
            return metricNew;
        }


    }
}
