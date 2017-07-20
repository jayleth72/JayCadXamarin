using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
    public class DegreesMinutesSecondsConversionViewModel : BaseViewModel
    {
        private string _decimalInput;                       // User Input. 
        private string _conversionResult;                   // calculated conversion result.
        private double _decimalDoubleInput;                 // User Input converted to double after check.
		/// <summary>
		/// Gets or sets the de input from the user.
		/// </summary>
		/// <value>The degrees input.</value>
		public string DecimalInput
		{
			get { return _decimalInput; }
			set { SetValue(ref _decimalInput, value); }
		}

		/// <summary>
		/// Gets or sets the de input from the user.
		/// </summary>
		/// <value>The degrees input.</value>
		public string ConversionResult
		{
			get { return _conversionResult; }
			set { SetValue(ref _conversionResult, value); }
		}

		public DegreesMinutesSecondsConversionViewModel(IPageService pageService) : base(pageService)
        {
            ClearInputCommand = new Command(ClearInput);                                        // Clear User Input
            ConvertResultCommand = new Command(ConvertResult);                                  // Convert User Input to Degrees, Minutes and Seconds
            ClearResultCommand = new Command(ClearResult);                                    // Clear Conversion Results
		
		}

        // Buttons on View
        public ICommand ClearInputCommand{ get; private set; }
        public ICommand ConvertResultCommand { get; private set; }
        public ICommand ClearResultCommand { get; private set; }
		

        /// <summary>
        /// Clears the input.
        /// </summary>
        private void ClearInput()
        {
          	_decimalInput = "";
			OnPropertyChanged(DecimalInput);    
        }

        /// <summary>
        /// Converts the result.
        /// </summary>
		private async void ConvertResult()
		{
            ClearResult();

            // Check for data entered 
            if (NoDataEntered())
            {
				await _pageService.DisplayAlert("Data Input Error", "Nothing to convert, please enter some data", "Ok");
				return;
            }

			// Check Seconds Field is Numerical (Doubles Allowed) if the field is not empty
			if (DataFormatError(_decimalInput))
			{
				await _pageService.DisplayAlert("Data Input Error", "Please enter numerical data in Decimal Degrees Field", "Ok");
				return;
			}

            _conversionResult = ConvertDecimalToDegMinSec(_decimalDoubleInput);

            OnPropertyChanged(ConversionResult);
		}

        /// <summary>
        /// Clears the result.
        /// </summary>
		private void ClearResult()
		{
            _conversionResult = "";
            OnPropertyChanged(ConversionResult);
		}
        		
		/// <summary>
		/// Test if any data entered by user.
		/// </summary>
		/// <returns><c>true</c>, if no data entered, <c>false</c> otherwise.</returns>
		private bool NoDataEntered()
		{
            if (String.IsNullOrEmpty(_decimalInput))
				return true;
			else
				return false;
		}

		/// <summary>
		/// Only Integer values should be entered for Degrees and Minutes.
		/// </summary>
		/// <returns><c>True</c>, if Numerical Double values entered for Decimal Fields, <c>false</c> otherwise.</returns>
		private bool DataFormatError(string input)
		{
			return !(Double.TryParse(input, out _decimalDoubleInput));
		}

        /// <summary>
        /// Converts the decimal degrees to degrees, minutes and second.
        /// </summary>
        /// <returns>Degrees, minutes & seconds as a string.</returns>
        /// <param name="input">Double User Input</param>
        private string ConvertDecimalToDegMinSec(double input)
        {
			// Get the whole degrees value from the decimal value
			int degrees = (int)input;

			// Get the whole minutes value from the decimal value
			double calcMinutesValue = ((input - (double)degrees) * 60);
			int minutes = (int)calcMinutesValue;

			// Calculate seconds
			double theSeconds = ((calcMinutesValue - (double)minutes) * 60);
            theSeconds = Math.Round(theSeconds, 1, MidpointRounding.AwayFromZero);
            //Round Seconds to one decimal plae

            return degrees.ToString() + "\u00B0 " + minutes.ToString() + "\' " + theSeconds.ToString() + "\"";
        }
    }
}
