using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tims_calculation
{
    class IntervalVariable : BaseVariable 
    {
        public decimal Rozmah { get; private set; }
        public decimal AmountsOfInterval { get; private set; }
        public List<List<decimal>> Intervals { get; set; } = new List<List<decimal>>();
        public List<List<double>> OriginIntervals { get; set; } = new List<List<double>>(); 

        public override void ReadFromFile()
        {
            var lines = System.IO.File.ReadAllLines(@"C:\Users\ostap\source\repos\tims_calculation\tims_calculation\TextFiles\ITF.txt");
            List<double> Z_i = new List<double>();
            List<int> M_i ;
            
            var splited = lines[0].Split(',').Select(item => item.Split(' '));
            foreach (var splt in splited)
            { 
                OriginIntervals.Add(new List<double> { Convert.ToDouble(splt[0]), Convert.ToDouble(splt[1]) });
            }

            foreach(var i in OriginIntervals)
            {
                Z_i.Add(Convert.ToDouble(((i[0] + i[1])/2).ToString("F2")));

            }

            M_i = lines[1].Split(' ').Select(item => Convert.ToInt32(item)).ToList();

            FrequencyTable = Z_i.Zip(M_i, (k, v) => new List<double> { k,v} ).ToDictionary((key) => key[0], (value) => Convert.ToInt32(value[1]));
            FromTableToVariationRange();
        }

        private void FindRozmah()
        {
            Rozmah = Convert.ToDecimal(VariationRange.Max() - VariationRange.Min());
        }

        public new void GenerateSample(double begin,double end,int volume)
        {
            base.GenerateSample(begin, end, volume);
            FindRozmah();
            FindAmountsOfIntervals();
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
                if (i == AmountsOfInterval-1)
                {
                    end += 0.01m;
                }
                int X_i = VariationRange.Count(num => (decimal)num >= begin && (decimal)num < end);
                decimal center = Convert.ToDecimal(((end + begin) / 2).ToString("F2"));
                FrequencyTable.Add(Convert.ToDouble(center) , X_i);
                begin = end;
                end += interval;
            }
        }



        public void ShowVariantionRange()
        {
            base.ShowVariationRange();
        }

    }
}
