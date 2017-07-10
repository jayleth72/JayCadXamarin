using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
    public class DecimalAngleConversionViewModel:BaseViewModel
    {
		private readonly IPageService _pageService;            // This is here to enable Page Navigation and DispalyAlerts.
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
		public ICommand BackToPreviousPageCommand { get; private set; }
		public ICommand BackToMainMenuCommand { get; private set; }


		public DecimalAngleConversionViewModel(IPageService pageService)
        {
            ResetDegreesCommand = new Command(ResetDegreesStepper);                                    // Reset Degrees Stepper
            ResetMinutesCommand = new Command(ResetMinutesStepper);                                    // Reset Minutes Stepper
            ResetSecondsCommand = new Command(ResetSecondsStepper);                                    // Reset Seconds Stepper
            ClearResultCommand = new Command(ClearConversionResult);                                   // Clear Result of conversionn field
			ConvertToDecimalCommand = new Command(ConvertToDecimal);                                   // Clear Result of conversionn field
			BackToPreviousPageCommand = new Command(async () => await BackToPreviousPage());    // Navigation Back Buttom
            BackToMainMenuCommand = new Command(async () => await BackToMainMenu());            // Navigation for Button to Main Menu

			// Enable navigation from View Model
			_pageService = pageService;


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

            OnPropertyChanged();
        }

        async private void ConvertToDecimal()
        {
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

            if (!(Int32.TryParse(_minutesInput, out _minutesIntegerInput)) && !(String.IsNullOrEmpty(_minutesInput)))
			{
				await _pageService.DisplayAlert("Data Input Error", "Please enter Integer numerical data (No decimals) in Minutes Field", "Ok");
				return;
			}

            if (!(Double.TryParse(_secondsInput, out _secondsDoubleInput)) && !(String.IsNullOrEmpty(_secondsInput)))
			{
				await _pageService.DisplayAlert("Data Input Error", "Please enter numerical data in Seconds Field", "Ok");
				return;
			}

		}

		private async Task BackToPreviousPage()
		{
			await _pageService.PopAsync();
		}

		private async Task BackToMainMenu()
		{
			await _pageService.PopToRootAsync();
		}

        private bool NoDataEntered()
        {
            if (String.IsNullOrEmpty(_degreesInput) && String.IsNullOrEmpty(_minutesInput) && String.IsNullOrEmpty(_secondsInput))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Only Integer values should be entered for Degrees and Minutes
        /// </summary>
        /// <returns><c>True</c>, if Integer values entered for Degrees and Minutes Fields, <c>false</c> otherwise.</returns>
        private bool DataFormatError(string input)
        {
           return !(Int32.TryParse(input, out _degreesIntegerInput));
        }
    }
}
