using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tims_calculation
{
    class IntervalVariable 
    {
        public decimal Rozmah { get; private set; }
        public decimal AmountsOfInterval { get; private set; }
        public Dictionary<decimal, int> FrequencyTable { get; set; } = new Dictionary<decimal, int>();
        // public List<decimal> VariationRange { get; private set; } = new List<decimal>();
        public List<double> VariationRange { get; private set; } = new List<double>();
        public List<List<decimal>> Intervals { get; set; } = new List<List<decimal>>();
        public List<decimal> EmpCDFValues { get; private set; }

        public void GenerateSample(double begin,double end,int volume)
        {
            for (int i = 0; i < volume; i++)
            {
                VariationRange.Add(Programm.GetRandom(begin, end));
            }
            VariationRange.Sort();
            FindRozmah();
            FindAmountsOfIntervals();
        }

        public void FindRozmah()
        {
            Rozmah = Convert.ToDecimal(VariationRange.Max() - VariationRange.Min());
        }

        public void FindAmountsOfIntervals()
        {
            int r = 0;
            while(true)
            {
                if(Math.Pow(2,r) >= VariationRange.Count)
                {
                    AmountsOfInterval = r;
                    break;
                }
                r++;
            }
        }

        public void FormFrequencyTable()
        {
            decimal interval = Convert.ToDecimal((Rozmah / AmountsOfInterval).ToString("F2")) + 0.01m;
            decimal begin = Convert.ToDecimal(VariationRange[0]);
            decimal end = Convert.ToDecimal( (Convert.ToDecimal(VariationRange[0]) + interval).ToString("F2"));
            for(int i = 0; i < AmountsOfInterval; i++)
            {
                Intervals.Add(new List<decimal> { begin, end });
                int X_i = VariationRange.Where(num => (decimal)num >= begin && (decimal)num <= end).Count();
                decimal center = Convert.ToDecimal(((end + begin) / 2).ToString("F2"));
                FrequencyTable.Add(center , X_i);
                begin = end;
                end += interval;
            }
        }



        public void ShowVariantionRange()
        {
           // base.ShowVariationRange();
        }

    }
}
