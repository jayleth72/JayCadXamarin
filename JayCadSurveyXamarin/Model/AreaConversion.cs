using System;
using System.Collections.Generic;

namespace JayCadSurveyXamarin.Model
{
    public class AreaConversion
    {
		public int id { get; set; }
		public string ConvertFrom { get; set; }         // The Unit the user wants to convert
		public string ConvertTo { get; set; }           // The Unit the user wants tp convert to
		public string ConvertName { get; set; }         // The Name of the Unit
		public double ConversionFactor { get; set; }    // Numeric conversion factor

		public enum CONVERSION_TYPE
		{
			HECTARES_TO_ACRES,
			ACRES_TO_HECTARES,
		}

		public CONVERSION_TYPE conversionType { get; set; }

		static List<AreaConversion> _conversionList;
		public static List<AreaConversion> AllAreaConversions
		{
			get
			{
				if (_conversionList == null)
				{
                    _conversionList = new List<AreaConversion>
                    {
                        new AreaConversion { id =0, ConvertFrom="Hectares", ConvertTo="Acres", ConvertName="Hectares to Acres", conversionType=CONVERSION_TYPE.HECTARES_TO_ACRES, ConversionFactor=2.47105 },
                        new AreaConversion { id =1, ConvertFrom="Acres", ConvertTo="Hectares", ConvertName="Acres to Hectares", conversionType=CONVERSION_TYPE.ACRES_TO_HECTARES, ConversionFactor=0.404686 }
                    };	
				}

				return _conversionList;
			}
		}
    }
}
