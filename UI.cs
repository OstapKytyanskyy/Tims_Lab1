using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace tims_calculation
{
    class UI
    {
        public static string PrintMode(double s)
        {
            return s.ToString();
        }



        public static void StartUI(EnumVariables variable, WayOfCreation wayOfCreation)
        {

            if (variable == EnumVariables.DescretVariable && wayOfCreation == WayOfCreation.GenerateRandomly)
            {
                DescreteVariable desvar = new DescreteVariable();
                
                Console.WriteLine("\nFor Decrete Variable");
                Console.WriteLine("Enter begin of sample, end and volume");

                dynamic input = Console.ReadLine();
                input = input.ToString().Split(' ');

                

                desvar.GenerateSample(Convert.ToDouble(input[0]), Convert.ToDouble(input[1]),Convert.ToInt32(input[2]));
                desvar.FormFrequencyTable();

                PrintStatis(desvar);

                string x_axis;
                string y_axis;
                string ecdf;
              
                desvar.PackParametrsToPython(out x_axis, out y_axis, out ecdf);

                PlotingGraphs(x_axis, y_axis, ecdf);
                
                

            }
            else if(variable == EnumVariables.DescretVariable && wayOfCreation == WayOfCreation.FromFile)
            {
                DescreteVariable desvar = new DescreteVariable();

                desvar.ReadFromFile();
                desvar.ShowVariationRange();

                PrintStatis(desvar);

                string x_axis;
                string y_axis;
                string ecdf;

                desvar.PackParametrsToPython(out x_axis, out y_axis, out ecdf);

                PlotingGraphs(x_axis, y_axis, ecdf);


            }
            else if(variable == EnumVariables.IntervalVariable && wayOfCreation == WayOfCreation.GenerateRandomly)
            {
                IntervalVariable intvar = new IntervalVariable();
                intvar.GenerateSample(0, 10, 80);
                intvar.FormFrequencyTable();

            }


        }

        public static void PlotingGraphs(string x_axis,string y_axis,string ecdf)
        {
            Task[] tasks = new Task[3]
                {
                    new Task(() => Programm.RunPython(x_axis, y_axis, ecdf, @"C:\Users\ostap\PycharmProjects\tims\main.py")),
                    new Task(() => Programm.RunPython(x_axis, y_axis, ecdf, @"C:\Users\ostap\PycharmProjects\tims\poligon.py")),
                    new Task(() => Programm.RunPython(x_axis, y_axis, ecdf, @"C:\Users\ostap\PycharmProjects\tims\ECDF.py"))
                };

            foreach (var t in tasks)
                t.Start();
            Task.WaitAll(tasks);
        }


        public static void PrintStatis(DescreteVariable desvar)
        {
            List<string> resultText = new List<string>();
            resultText.Add("Variation Range : " + desvar.ShowVariationRange());
            var ls = NumericalCharacteristics.Mode(desvar.FrequencyTable);
            string mode = " ";
            foreach (var md in ls)
            {
                mode += md.ToString() + " ";
            }
            resultText.Add("Mode : " + mode + " ");



            resultText.Add($"Median : {NumericalCharacteristics.Median(desvar.VariationRange)}");

            resultText.Add($"Mean : {NumericalCharacteristics.Mean(desvar.VariationRange)}");

            resultText.Add($"Deviation : {NumericalCharacteristics.Deviation(desvar.VariationRange)}");

            resultText.Add($"Variance : {NumericalCharacteristics.Variance(desvar.VariationRange)}");

            resultText.Add($"Standart Deviation : {NumericalCharacteristics.StandartDeviation(desvar.VariationRange)}");

            resultText.Add($"Variation of Row : {NumericalCharacteristics.VariationOfRow(desvar.VariationRange)}");

            resultText.Add($"Skewness : {NumericalCharacteristics.Skewness(desvar.VariationRange)}");

            resultText.Add($"Kurtoris : {NumericalCharacteristics.Kurtoris(desvar.VariationRange)}");

            System.IO.File.WriteAllLines(@"C:\Users\ostap\source\repos\tims_calculation\tims_calculation\TextFiles\Statistics.txt", resultText);

            foreach (var line in System.IO.File.ReadAllLines(@"C:\Users\ostap\source\repos\tims_calculation\tims_calculation\TextFiles\Statistics.txt"))
            {
                Console.WriteLine(line);
            }
        }


        public static void PrintStatis(IntervalVariable intervar)
        {
            List<string> resultText = new List<string>();

            var ls = NumericalCharacteristics.Mode(intervar);
            string mode = " ";
            foreach (var md in ls)
            {
                mode += md.ToString() + " ";
            }
            resultText.Add("Mode : " + mode + " ");

            resultText.Add($"Mean : {NumericalCharacteristics.Mean(intervar.VariationRange)}");

            resultText.Add($"Median : {NumericalCharacteristics.Median(intervar)}");

            resultText.Add($"Deviation : {NumericalCharacteristics.Deviation(intervar.VariationRange)}");

            resultText.Add($"Variance : {NumericalCharacteristics.Variance(intervar.VariationRange)}");

            resultText.Add($"Standart Deviation : {NumericalCharacteristics.StandartDeviation(intervar.VariationRange)}");

            resultText.Add($"Variation of Row : {NumericalCharacteristics.VariationOfRow(intervar.VariationRange)}");

            resultText.Add($"Skewness : {NumericalCharacteristics.Skewness(intervar.VariationRange)}");

            resultText.Add($"Kurtoris : {NumericalCharacteristics.Kurtoris(intervar.VariationRange)}");

            System.IO.File.WriteAllLines(@"C:\Users\ostap\source\repos\tims_calculation\tims_calculation\TextFiles\Statistics.txt", resultText);

            foreach (var line in System.IO.File.ReadAllLines(@"C:\Users\ostap\source\repos\tims_calculation\tims_calculation\TextFiles\Statistics.txt"))
            {
                Console.WriteLine(line);
            }
        }

    }
}