using System;
using System.Collections.Generic;

namespace JayCadSurveyXamarin.Model
{
    public class LengthConversion
    {
        public string ConvertFrom { get; set; }
		public string ConvertTo { get; set; }
        public string ConvertName { get; set; }
        public double ConversionFactor { get; set; }

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
