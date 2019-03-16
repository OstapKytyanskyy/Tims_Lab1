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
        public decimal DifferenceBetweenCentres { get; set; }
        public List<List<decimal>> Intervals { get; set; } = new List<List<decimal>>();
        public decimal Interval { get; set; }
        public List<decimal> WeightOfInterval { get; set; } = new List<decimal>();
        
        public Dictionary<List<decimal>, int> OriginalStatisticMaterial { get; set; } = new Dictionary<List<decimal>, int>();

        public override void ReadFromFile()
        {
            var lines = System.IO.File.ReadAllLines(@"C:\Users\ostap\source\repos\tims_calculation\tims_calculation\TextFiles\ITF.txt");
            List<double> Z_i = new List<double>();
            List<int> M_i ;
            
            var splited = lines[0].Split(',').Select(item => item.Split(' '));
            foreach (var splt in splited)
            { 
                Intervals.Add(new List<decimal> { Convert.ToDecimal(splt[0]), Convert.ToDecimal(splt[1]) });
            }

            foreach(var i in Intervals)
            {
                Z_i.Add(Convert.ToDouble(((i[0] + i[1])/2).ToString("F2")));

            }


            M_i = lines[1].Split(' ').Select(item => Convert.ToInt32(item)).ToList();

            OriginalStatisticMaterial = Intervals.Zip(M_i, (k, v) => new List<object> { k, v })
                .ToDictionary((key)=> (List<decimal>)key[0], (value) => Convert.ToInt32(value[1])); 
            
            FrequencyTable = Z_i.Zip(M_i, (k, v) => new List<double> { k,v} )
                .ToDictionary((key) => key[0], (value) => Convert.ToInt32(value[1]));
            
            FromTableToVariationRange();
            FindRozmah();
            FindAmountsOfIntervals();
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
            Interval = Convert.ToDecimal((Rozmah / AmountsOfInterval).ToString("F2")) + 0.01m;
            decimal begin = Convert.ToDecimal(VariationRange[0]);
            decimal end = Convert.ToDecimal( (Convert.ToDecimal(VariationRange[0]) + Interval).ToString("F2"));
            for(int i = 0; i < AmountsOfInterval; i++)
            {
                Intervals.Add(new List<decimal> { begin, end });
                if (i == AmountsOfInterval-1)
                {
                    end += 0.01m;
                }
                int X_i = VariationRange.Count(num => (decimal)num >= begin && (decimal)num < end);
                OriginalStatisticMaterial.Add(new List<decimal> { begin, end }, X_i);
                decimal center = Convert.ToDecimal(((end + begin) / 2).ToString("F2"));
                FrequencyTable.Add(Convert.ToDouble(center) , X_i);
                begin = end;
                end += Interval;
            }
        }

        //public void GetDiffernceBetweenCenters()
        //{
        //    var ls = FrequencyTable.Keys.ToList();
        //    DifferenceBetweenCentres = Convert.ToDecimal(ls[1]) - Convert.ToDecimal(ls[0]);
        //}

        public void FormWeight()
        {
            decimal sum = FrequencyTable.Values.Sum();
            WeightOfInterval.Add(0);
            foreach(var i in FrequencyTable.Values)
            {
                WeightOfInterval.Add(i / sum);
            }
        }

        public List<string> ShowEmpiricalCDF(out List<string> expr )
        {
            expr = new List<string>();
            List<string> result = new List<string>();
            if (WeightOfInterval.Count == 0)
            {
                FormWeight();
            }
           
            result.Add($" 0  x<= {OriginalStatisticMaterial.Keys.First().First()}");

            var intervals = OriginalStatisticMaterial.Keys.ToList();
            
            for(int i = 0; i < intervals.Count; i++)
            {
                expr.Add($"{WeightOfInterval[i + 1] / (intervals[i][1] - intervals[i][0])} (x - {intervals[i][0]}) + {WeightOfInterval[i]}");
                result.Add($"{WeightOfInterval[i + 1] / (intervals[i][1] - intervals[i][0])} (x - {intervals[i][0]}) + {WeightOfInterval[i]}    :    {intervals[i][0]} < x <= {intervals[i][1]}");
            }
            result.Add($"1    :    x > {intervals.Last()[1]}");
            return result;

        }

        public string PackParametrsToPython(out string result,out string x_axis)
        {
            result = "";
            x_axis = "";
            List<string> expr;
            var ecdf = ShowEmpiricalCDF(out expr);
            foreach(var item in ecdf)
            {
                result += item + "|";
            }

            foreach (KeyValuePair<double, int> keyValue in FrequencyTable)
            {
                x_axis += keyValue.Key.ToString("F") + "|";
                //y_axis += keyValue.Value.ToString("F") + "|";

            }

            return result;
        }

        public void ShowVariantionRange()
        {
            base.ShowVariationRange();
        }

    }
}
