using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;

namespace tims_calculation
{
    class Programm
    {
        public static Random rnd = new Random();
        public static double GetRandom(double lowerBorder,double upperBorder)
        {
            
            return Convert.ToDouble((rnd.NextDouble() * (upperBorder - lowerBorder) + lowerBorder).ToString("F1"));
        }
        static void Main(string[] args)
        {
            
            Dictionary<double, double> points = new Dictionary<double, double> ();
            //points.Add(1, 2);
            //points.Add(3, 5);
            //points.Add(4, 7);
            //points.Add(8, 2);
            //points.Add(9, 4);
            //for (int i = 0; i < 10; i++)
            //{
            //    var randNum = GetRandom();
            //    if(points.ContainsKey(randNum))
            //    {
            //        --i;
            //        continue;
            //    }
            //    points.Add(randNum, GetRandom());
            //}

            DiscreteVariable v = new DiscreteVariable();


            List<double> row = new List<double> { 2, 2, 2, 3, 3, 3, 4,4,5,5,6,6,7,7,7,7,7,10,10,12,12,13,13,14,14,14,15,15,15,20};
            foreach(var num in row.Distinct())
            {
                Console.WriteLine(Statistics.EmpiricalCDF(row, num));
            }
            v.GenerateSample(0, 2, 50);
            //v.ShowVariationRange();
            v.FormFrequencyTable();
            v.EmpiricalCDF();
            //Console.WriteLine(NumericalCharacteristics.VariationOfRow(row));
            
            //string x_axis = "";
            //string y_axis ="";

            //foreach(KeyValuePair<double,double> keyValue in points)
            //{
            //    x_axis +=  keyValue.Key.ToString("F") + "|";
            //    y_axis += keyValue.Value.ToString("F") + "|" ;
            //}
            //RunPython(x_axis, y_axis);
            Console.ReadLine();
        }
        static void RunPython(string x_axis, string y_axis)
        {

            string progToRun = @"C:\Users\ostap\PycharmProjects\tims\main.py";
            char[] spliter = { '\r' };
           
            Process proc = new Process();
            proc.StartInfo.FileName = "python.exe";
            proc.StartInfo.Arguments = string.Format("{0} {1} {2}", progToRun, x_axis, y_axis);

            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;

           
            proc.Start();

            StreamReader sReader = proc.StandardOutput;
            string[] output = sReader.ReadToEnd().Split(spliter);

            foreach (string s in output)
                Console.WriteLine(s);

            proc.WaitForExit();

        }
    }
}