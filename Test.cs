using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxCalculatorInterviewTests
{

	public interface ITaxCalculator
	{
		double GetStandardTaxRate(TaxCalculator.Commodity commodity);
		void SetCustomTaxRate(TaxCalculator.Commodity commodity, double rate);
		double GetTaxRateForDateTime(TaxCalculator.Commodity commodity, DateTime date);
		double GetCurrentTaxRate(TaxCalculator.Commodity commodity);
	}

	public class TaxCalculator : ITaxCalculator
	{

		public enum Commodity
		{
			//PLEASE NOTE: THESE ARE THE ACTUAL TAX RATES THAT SHOULD APPLY, WE JUST GOT THEM FROM THE CLIENT!
			Default,            //25%
			Alcohol,            //25%
			Food,               //12%
			FoodServices,       //12%
			Literature,         //6%
			Transport,          //6%
			CulturalServices    //6%
		}

		public double GetStandardTaxRate(Commodity commodity)
		{
			switch (commodity)
			{
				case Commodity.Alcohol:
					return 0.25;
				case Commodity.Food:
				case Commodity.FoodServices:
					return 0.12;
				case Commodity.Literature:
				case Commodity.Transport:
				case Commodity.CulturalServices:
					return 0.6;
			}

			return 0.25;
		}

		
		public void SetCustomTaxRate(Commodity commodity, double rate)
		{
			_customRates[Tuple.Create(commodity, DateTime.Now)] = rate;
		}
		static Dictionary<Tuple<Commodity, DateTime>, double> _customRates = new Dictionary<Tuple<Commodity, DateTime>, double>();


		public double GetTaxRateForDateTime(Commodity commodity, DateTime date)
		{
			return _customRates[Tuple.Create(commodity, date)];
		}

				public double GetCurrentTaxRate(Commodity commodity)
		{
			var key = _customRates.Keys.Where(x => x.Item1 == commodity)
										.OrderByDescending(x => x.Item2)
										.FirstOrDefault();
			if (key == null)
			{
				return GetStandardTaxRate(commodity);
			}

			return _customRates[key];
		}

	}
}
