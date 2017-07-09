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
		public double InchValue { get; set; }                  // Value in inches e.g 2" is  2
	
		static List<Inches> _inchesList;

		public static List<Inches> AllInches
		{
			get
			{
				if (_inchesList == null)
				{
					_inchesList = new List<Inches>
					{
                        new Inches { DisplayValue="0 inches", InchValue=0.0 },
						new Inches { DisplayValue="1 inches", InchValue=1.0 },
                        new Inches { DisplayValue="2 inches", InchValue=2.0  },
						new Inches { DisplayValue="3 inches", InchValue=3.0  },
						new Inches { DisplayValue="4 inches", InchValue=4.0  },
						new Inches { DisplayValue="5 inches", InchValue=5.0  },
						new Inches { DisplayValue="6 inches", InchValue=6.0  },
						new Inches { DisplayValue="7 inches", InchValue=7.0  },
						new Inches { DisplayValue="8 inches", InchValue=8.0  },
						new Inches { DisplayValue="9 inches", InchValue=9.0  },
						new Inches { DisplayValue="10 inches", InchValue=10.0  },
						new Inches { DisplayValue="11 inches", InchValue=11.0  }
											
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
