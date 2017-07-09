using System;
using System.Collections.Generic;

namespace JayCadSurveyXamarin.Model
{
    public class Perches
    {
		public string DisplayValue { get; set; }                // Value displayed in Picker
		public double perchesValue { get; set; }                  // Value of Rood e.g 2" is  2

        static List<Perches> _perchesList;

		public static List<Perches> AllPerches
		{
			get
			{
				if (_perchesList == null)
				{
					_perchesList = new List<Perches>
					{
						new Perches { DisplayValue="0 Perches", perchesValue=0},
						new Perches { DisplayValue="1 Perches", perchesValue=1.0 },
						new Perches { DisplayValue="2 Perches", perchesValue=2.0 },
						new Perches { DisplayValue="3 Perches", perchesValue=3.0 },
						new Perches { DisplayValue="4 Perches", perchesValue=4.0},
						new Perches { DisplayValue="5 Perches", perchesValue=5.0 },
						new Perches { DisplayValue="6 Perches", perchesValue=6.0 },
						new Perches { DisplayValue="7 Perches", perchesValue=7.0 },
						new Perches { DisplayValue="8Perches", perchesValue=8.0 },
						new Perches { DisplayValue="9 Perches", perchesValue=9.0 },
						new Perches { DisplayValue="10 Perches", perchesValue=10.0 },
						new Perches { DisplayValue="11 Perches", perchesValue=11.0 },
						new Perches { DisplayValue="12 Perches", perchesValue=12.0},
						new Perches { DisplayValue="13 Perches", perchesValue=13.0 },
						new Perches { DisplayValue="14 Perches", perchesValue=14.0 },
						new Perches { DisplayValue="15 Perches", perchesValue=15.0 },
						new Perches { DisplayValue="16 Perches", perchesValue=16.0 },
						new Perches { DisplayValue="17 Perches", perchesValue=17.0},
						new Perches { DisplayValue="18 Perches", perchesValue=18.0 },
						new Perches { DisplayValue="19 Perches", perchesValue=19.0 },
						new Perches { DisplayValue="20 Perches", perchesValue=20.0 },
                        new Perches { DisplayValue="21 Perches", perchesValue=21.0 },
                        new Perches { DisplayValue="22 Perches", perchesValue=22.0 },
                        new Perches { DisplayValue="23 Perches", perchesValue=23.0 },
                        new Perches { DisplayValue="24 Perches", perchesValue=24.0 },
                        new Perches { DisplayValue="25 Perches", perchesValue=25.0 },
                        new Perches { DisplayValue="26 Perches", perchesValue=26.0 },
                        new Perches { DisplayValue="27 Perches", perchesValue=27.0 },
                        new Perches { DisplayValue="28 Perches", perchesValue=28.0 },
                        new Perches { DisplayValue="29 Perches", perchesValue=29.0 },
                        new Perches { DisplayValue="30 Perches", perchesValue=30.0 },
                        new Perches { DisplayValue="31 Perches", perchesValue=31.0 },
                        new Perches { DisplayValue="32 Perches", perchesValue=32.0 },
                        new Perches { DisplayValue="33 Perches", perchesValue=33.0 },
                        new Perches { DisplayValue="34 Perches", perchesValue=34.0 },
                        new Perches { DisplayValue="35 Perches", perchesValue=35.0 },
                        new Perches { DisplayValue="36 Perches", perchesValue=36.0 },
                        new Perches { DisplayValue="37 Perches", perchesValue=37.0 },
                        new Perches { DisplayValue="38 Perches", perchesValue=38.0 },
                        new Perches { DisplayValue="39 Perches", perchesValue=39.0 },
                        new Perches { DisplayValue="40 Perches", perchesValue=40.0 }
					};
				}

				return _perchesList;
			}
		}

		public Perches()
        {
        }
    }
}
