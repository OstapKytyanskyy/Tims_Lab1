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
        public static decimal GetRandomDecimal(double lowerBorder, double upperBorder)
        {

            return Convert.ToDecimal((rnd.NextDouble() * (upperBorder - lowerBorder) + lowerBorder).ToString("F1"));
        }
        static void Main(string[] args)
        {
            
            
            
            Console.WriteLine("Choose Variable, type D for Descrete and I for Interval");
            var chose = Console.ReadKey();
            if (chose.ToString().ToUpper() == "D")
            {
                UI.StartUI(EnumVariables.DescretVariable);
            }
            else
            {
                UI.StartUI(EnumVariables.IntervalVariable);
            }
//            Dictionary<double, double> points = new Dictionary<double, double> ();
//            points.Add(1, 2);
//            points.Add(3, 5);
//            points.Add(4, 7);
//            points.Add(8, 2);
//            points.Add(9, 4);
            //for (int i = 0; i < 10; i++)
            //{
            //    var randNum = GetRandom();
            //    if (points.ContainsKey(randNum))
            //    {
            //        --i;
            //        continue;
            //    }
            //    points.Add(randNum, GetRandom());
            //}

            //DiscreteVariable v = new DiscreteVariable();
            List<double> ls = new List<double> {8,7,6,9,10,9,11,8,9,10,8,9,6,9,8,10,7,10,12,7 };
            //IntervalVariable inter = new IntervalVariable();
            //inter.FindRozmah();
            //inter.GenerateSample(0,20,50);
            //inter.FormFrequencyTable();
            NumericalCharacteristics.Mode(ls);

            //v.GenerateSample(0, 10, 5000000);
            //v.ShowVariationRange();
            //v.FormFrequencyTable();
            //v.EmpiricalCDF();
            //Console.WriteLine(NumericalCharacteristics.Mean(v.VariationRange));

            //string x_axis = "";
            //string y_axis = "";

            //foreach (KeyValuePair<double, double> keyValue in points)
            //{
            //    x_axis += keyValue.Key.ToString("F") + "|";
            //    y_axis += keyValue.Value.ToString("F") + "|";
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