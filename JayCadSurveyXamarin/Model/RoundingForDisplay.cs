using System;
using SQLite;

namespace JayCadSurveyXamarin.Model
{
    // Used to hold the selected rounding for display purposes
    public class RoundingForDisplay
    {
        public int RoundingId
        {
            get;
            set;
        }

		public int RoundingName
		{
			get;
			set;
		}

        public int Rounding
        {
            get;
            set;
        }
    }
}
