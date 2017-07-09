using System;
using System.Collections.Generic;

namespace JayCadSurveyXamarin.Model
{
    /// <summary>
    /// Class to represent the old measurement Unit Roods.
    /// 1 rood is nearly 1/4 of an Acre
    /// </summary>
    public class Roods
    {
		public string DisplayValue { get; set; }                // Value displayed in Picker
		public double RoodsValue { get; set; }                  // Value of Rood e.g 2" is  2

		static List<Roods> _roodsList;

		public static List<Roods> AllRoods
		{
			get
			{
				if (_roodsList == null)
				{
					_roodsList = new List<Roods>
					{
						new Roods { DisplayValue="0 Roods", RoodsValue=0},
						new Roods { DisplayValue="1 Roods", RoodsValue=1.0 },
						new Roods { DisplayValue="2 Roods", RoodsValue=2.0 },
					    new Roods { DisplayValue="3 Roods", RoodsValue=3.0 },
					};
				}

				return _roodsList;
			}
		}

		public Roods()
        {
        }
    }
}
