using System;
namespace JayCadSurveyXamarin.Model
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:JayCadSurveyXamarin.Model.Angle"/> class.
    /// Class used to represent an Angle Object.
    /// </summary>
    public class Angle
    {
        private int _degrees = 0;
		private int _minutes = 0;
		private int _seconds = 0;
		private double _decimalSeconds = 0.0;
		private double _decimalAngle = 0.0;

        public int Minutes { get => _minutes; set => _minutes = value; }
        public int Degrees { get => _degrees; set => _degrees = value; }
        public double DecimalSeconds { get => _decimalSeconds; set => _decimalSeconds = value; }
        public double DecimalAngle { get => _decimalAngle; set => _decimalAngle = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:JayCadSurveyXamarin.Model.Angle"/> class.
        /// </summary>
        /// <param name="deg">Deg.</param>
        /// <param name="min">Minutes.</param>
        /// <param name="secs">Secs.</param>
        public Angle (int deg, int min, int secs)
        {
            _degrees = deg;
            _minutes = min;
            _seconds = secs;

            // calculate Decimal Angle
            ConvertDegMinSecToDecimal();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:JayCadSurveyXamarin.Model.Angle"/> class.
        /// </summary>
        /// <param name="decimalAngle">Decimal angle.</param>
        public Angle (double decimalAngle)
        {
            // Initalise degrees, minutes and seconds according to decimal degrees conversion
            ConvertDecimalToDegMinSec();
        }

		/// <summary>
		/// Converts Deg Min Second to decimal degrees
		/// </summary>
		private void ConvertDegMinSecToDecimal()
		{
		
			_decimalAngle = _degrees + ((double)_minutes / 60) + ((double)_seconds / 3600);
			
		}

        /// <summary>
        /// Converts the decimal angle to degrees, minutes & seconds version.
        /// </summary>
		private void ConvertDecimalToDegMinSec()
		{
            // Get the whole degrees value from the decimal value
			_degrees = (int)_decimalAngle;


			// Get the whole minutes value from the decimal value
			double calcMinutesValue = ((_decimalAngle - (double)_degrees) * 60);
			_minutes = (int)calcMinutesValue;

			// Calculate seconds
			_decimalSeconds = ((calcMinutesValue - (double)_minutes) * 60);
			
			// round to nearest whole number
			_seconds = (int)Math.Round(_decimalSeconds);
		}

        /// <summary>
        /// Adds the angle to another angle object's angle data.
        /// </summary>
        /// <returns>The angle.</returns>
        /// <param name="otherAngle">Other angle.</param>
		public String AddAngle(Angle otherAngle)
		{
			// Add this objects angle data to the one passed in as a parameter

			_decimalAngle = _decimalAngle + otherAngle.DecimalAngle;
			ConvertDecimalToDegMinSec();

            // u00B0 is Hex for degrees symbol
			return _degrees + "\u00B0 " + Math.Abs(_minutes) + "' " + Math.Abs(_seconds) + "\"";
		}

		/// <summary>
		/// Subtract the angle to another angle object's angle data.
		/// </summary>
		/// <returns>The angle.</returns>
		/// <param name="otherAngle">Other angle.</param>
		public String SubtractAngle(Angle otherAngle)
		{
			// Add this objects angle data to the one passed in as a parameter

			_decimalAngle = _decimalAngle - otherAngle.DecimalAngle;
			ConvertDecimalToDegMinSec();

			// u00B0 is Hex for degrees symbol
			return _degrees + "\u00B0 " + Math.Abs(_minutes) + "' " + Math.Abs(_seconds) + "\"";
		}

	}
}
