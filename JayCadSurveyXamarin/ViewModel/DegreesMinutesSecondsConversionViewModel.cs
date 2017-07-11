using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
    public class DegreesMinutesSecondsConversionViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;         // This is here to enable Page Navigation and DispalyAlerts.
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

		public DegreesMinutesSecondsConversionViewModel(IPageService pageService)
        {
            ClearInputCommand = new Command(ClearInput);                                        // Clear User Input
            ConvertResultCommand = new Command(ConvertResult);                                  // Convert User Input to Degrees, Minutes and Seconds
            ClearResultCommand = new Command(ClearResult);                                    // Clear Conversion Results
			BackToPreviousPageCommand = new Command(async () => await BackToPreviousPage());    // Navigation Back Buttom
			BackToMainMenuCommand = new Command(async () => await BackToMainMenu());            // Navigation for Button to Main Menu

			// Enable navigation from View Model
			_pageService = pageService;

		}

        // Buttons on View
        public ICommand ClearInputCommand{ get; private set; }
        public ICommand ConvertResultCommand { get; private set; }
        public ICommand ClearResultCommand { get; private set; }
		public ICommand BackToPreviousPageCommand { get; private set; }
		public ICommand BackToMainMenuCommand { get; private set; }

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
		}

        /// <summary>
        /// Clears the result.
        /// </summary>
		private void ClearResult()
		{
            _conversionResult = "";
            OnPropertyChanged(ConversionResult);
		}

		private async Task BackToPreviousPage()
		{
			await _pageService.PopAsync();
		}

		private async Task BackToMainMenu()
		{
			await _pageService.PopToRootAsync();
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
    }
}
