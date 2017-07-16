using System;
using System.Collections.Generic;
using SQLite;

namespace JayCadSurveyXamarin.Model
{
    // Used to hold the selected rounding for display purposes
    public class RoundingForDisplay
    {
        [PrimaryKey]
        public int RoundingId
        {
            get;
            set;
        }

		public string RoundingName
		{
			get;
			set;
		}

        public int RoundingValue
        {
            get;
            set;
        }

	}
}
