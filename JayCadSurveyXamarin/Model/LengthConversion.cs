using System;
using System.Collections.Generic;

namespace JayCadSurveyXamarin.Model
{
	/// <summary>
	/// Length conversion.
	/// Contains a list for use in the ConvertToUnit Picker in the LengthConversionPage
    /// List contains conversions with display names and conversion factors 
	/// </summary>
	public class LengthConversion
    {
        public string ConvertFrom { get; set; }         // The Unit the user wants to convert
		public string ConvertTo { get; set; }           // The Unit the user wants tp convert to
        public string ConvertName { get; set; }         // The Name of the Unit
        public double ConversionFactor { get; set; }    // Numeric conversion factor

        static List<LengthConversion> _conversionList;
        public static List<LengthConversion> AllLengthConversions
        {
            get
            {
                if (_conversionList == null)
                {
                    _conversionList = new List<LengthConversion>
                    {
                        new LengthConversion { ConvertFrom="Metres", ConvertTo="Feet", ConvertName="Metres to Feet", ConversionFactor=3.28084 },
                        new LengthConversion { ConvertFrom="Feet", ConvertTo="Metres", ConvertName="Feet to Metres", ConversionFactor=0.3048 },
                        new LengthConversion { ConvertFrom="Metres", ConvertTo="Links", ConvertName="Metres to Links", ConversionFactor=4.970969538 },
                        new LengthConversion { ConvertFrom="Links", ConvertTo="Metres", ConvertName="Links to Metres", ConversionFactor=0.201168 }
                    };    
                }

                return _conversionList;
            }
        }

        public LengthConversion()
        {
        }
    }
}
