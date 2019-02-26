using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Statistics;
using System.Text;
using System.Threading.Tasks;

namespace tims_calculation
{
    class DiscreteVariable
    {
        public Dictionary<double, int> FrequencyTable { get; private set; } = new Dictionary<double, int>();
        public List<double> VariationRange { get; private set; } = new List<double>();
        public List<double> EmpCDFValues { get; private set; } = new List<double>();
        public void GenerateSample(double begin,double end,int volume)
        {
            for (int i = 0; i < volume; i++)
            {
                VariationRange.Add(Programm.GetRandom(begin, end));
            }
            VariationRange.Sort();
        }

        public void ShowVariationRange()
        {
            foreach(var num in VariationRange)
            {
                Console.Write($"{num}  ");
            }
            Console.WriteLine();
        }

        public void FormFrequencyTable()
        {
            FrequencyTable = VariationRange.GroupBy(val => val).ToDictionary((keyitem) => keyitem.Key, (valueItem) => valueItem.Count());
        }

        public void EmpiricalCDF()
        {
            foreach(var num in VariationRange.Distinct())
            {
                EmpCDFValues.Add(Statistics.EmpiricalCDF(VariationRange, num));
            }
        }

    }
}
