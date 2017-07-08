﻿﻿﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JayCadSurveyXamarin.Model;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JayCadSurveyXamarin.ViewModel
{
    public class LengthConversionViewModel: INotifyPropertyChanged
    {
        private readonly IPageService _pageService;
        private LengthConversion _selectedConversion;
        private string _conversionResult = "";              // Result from a user selected conversion
        private string _convertFromUserInput = "";          // User entered value to be converted
        private string _userInputPlaceholder;               // Placeholder for userInput value to be converted
        private bool _isFeetPickersVisible;                 // Visibility modifier for Inches and FractionInches pickers
        private int _feetInput = 0;                         // Variable to hold value of user input value when converting from feet to metres
        private double _numericalDoubleInput = 0.0;         // Variable to hold value of user input value when converting from other conversions

        /// <summary>
        /// Gets or sets the selected length conversion from the Conversion Picker on the LengthConversion View.
        /// </summary>
        /// <value>The selected length conversion. For examle Metres to Feet</value>
		public LengthConversion SelectedLengthConversion
		{
			get
			{
				return _selectedConversion;
			}
			set
			{
				if (_selectedConversion != value)
				{
					_selectedConversion = value;
					OnPropertyChanged();
					setFeetPickersVisibility();
				}
			}
		}

        /// <summary>
        /// Gets or sets the conversion result for the Selected Conversion.
        /// </summary>
        /// <value>The conversion result.</value>
		public string ConversionResult
		{
			get
			{
				return _conversionResult;
			}
			set
			{
				if (_conversionResult != value)
				{
					_conversionResult = value;
					OnPropertyChanged();
				}
			}
		}

        /// <summary>
        /// Gets the User enterded value that is to be converted.
        /// </summary>
        /// <value>The convert from user input.</value>
		public string ConvertFromUserInput
		{
			get
			{
				return _convertFromUserInput;
			}
			set
			{
				if (_convertFromUserInput != value)
				{
					_convertFromUserInput = value;
					OnPropertyChanged();
				}
			}
		}

        /// <summary>
        /// Gets or sets the user input placeholder.
        /// </summary>
        /// <value>The user input placeholder.</value>
		public string UserInputPlaceholder
		{

			get
			{
				return _userInputPlaceholder;
			}
			set
			{
				if (_userInputPlaceholder != value)
				{

					_userInputPlaceholder = value;
					OnPropertyChanged();

				}
			}
		}

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:JayCadSurveyXamarin.ViewModel.LengthConversionViewModel"/> is feet pickers visible.
        /// </summary>
        /// <value><c>true</c> if is feet pickers visible; otherwise, <c>false</c>.</value>
		public bool IsFeetPickersVisible
		{
			get
			{
				return _isFeetPickersVisible;
			}
			set
			{
				_isFeetPickersVisible = value;
				OnPropertyChanged();
			}
		}

       	public ICommand ClearInputFieldCommand { get; private set; }
        public ICommand ClearResultFieldCommand { get; private set; }
		public ICommand ConvertUserInputCommand { get; private set; }
        public ICommand ClearStackCommand { get; private set; }
        public ICommand ShowStackCommand { get; private set; }
        public ICommand BackToPreviousPageCommand { get; private set; }
        public ICommand BackToMainMenuCommand { get; private set; }

		public LengthConversionViewModel(IPageService pageService)
        {
            // Initialis buttons in View
            ClearInputFieldCommand = new Command(ClearInputField);
            ClearResultFieldCommand = new Command(ClearResultField);
            ConvertUserInputCommand = new Command(ConvertUserInput);
            ClearStackCommand = new Command(ClearStack);
            ShowStackCommand = new Command(ShowStack);
            BackToPreviousPageCommand = new Command(async () => await BackToPreviousPage());    // Navigation Back Buttom
            BackToMainMenuCommand = new Command(async () => await BackToMainMenu());            // Navigation for Button to Main Menu

            // Enable navigation from View Model
            _pageService = pageService;
            			
        }
				
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
               
               
        private void ClearInputField()
        {
            _convertFromUserInput = "";
            OnPropertyChanged(ConvertFromUserInput);
        }

		private void ClearResultField()
		{
			_conversionResult = "";
			OnPropertyChanged(ConversionResult);
		}
          

        private async void ConvertUserInput()
        {
            string userInput = this.ConvertFromUserInput;
            string result = "";
           
            // Check if they have entered anything at all
            if (userInput.Equals(null) || userInput.Length == 0)
            {
                // No data entered display error message
                await _pageService.DisplayAlert("Input Error", "No data entered, please enter numerical value", "ok"); 
                return;
            }

			// check for valid numerical input
            if (!InputCheckValid(userInput))
            {
                // Check if user input is valid for Feet to Metres to conversion - only enter integer values and no-decimal values
                if (SelectedLengthConversion.conversionType == LengthConversion.CONVERSION_TYPE.FEET_TO_METRES)
                    await _pageService.DisplayAlert("Input Error", "You must enter an integer value with no decimals", "ok");
                else
                    await _pageService.DisplayAlert("Input Error", "You must enter a numerical value", "ok");
                
                return;
			}

            if (SelectedLengthConversion.conversionType == LengthConversion.CONVERSION_TYPE.FEET_TO_METRES)
            {
                result = (CalculateDecimalFeet() * SelectedLengthConversion.ConversionFactor).ToString();
            }
            else
            {
                result = ((_numericalDoubleInput * SelectedLengthConversion.ConversionFactor).ToString()) + " " + SelectedLengthConversion.ConvertTo; 

            }
                
            _conversionResult = "shit";
			// await _pageService.DisplayAlert("Input succes", _conversionResult, "ok");
            OnPropertyChanged(ConvertFromUserInput);

		}
        		
		private void ClearStack()
		{

		}
        	

		private void ShowStack()
		{

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
        /// Sets the feet pickers visibility.  Visibility is true when converting from feet to metres otherwise false.
        /// </summary>
		private void setFeetPickersVisibility()
		{
			if (SelectedLengthConversion.conversionType == LengthConversion.CONVERSION_TYPE.FEET_TO_METRES)
				IsFeetPickersVisible = true;
			else
				IsFeetPickersVisible = false;

		}

        private bool InputCheckValid(string input)
        {
            bool isValid = false;
           
            // Check for Selected Conversion
            if (SelectedLengthConversion.conversionType == LengthConversion.CONVERSION_TYPE.FEET_TO_METRES)
            {
                // Input for Feet to Metres should be an int (no decimal place etc)
                if (Int32.TryParse(input, out _feetInput))
                {
                    isValid = true;
                }    
            }    
            else
            {
                if (Double.TryParse(input, out _numericalDoubleInput))
                {
                    isValid = true;
                }    
            }  

            return isValid;
        }

        private double CalculateDecimalFeet()
        {
            return 0.0;
        }
    }   

}
