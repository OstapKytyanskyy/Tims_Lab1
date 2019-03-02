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



            //Console.WriteLine("Choose Variable, type D for Descrete and I for Interval");
            //var chose = Console.ReadKey();
            //Console.WriteLine("If you want to read from file press F or press G if u want to do it randomly");
            //var way = Console.ReadKey();
            //if (chose.Key.ToString().ToUpper() == "D" && way.Key.ToString().ToUpper() == "F")
            //{
            //    UI.StartUI(EnumVariables.DescretVariable,WayOfCreation.FromFile);
            //}
            //else
            //{
            //    UI.StartUI(EnumVariables.IntervalVariable,WayOfCreation.FromFile);
            //}


             DescreteVariable v = new DescreteVariable();
            //v.ReadFromFile();
            //v.FromTableToVariationRange();
            v.GenerateSample(0,5,10);
            //List<double> ls = new List<double> {8,7,6,9,10,9,11,8,9,10,8,9,6,9,8,10,7,10,12,7 };
            // IntervalVariable inter = new IntervalVariable();
            //inter.ReadFromFile();
            //inter.FindRozmah();
            //inter.GenerateSample(0,20,50);
            //inter.FormFrequencyTable();

            // = NumericalCharacteristics.Mode(inter.FrequencyTable);

            //v.GenerateSample(0, 10, 5000000);
            //v.ShowVariationRange();
            v.FormFrequencyTable();
            v.EmpiricalCDF();
            //Console.WriteLine(NumericalCharacteristics.Mean(v.VariationRange));

            string x_axis;
            string y_axis;
            string ecdf;
            v.FormFrequencyTable();
            v.PackParametrsToPython(out x_axis, out y_axis, out ecdf);
            Console.WriteLine(x_axis);
            Console.WriteLine(y_axis);
            // EmpiricalCDF(new List<double> { 0.1, 0.2, 0.3, 0.5, 1.6, 1.8, 3.8, 3.9, 4.3, 4.3 });
            Task.Run(() =>
            {
                RunPython(x_axis, y_axis, ecdf, @"C:\Users\ostap\PycharmProjects\tims\main.py");

            });
            Task.Run(() =>
            {
                RunPython(x_axis, y_axis, ecdf, @"C:\Users\ostap\PycharmProjects\tims\poligon.py");
            });
            Task.Run(() =>
            {
                RunPython(x_axis, y_axis, ecdf, @"C:\Users\ostap\PycharmProjects\tims\ECDF.py");
            });
            Console.ReadLine();
        }

        
        static void RunPython(string x_axis, string y_axis,string ecdf,string Path)
        {

            string progToRun = Path;
            char[] spliter = { '\r' };
           
            Process proc = new Process();
            proc.StartInfo.FileName = "python.exe";
            proc.StartInfo.Arguments = string.Format("{0} {1} {2} {3}", progToRun, x_axis, y_axis,ecdf);

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