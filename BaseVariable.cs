using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tims_calculation
{
    class BaseVariable
    {
        public Dictionary<double, int> FrequencyTable { get; set; } 
        public List<double> VariationRange { get; private set; } 
        public List<double> EmpCDFValues { get; private set; }
        public BaseVariable()
        {
            FrequencyTable = new Dictionary<double, int>();
            VariationRange = new List<double>();
            EmpCDFValues = new List<double>();
        }
        public void GenerateSample(double begin, double end, int volume)
        {
            for (int i = 0; i < volume; i++)
            {
                VariationRange.Add(Programm.GetRandom(begin, end));
            }
            VariationRange.Sort();
        }

        public void ShowVariationRange()
        {
            Console.WriteLine("Variation Range : ");
            foreach (var num in VariationRange)
            {
                Console.Write($"{num}  ");
            }
            Console.WriteLine();
        }

    }
}
