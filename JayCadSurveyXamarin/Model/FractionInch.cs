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
		public double FractionInchValue { get; set; }          // Value in Fractioninches e.g 2" is  2
		
		static List<FractionInch> _fractionInchesList;

		public static List<FractionInch> AllFractionInches
		{
			get
			{
				if (_fractionInchesList == null)
				{ 
					_fractionInchesList = new List<FractionInch>
					{
                        new FractionInch { DisplayValue="0/16 of an Inch", FractionInchValue=0},
						new FractionInch { DisplayValue="1/16 of an Inch", FractionInchValue=1.0 },
						new FractionInch { DisplayValue="2/16 of an Inch", FractionInchValue=2.0 },
						new FractionInch { DisplayValue="3/16 of an Inch", FractionInchValue=3.0 },
						new FractionInch { DisplayValue="4/16 of an Inch", FractionInchValue=4.0},
						new FractionInch { DisplayValue="5/16 of an Inch", FractionInchValue=5.0 },
						new FractionInch { DisplayValue="6/16 of an Inch", FractionInchValue=6.0 },
						new FractionInch { DisplayValue="7/16 of an Inch", FractionInchValue=7.0 },
						new FractionInch { DisplayValue="8/16 of an Inch", FractionInchValue=8.0 },
						new FractionInch { DisplayValue="9/16 of an Inch", FractionInchValue=9.0 },
						new FractionInch { DisplayValue="10/16 of an Inch", FractionInchValue=10.0 },
						new FractionInch { DisplayValue="11/16 of an Inch", FractionInchValue=11.0 },
						new FractionInch { DisplayValue="12/16 of an Inch", FractionInchValue=12.0},
						new FractionInch { DisplayValue="13/16 of an Inch", FractionInchValue=13.0 },
						new FractionInch { DisplayValue="14/16 of an Inch", FractionInchValue=14.0 },
						new FractionInch { DisplayValue="15/16 of an Inch", FractionInchValue=15.0 }

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
