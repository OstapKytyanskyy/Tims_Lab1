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

        #region DescreteFunctions
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

        #region IntervalFunctions
        
        public static List<decimal> Mode(IntervalVariable intervar)
        {
            var modes = intervar.FrequencyTable.Where(item=> item.Value == intervar.FrequencyTable.Values.Max()).Select(item => item.Key).Distinct().ToList();
            List<decimal> Mode = new List<decimal>();
            int BeforeMode = 0;
            int AfterMode = 0;
           // intervar.GetDiffernceBetweenCenters();
            List<decimal> interval;
            foreach (var mode in modes)
            {
                var beginOfInterval = BeginOfInterval(intervar, Convert.ToDecimal(mode), out interval);
                if (intervar.FrequencyTable.ContainsKey(Convert.ToDouble(Convert.ToDecimal(mode) - interval[0])))
                {
                    BeforeMode = intervar.FrequencyTable[Convert.ToDouble(Convert.ToDecimal(mode) - interval[0])];
                    
                }
                if (intervar.FrequencyTable.ContainsKey(Convert.ToDouble(Convert.ToDecimal(mode) + interval[0])))
                {
                    AfterMode = intervar.FrequencyTable[Convert.ToDouble(Convert.ToDecimal(mode) + interval[0])];
                }
                if(!intervar.FrequencyTable.ContainsKey(Convert.ToDouble(Convert.ToDecimal(mode) - interval[0])))
                {
                    BeforeMode = 0;
                }
                else
                {
                    AfterMode = 0;
                }

                Mode.Add(BeginOfInterval(intervar, Convert.ToDecimal(mode),out interval)[0] +( interval[0] * ( (intervar.FrequencyTable[mode] - Convert.ToDecimal(BeforeMode)) /
                    ( (intervar.FrequencyTable[mode] - Convert.ToDecimal(BeforeMode)) + (intervar.FrequencyTable[mode] - Convert.ToDecimal(AfterMode)) ))));


            }

            return Mode;

        }


        private static List<decimal> BeginOfInterval(IntervalVariable intervar,decimal centerInterval,out List<decimal> differnece)
        {
            differnece = intervar.Intervals.Where(item => ((item[0] + item[1]) / 2) == centerInterval).Select(item => item[1] - item[0]).ToList();
            return intervar.Intervals.Where(item => ((item[0] + item[1]) / 2) == centerInterval).Select(item => item[0]).ToList();
        }
       

        public static decimal Median(IntervalVariable intervar)
        {
            int centerOfN = intervar.FrequencyTable.Values.Sum() / 2;
            List<decimal> difference;
            var MedianInterval = intervar.Intervals.Where(item => item[0] <= centerOfN && centerOfN <= item[1]).Select(item =>(item[0] + item[1])/2).ToList();
            var beginOfInterval = BeginOfInterval(intervar, MedianInterval[0], out difference);
            decimal Sme_1 = 0;
            if (intervar.FrequencyTable.ContainsKey(Convert.ToDouble(MedianInterval[0] - difference[0]) ))
            {
                Sme_1 = intervar.FrequencyTable[Convert.ToDouble(MedianInterval[0] - difference[0])];
            }
           
            decimal Mediana = beginOfInterval[0] + (difference[0] *
                ((Convert.ToDecimal(centerOfN) - Sme_1)/ intervar.FrequencyTable[Convert.ToDouble(MedianInterval[0])] ));

            return Mediana;
        }

        #endregion
    }
}
