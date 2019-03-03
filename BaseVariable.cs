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
        public List<double> VariationRange { get;  set; } 
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
        public virtual void ReadFromFile()
        {

        }

        public void FromTableToVariationRange()
        {
            if (FrequencyTable.Count == 0)
            {
                return;
            }
            foreach (KeyValuePair<double, int> keyValue in FrequencyTable)
            {
                int i = 0;
                while (i < keyValue.Value)
                {
                    VariationRange.Add(keyValue.Key);
                    i++;
                }

            }

        }

        public string ShowVariationRange()
        {
            string result = "";
            foreach (var num in VariationRange)
            {
                result += num + " ";
            }
            return result;
        }

    }
}
