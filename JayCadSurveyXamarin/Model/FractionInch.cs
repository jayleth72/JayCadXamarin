using System;
using System.Collections.Generic;

namespace JayCadSurveyXamarin.Model
{
    /// <summary>
    /// Fraction inch. Fraction Inch is 1/184 of a decimal foot
    /// </summary>
    public class FractionInch
    {
		public string DisplayValue { get; set; }            // Value displayed in Picker
		public int FractionInchValue { get; set; }          // Value in Fractioninches e.g 2" is  2
		public double DecimalFeetValue { get; set; }        // 16 FractionInches makes 1 inch

		static List<FractionInch> _fractionInchesList;

		public static List<FractionInch> AllFractionInches
		{
			get
			{
				if (_fractionInchesList == null)
				{
					_fractionInchesList = new List<FractionInch>
					{
						new FractionInch { DisplayValue="1/16", FractionInchValue=1/16, DecimalFeetValue=1/184 },
						new FractionInch { DisplayValue="2/16", FractionInchValue=2/16, DecimalFeetValue=2/184 },
						new FractionInch { DisplayValue="3/16", FractionInchValue=3/16, DecimalFeetValue=3/184 },
						new FractionInch { DisplayValue="4/16", FractionInchValue=4/16, DecimalFeetValue=4/184 },
						new FractionInch { DisplayValue="5/16", FractionInchValue=5/16, DecimalFeetValue=5/184 },
						new FractionInch { DisplayValue="6/16", FractionInchValue=6/16, DecimalFeetValue=6/184 },
						new FractionInch { DisplayValue="7/16", FractionInchValue=7/16, DecimalFeetValue=7/184 },
						new FractionInch { DisplayValue="8/16", FractionInchValue=8/16, DecimalFeetValue=8/184 },
						new FractionInch { DisplayValue="9/16", FractionInchValue=9/16, DecimalFeetValue=9/184 },
						new FractionInch { DisplayValue="10/16", FractionInchValue=10/16, DecimalFeetValue=10/184 },
						new FractionInch { DisplayValue="11/16", FractionInchValue=11/16, DecimalFeetValue=11/184 },
						new FractionInch { DisplayValue="12/16", FractionInchValue=12/16, DecimalFeetValue=12/184 },
						new FractionInch { DisplayValue="13/16", FractionInchValue=13/16, DecimalFeetValue=13/184},
						new FractionInch { DisplayValue="14/16", FractionInchValue=14/16, DecimalFeetValue=14/184 },
						new FractionInch { DisplayValue="15/16", FractionInchValue=15/16, DecimalFeetValue=15/184 }

					};
				}

				return _fractionInchesList;
			}
		}

        public FractionInch()
        {
        }
    }
}
