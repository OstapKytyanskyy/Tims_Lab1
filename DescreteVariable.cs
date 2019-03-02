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


        public void GenerateSample(int begin, int end, int volume)
        {
            base.GenerateSample(begin, end, volume);
        }

        public void ShowVariantionRange()
        {
            base.ShowVariationRange();
        }


        public  void FormFrequencyTable()
        {
            FrequencyTable = VariationRange.GroupBy(val => val).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Count());
        }

        public  void EmpiricalCDF()
        {
            foreach(var num in VariationRange.Distinct())
            {
                EmpCDFValues.Add(Statistics.EmpiricalCDF(VariationRange, num));
            }
        }

    }
}
