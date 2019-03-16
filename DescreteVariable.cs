using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Statistics;
using System.Text;
using System.Threading.Tasks;

namespace tims_calculation
{
    class DescreteVariable :  BaseVariable
    {
        
        public DescreteVariable() : base()
        {
        }



        public override void ReadFromFile()
        {
            var lines = System.IO.File.ReadAllLines(@"C:\Users\ostap\source\repos\tims_calculation\tims_calculation\TextFiles\DTF.txt");

            var X_i = lines[0].Split(' ').Select(item => Convert.ToDouble(item)).ToList();
            var M_i = lines[1].Split(' ').Select(item => Convert.ToInt32(item)).ToList();

            FrequencyTable = X_i.Zip(M_i, (x, m) => new List<double> { x, m } ).ToDictionary((keyItem) => keyItem[0] ,(valueItem) => Convert.ToInt32(valueItem[1]));
            FromTableToVariationRange();
        }

        public void PackParametrsToPython(out string x_axis,out string y_axis,out string ecdf)
        {
            EmpiricalCDF();
            x_axis = "";
            y_axis = "";
            ecdf = "";
            foreach (KeyValuePair<double, int> keyValue in FrequencyTable)
            {
                x_axis += keyValue.Key.ToString("F") + "|";
                y_axis += keyValue.Value.ToString("F") + "|";

            }
            foreach(var val in EmpCDFValues)
            {
                ecdf += val.ToString("F2") + "|";
            }
            

        }

        public  void FormFrequencyTable()
        {
            FrequencyTable = VariationRange.GroupBy(val => val).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Count());
        }

        public  void EmpiricalCDF()
        {
            foreach(double num in VariationRange.Distinct())
            {
                EmpCDFValues.Add(Statistics.EmpiricalCDF(VariationRange, num));
            }
        }

        public void ShowEmpericalCDF()
        {
            var x_i = VariationRange.Distinct().ToList();
            Console.WriteLine($"0\t x < {x_i[0]}");

            int i = 0, j = 1 ;
            while(i < x_i.Count && j < x_i.Count)
            {
                Console.WriteLine($"{EmpCDFValues[i]} \t {x_i[i]} <= x < {x_i[j]}");
                ++i;
                ++j;
            }
            Console.WriteLine($"{1} \t  x >= {x_i[i]}");
        }

    }
}
