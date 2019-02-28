using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;

namespace tims_calculation
{
    class NumericalCharacteristics
    {

        #region DoubleFunctions
        public static double Median(List<double> varRange)
        {
            //return varRange.Count / 2 == 0 ?
            //    (varRange[(varRange.Count - 1) / 2] + varRange[(varRange.Count / 2)]) / 2 :
            //    varRange[varRange.Count / 2];
            return SortedArrayStatistics.Median(varRange.ToArray());
        }

        public static List<double> Mode(Dictionary<double,int> freqTable)
        {
            return (freqTable.Where(pair => pair.Value == freqTable.Values.Max()).Select(pair => pair.Key)).ToList();
           
        }

        public static double Mean(List<double> varRange)
        {
           
            return Statistics.Mean(varRange);
        }

        public static double Deviation(List<double> varRange)
        {
            return (varRange.Count - 1) * Statistics.Variance(varRange);
        }

        public static double Variance(List<double> varRange)
        {
            return Statistics.Variance(varRange);
        }

        public static double StandartDeviation(List<double> varRange)
        {
            return Statistics.StandardDeviation(varRange);
        }

        public static double VariationOfRow(List<double> varRange)
        {
            return StandartDeviation(varRange) / Mean(varRange);
        }

        public static double Skewness(List<double> varRange)
        {
            return Statistics.Skewness(varRange);
        }

        public static double Kurtoris(List<double> varRange)
        {
            return Statistics.Kurtosis(varRange);
        }
        #endregion

        #region DecimalFunctions

        public static List<decimal> Mode(Dictionary<decimal,int> Table)
        {
            return Table.Where(item => item.Value == Table.Values.Max()).Select(item=> item.Key).ToList();
        }

      //  public static double Mean (Dictionary<decimal,int> Table)
        //{
          //  int mean = 0;


        //}

        #endregion
    }
}
