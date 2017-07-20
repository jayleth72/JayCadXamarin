using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
    public class DecimalAngleConversionViewModel:BaseViewModel
    {
		private string _degreesInput;                          // Binding to stepper for degrees user input. 
        private string _minutesInput;                          // Binding to stepper for minutes user input.
        private string _secondsInput;                          // Binding to stepper for minutes user input.
        private string _decimalConversionResult;               // Result of decimal conversion.
        private int _degreesIntegerInput;                      // Holds Degrees Input in Integer format and used for data entry errors.
        private int _minutesIntegerInput;                      // Holds Minutes Input in Integer format and used for data entry errors.
        private double _secondsDoubleInput;                    // Holds Seconds Input in Double format and used for data entry errors.

		/// <summary>
		/// Gets or sets the degrees input from the user.
		/// </summary>
		/// <value>The degrees input.</value>
		public string DegreesInput
		{
			get { return _degreesInput; }
			set { SetValue(ref _degreesInput, value); }
		}

		/// <summary>
		/// Gets or sets the minutess from the user.
		/// </summary>
		/// <value>The minutess input.</value>
		public string MinutesInput
		{
			get { return _minutesInput; }
			set { SetValue(ref _minutesInput, value); }
		}

		/// <summary>
		/// Gets or sets the minutess input from the user.
		/// </summary>
		/// <value>The minutess input.</value>
		public string SecondsInput
		{
			get { return _secondsInput; }
			set { SetValue(ref _secondsInput, value); }
		}

		/// <summary>
		/// Gets or sets the minutess input from the user.
		/// </summary>
		/// <value>The minutess input.</value>
		public string DecimalConversionResult
		{
			get { return _decimalConversionResult; }
			set { SetValue(ref _decimalConversionResult, value); }
		}

        public ICommand ResetDegreesCommand { get; private set; }
		public ICommand ResetMinutesCommand { get; private set; }
		public ICommand ResetSecondsCommand { get; private set; }
		public ICommand ClearResultCommand { get; private set; }
		public ICommand ConvertToDecimalCommand { get; private set; }
		

		public DecimalAngleConversionViewModel(IPageService pageService) : base(pageService)
        {
            ResetDegreesCommand = new Command(ResetDegreesStepper);                                    // Reset Degrees Stepper
            ResetMinutesCommand = new Command(ResetMinutesStepper);                                    // Reset Minutes Stepper
            ResetSecondsCommand = new Command(ResetSecondsStepper);                                    // Reset Seconds Stepper
            ClearResultCommand = new Command(ClearConversionResult);                                   // Clear Result of conversionn field
			ConvertToDecimalCommand = new Command(ConvertToDecimal);                                   // Clear Result of conversionn field

			// Get rounding for Conversion Results display
			RetrieveResultRounding("DecimalAngleConversion");
        }

        // Button Methods
        private void ResetDegreesStepper()
        {
            _degreesInput = "";
            OnPropertyChanged(DegreesInput);
        }

		private void ResetMinutesStepper()
		{
			_minutesInput = "";

            OnPropertyChanged(MinutesInput);
		}
		
        private void ResetSecondsStepper()
		{
			_secondsInput = "";

			OnPropertyChanged(SecondsInput);
		}

        private void ClearConversionResult()
        {
            _decimalConversionResult = "";

            OnPropertyChanged(DecimalConversionResult);
        }

        private async void ConvertToDecimal()
        {
             ClearConversionResult();

	         if (NoDataEntered())
	         {
		        await _pageService.DisplayAlert("Data Input Error", "Nothing to convert, please enter some data", "Ok");
	             return;
	         }

	         // Check Degrees Field is a integer if the field is not empty
	         if (!(Int32.TryParse(_degreesInput, out _degreesIntegerInput)) && !(String.IsNullOrEmpty(_degreesInput)))
	         {
	            await _pageService.DisplayAlert("Data Input Error", "Please enter Integer numerical data (No decimals) in Degrees Field", "Ok");
		        return;
	         }
            else

            // Check Minutes Field is a integer if the field is not empty
            if (!(Int32.TryParse(_minutesInput, out _minutesIntegerInput)) && !(String.IsNullOrEmpty(_minutesInput)))
            {
            	await _pageService.DisplayAlert("Data Input Error", "Please enter Integer numerical data (No decimals) in Minutes Field", "Ok");
            	return;
            }

            // Check Seconds Field is Numerical (Doubles Allowed) if the field is not empty
            if (!(Double.TryParse(_secondsInput, out _secondsDoubleInput)) && !(String.IsNullOrEmpty(_secondsInput)))
            {
            	await _pageService.DisplayAlert("Data Input Error", "Please enter numerical data in Seconds Field", "Ok");
            	return;
            }

  			// Check that minutes numbers are in range
			if (NumberOutOfRange(59, 0, _minutesIntegerInput))
			{
				await _pageService.DisplayAlert("Data Input Error", "Minutes need to be a Numerical value between 0 and 59", "Ok");
				return;
			}

            // Check that seconds numbers are in range
            if (NumberOutOfRange(59, 0, ((int)_secondsDoubleInput)))
            {
                await _pageService.DisplayAlert("Data Input Error", "Seconds need to be a Numerical value between 0 and 59", "Ok");
                return;
            }

            // Data entered and in correct format and in specified ranges if we get to here
            // Assign zero to empty fields
            _degreesIntegerInput = String.IsNullOrEmpty(_degreesInput) ? 0 : _degreesIntegerInput;
            _minutesIntegerInput = String.IsNullOrEmpty(_minutesInput) ? 0 : _minutesIntegerInput;
            _secondsDoubleInput = String.IsNullOrEmpty(_secondsInput) ? 0 : _secondsDoubleInput;

            // Converts Deg Min Second to decimal degrees
            // Allow for negative values
            Double result;
            if (_degreesIntegerInput >= 0)
               result= (double)_degreesIntegerInput + ((double)_minutesIntegerInput / 60) + (_secondsDoubleInput / 3600);
            else
               result = (double)_degreesIntegerInput - ((double)_minutesIntegerInput / 60) - (_secondsDoubleInput / 3600);

			// Round to input or default specified rounding 
			result = Math.Round(result, _conversionRounding, MidpointRounding.AwayFromZero);

            _decimalConversionResult = result.ToString();

            OnPropertyChanged(DecimalConversionResult);

		}

	    /// <summary>
        /// Test if any data entered by user.
        /// </summary>
        /// <returns><c>true</c>, if data entered was noed, <c>false</c> otherwise.</returns>
        private bool NoDataEntered()
        {
            if (String.IsNullOrEmpty(_degreesInput) && String.IsNullOrEmpty(_minutesInput) && String.IsNullOrEmpty(_secondsInput))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Only Integer values should be entered for Degrees and Minutes.
        /// </summary>
        /// <returns><c>True</c>, if Integer values entered for Degrees and Minutes Fields, <c>false</c> otherwise.</returns>
        private bool DataFormatError(string input)
        {
           return !(Int32.TryParse(input, out _degreesIntegerInput));
        }
               
    }
}
