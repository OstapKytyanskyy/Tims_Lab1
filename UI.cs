using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace tims_calculation
{
    public class UI
    {
        public static void PrintMode(double s)
        {
            Console.WriteLine(s.ToString());
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
                desvar.ShowVariationRange();
                var ls = NumericalCharacteristics.Mode(desvar.FrequencyTable);
                Console.WriteLine($"Mode : ");
                ls.ForEach(PrintMode);

                Console.WriteLine($"Median : {NumericalCharacteristics.Median(desvar.VariationRange)}");

                Console.WriteLine($"Mean : {NumericalCharacteristics.Mean(desvar.VariationRange)}");

                Console.WriteLine($"Deviation : {NumericalCharacteristics.Deviation(desvar.VariationRange)}");

                Console.WriteLine($"Variance : {NumericalCharacteristics.Variance(desvar.VariationRange)}");

                Console.WriteLine($"Standart Deviation : {NumericalCharacteristics.StandartDeviation(desvar.VariationRange)}");

                Console.WriteLine($"Variation of Row : {NumericalCharacteristics.VariationOfRow(desvar.VariationRange)}");

                Console.WriteLine($"Skewness : {NumericalCharacteristics.Skewness(desvar.VariationRange)}");

                Console.WriteLine($"Kurtoris : {NumericalCharacteristics.Kurtoris(desvar.VariationRange)}");
                
                
                
            }
            else if(variable == EnumVariables.DescretVariable && wayOfCreation == WayOfCreation.FromFile)
            {
                DescreteVariable desvar = new DescreteVariable();

                desvar.ReadFromFile();
                desvar.ShowVariationRange();
                var ls = NumericalCharacteristics.Mode(desvar.FrequencyTable);
                Console.WriteLine($"Mode : ");
                ls.ForEach(PrintMode);

                Console.WriteLine($"Median : {NumericalCharacteristics.Median(desvar.VariationRange)}");

                Console.WriteLine($"Mean : {NumericalCharacteristics.Mean(desvar.VariationRange)}");

                Console.WriteLine($"Deviation : {NumericalCharacteristics.Deviation(desvar.VariationRange)}");

                Console.WriteLine($"Variance : {NumericalCharacteristics.Variance(desvar.VariationRange)}");

                Console.WriteLine($"Standart Deviation : {NumericalCharacteristics.StandartDeviation(desvar.VariationRange)}");

                Console.WriteLine($"Variation of Row : {NumericalCharacteristics.VariationOfRow(desvar.VariationRange)}");

                Console.WriteLine($"Skewness : {NumericalCharacteristics.Skewness(desvar.VariationRange)}");

                Console.WriteLine($"Kurtoris : {NumericalCharacteristics.Kurtoris(desvar.VariationRange)}");

            }
            else if(variable == EnumVariables.IntervalVariable && wayOfCreation == WayOfCreation.FromFile)
            {
                IntervalVariable intvar = new IntervalVariable();
                Console.WriteLine("\nFor Interval Variable");
                Console.WriteLine("Enter begin of sample, end and volume");

               

                Console.WriteLine($"Mean : {NumericalCharacteristics.Mean(intvar.VariationRange)}");

                Console.WriteLine($"Deviation : {NumericalCharacteristics.Deviation(intvar.VariationRange)}");

                Console.WriteLine($"Variance : {NumericalCharacteristics.Variance(intvar.VariationRange)}");

                Console.WriteLine($"Standart Deviation : {NumericalCharacteristics.StandartDeviation(intvar.VariationRange)}");

                Console.WriteLine($"Variation of Row : {NumericalCharacteristics.VariationOfRow(intvar.VariationRange)}");

                Console.WriteLine($"Skewness : {NumericalCharacteristics.Skewness(intvar.VariationRange)}");

                Console.WriteLine($"Kurtoris : {NumericalCharacteristics.Kurtoris(intvar.VariationRange)}");

            }


        }
    }
}