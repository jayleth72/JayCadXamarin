using System;
using System.Collections.Generic;
using SQLite;

namespace JayCadSurveyXamarin.Model
{
    /// <summary>
    /// This is used to represent a result of a conversion (Area and length).
    /// This is used to show a stack of conversions
    /// </summary>
    public class ConversionCalculation
    {

        [PrimaryKey, AutoIncrement]
        public int CalculationId
        {
            get;
            set;
        }

       public string ConversionDisplayValue
        {
            get;
            set;
        }
              
        public double ConversiontToValue
        {
            get;
            set;
        }
    }
}
