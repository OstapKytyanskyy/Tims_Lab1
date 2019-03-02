using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace tims_calculation
{
    public class UI
    {
        public static void StartUI(EnumVariables variable)
        {
            if (variable.GetType().ToString() == EnumVariables.DescretVariable.GetType().ToString())
            {
                DescreteVariable desvar = new DescreteVariable();
                ;
                Console.WriteLine("For Decrete Variable");
                Console.WriteLine("Enter begin of sample, end and volume");
                
                var begin = Convert.ToDouble(Console.ReadKey());
                var end = Convert.ToDouble(Console.ReadKey());
                var volume = Convert.ToInt32(Console.ReadKey());
                
                desvar.GenerateSample(begin,end,volume);
                Console.WriteLine($"Varaition Range : " );
                desvar.ShowVariationRange();
                
                Console.WriteLine($"Mode : {NumericalCharacteristics.Mode(desvar.VariationRange)}");
                
                
            }
            
            
        }
    }
}