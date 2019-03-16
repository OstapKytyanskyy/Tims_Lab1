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
            Console.WriteLine("If you want to read from file press F or press G if u want to do it randomly");
            var way = Console.ReadKey();
            if (chose.Key.ToString().ToUpper() == "D" && way.Key.ToString().ToUpper() == "G")
            {
                UI.StartUI(EnumVariables.DescretVariable, WayOfCreation.GenerateRandomly);
            }
            else if (chose.Key.ToString().ToUpper() == "D" && way.Key.ToString().ToUpper() == "F")
            {
                UI.StartUI(EnumVariables.DescretVariable, WayOfCreation.FromFile);
            }
            else
            {
                UI.StartUI(EnumVariables.IntervalVariable, WayOfCreation.GenerateRandomly);
            }


            //DescreteVariable v = new DescreteVariable();
            //v.ReadFromFile();
            //v.EmpiricalCDF();
            //v.ShowEmpericalCDF();
            //MathNet.Numerics.Statistics.SortedArrayStatistics.LowerQuartile(v.VariationRange.ToArray());
            //MathNet.Numerics.Statistics.SortedArrayStatistics.UpperQuartile(v.VariationRange.ToArray());
            //MathNet.Numerics.Statistics.SortedArrayStatistics.Quantile(v.VariationRange.ToArray(), 0.98);
            //MathNet.Numerics.Statistics.SortedArrayStatistics.Quantile(v.VariationRange.ToArray(), 1);
            ////v.FromTableToVariationRange();
            //v.GenerateSample(0,5,10);
            ////List<double> ls = new List<double> {8,7,6,9,10,9,11,8,9,10,8,9,6,9,8,10,7,10,12,7 };
            //IntervalVariable inter = new IntervalVariable();
            //inter.ReadFromFile();
            ////UI.PrintStatis(inter);
            ////inter.ShowFrequencyTable();
            //////inter.FindRozmah();
            //////inter.GenerateSample(0,20,50);
            //////inter.FormFrequencyTable();
            //NumericalCharacteristics.Median(inter);
            ////List<string> vs;
            ////inter.ShowEmpiricalCDF(out vs);
            //string g;
            //string x_axis;
            //inter.PackParametrsToPython(out g,out x_axis);
            //RunPython(Path: @"C:\Users\ostap\PycharmProjects\tims\ecdf_interval.py", x_axis: x_axis, ecdf_interval: g);
            //// = NumericalCharacteristics.Mode(inter.FrequencyTable);

            ////v.GenerateSample(0, 10, 5000000);
            ////v.ShowVariationRange();
            //v.FormFrequencyTable();
            //v.EmpiricalCDF();
            ////Console.WriteLine(NumericalCharacteristics.Mean(v.VariationRange));


            Console.ReadLine();
        }

        
        public static void RunPython(string Path, string x_axis = null, string y_axis = null, string ecdf_discrete = null, string ecdf_interval = null)
        {

            string progToRun = Path;
            char[] spliter = { '\r' };
           
            Process proc = new Process();
            proc.StartInfo.FileName = "python.exe";
            proc.StartInfo.Arguments = string.Format("{0} {1} {2} {3} {4}", progToRun, x_axis, y_axis,ecdf_discrete, ecdf_interval);

            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;

           
            proc.Start();

            //StreamReader sReader = proc.StandardOutput;
            //string[] output = sReader.ReadToEnd().Split(spliter);
            //var y = sReader.ReadToEnd().Split(spliter);
            ////var message = System.Convert.FromBase64String(y[0]);
            //foreach (string s in output)
            //    Console.WriteLine(s);
            //var x = System.Text.Encoding.UTF8.GetString(sReader.ReadToEnd());
            //Console.WriteLine(x);
            proc.WaitForExit();

        }
    }
}