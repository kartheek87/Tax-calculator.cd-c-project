using System;
using TaxCalculatorInterviewTests;

namespace Test
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			ITaxCalculator sut = new TaxCalculator();

            for (int i = 0; i < 10; i++)
            {
				sut.SetCustomTaxRate(TaxCalculator.Commodity.Food, 1 * i);
				System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(12));
            }

            Console.WriteLine(sut.GetCurrentTaxRate(TaxCalculator.Commodity.Food));
        }
    }
}
