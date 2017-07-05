using System;
using System.Collections.Generic;

namespace JayCadSurveyXamarin.Model
{   
    /// <summary>
    /// Class to represent Inches for Display in InchesPicker in LengthConversionPage
    /// Contains values for display, and values to facilitate conversion to decimal feet
    /// </summary>
    public class Inches
    {
        
		public string DisplayValue { get; set; }            // Value displayed in Picker
		public int InchValue { get; set; }                  // Value in inches e.g 2" is  2
		public double DecimalFeetValue { get; set; }        // 12 inches to 1 foot so 1" is 1/12 of a foot

		static List<Inches> _inchesList;

		public static List<Inches> AllInches
		{
			get
			{
				if (_inchesList == null)
				{
					_inchesList = new List<Inches>
					{
						new Inches { DisplayValue="1", InchValue=1, DecimalFeetValue=1/12 },
                        new Inches { DisplayValue="2", InchValue=2, DecimalFeetValue=2/12 },
						new Inches { DisplayValue="3", InchValue=3, DecimalFeetValue=3/12 },
						new Inches { DisplayValue="4", InchValue=4, DecimalFeetValue=4/12 },
						new Inches { DisplayValue="5", InchValue=5, DecimalFeetValue=5/12 },
						new Inches { DisplayValue="6", InchValue=6, DecimalFeetValue=6/12 },
						new Inches { DisplayValue="7", InchValue=7, DecimalFeetValue=7/12 },
						new Inches { DisplayValue="8", InchValue=8, DecimalFeetValue=8/12 },
						new Inches { DisplayValue="9", InchValue=9, DecimalFeetValue=9/12 },
						new Inches { DisplayValue="10", InchValue=10, DecimalFeetValue=10/12 },
						new Inches { DisplayValue="11", InchValue=11, DecimalFeetValue=11/12 },
						new Inches { DisplayValue="12", InchValue=12, DecimalFeetValue=12/12 },
						new Inches { DisplayValue="13", InchValue=13, DecimalFeetValue=13/12 },
						new Inches { DisplayValue="14", InchValue=14, DecimalFeetValue=14/12 },
						new Inches { DisplayValue="15", InchValue=15, DecimalFeetValue=15/12 }
						
					};
				}

				return _inchesList;
			}
		}

        public Inches()
        {
        }
    }
}
