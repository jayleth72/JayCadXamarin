﻿﻿﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JayCadSurveyXamarin.Model;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace JayCadSurveyXamarin.ViewModel
{
    public class LengthConversionViewModel: BaseViewModel
    {
        private LengthConversion _selectedConversion;       // Selected Conversion from Conversion picker
        private FractionInch _selectedFractionInch;         // Selected Conversion from FractionInches picker
        private Inches _selectedInches;                     // Selected Inch from Inch picker
		private string _conversionResult = "";              // Result from a user selected conversion
        private string _convertFromUserInput = "";          // User entered value to be converted
        private string _userInputPlaceholder;               // Placeholder for userInput value to be converted
        private string _runningTotal;                       // Displays running total for conversions as a string
        private bool _isFeetPickersVisible;                 // Visibility modifier for Inches and FractionInches pickers
        private int _feetInput = 0;                         // Variable to hold value of user input value when converting from feet to metres
        private double _numericalDoubleInput = 0.0;         // Variable to hold value of user input value when converting from other conversions
        private double _runningTotalDouble = 0.0;           // Displays running total for conversions as a double
        private int _fractionInchPickerSelectedIndex;        
        private int _inchPickerSelectedIndex;
        private int _selectedLengthConversionIndex = 0;


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
					 SetFeetPickersVisibility();
                     ClearResultField();
                     ClearRunningTotalField();
                     ClearInputField();

                    // Also clear the stack
                    ClearStackCalculationsTable();
				}
			}
		}

		
		/// <summary>
		/// Gets or sets the selected fraction inches from the Fraction Inch Picker.
		/// </summary>
		/// <value>The selected inches.</value>
		public FractionInch SelectedFractionInch
		{
			get	{ return _selectedFractionInch; }
		    set { SetValue(ref _selectedFractionInch, value); }
		}

		public Inches SelectedInches
		{
			get	{ return _selectedInches; }
			set { SetValue(ref _selectedInches, value); }
		}

		/// <summary>
		/// Gets or sets the conversion result for the Selected Conversion.
		/// </summary>
		/// <value>The conversion result.</value>
		public string ConversionResult
		{
			get { return _conversionResult; }
			set { SetValue(ref _conversionResult, value); }
		}

        /// <summary>
        /// Gets the User enterded value that is to be converted.
        /// </summary>
        /// <value>The convert from user input.</value>
		public string ConvertFromUserInput
		{
			get	{ return _convertFromUserInput; }
			set { SetValue(ref _convertFromUserInput, value); }
		}

        /// <summary>
        /// Gets or sets the user input placeholder.
        /// </summary>
        /// <value>The user input placeholder.</value>
		public string UserInputPlaceholder
		{

			get { return _userInputPlaceholder; }
			set { SetValue(ref _userInputPlaceholder, value); }
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
				if (_isFeetPickersVisible != value)
				{
                    _isFeetPickersVisible = value;
					_fractionInchPickerSelectedIndex = 0;
					_inchPickerSelectedIndex = 0;
					OnPropertyChanged();

				}

			}
		}

		/// <summary>
        /// The running total of user entered conversions
        /// </summary>
        /// <value>The running total.</value>
        public string RunningTotal
		{

			get { return _runningTotal; }
			set { SetValue(ref _runningTotal, value); }
		}

		/// <summary>
        /// Gets or sets the index of the fraction inch picker.
        /// </summary>
        /// <value>The index of the fraction inch picker.</value>
		public int FractionInchPickerIndex
		{
			get	{ return _fractionInchPickerSelectedIndex; }
			set { SetValue(ref _fractionInchPickerSelectedIndex, value); }
		}

        /// <summary>
        /// Gets or sets the index of the inch picker.
        /// </summary>
        /// <value>The index of the inch picker.</value>
		public int InchPickerIndex
		{
			get { return _inchPickerSelectedIndex; }
			set { SetValue(ref _inchPickerSelectedIndex, value); }
		}

		/// <summary>
		/// Gets or sets the index of the Length Conversion Picker.
		/// </summary>
		/// <value>The index of the inch picker.</value>
		public int SelectedLengthConversionIndex
		{
			get { return _selectedLengthConversionIndex; }
			set { SetValue(ref _selectedLengthConversionIndex, value); }
		}
             
		
       	public ICommand ClearInputFieldCommand { get; private set; }
        public ICommand ClearResultFieldCommand { get; private set; }
		public ICommand ConvertUserInputCommand { get; private set; }
		public ICommand ClearStackCommand { get; private set; }
		public ICommand ShowStackCommand { get; private set; }

		public LengthConversionViewModel(IPageService pageService) : base(pageService)
        {
            // Initialis buttons in View
            ClearInputFieldCommand = new Command(ClearInputField);
            ClearResultFieldCommand = new Command(ClearResultField);
            ConvertUserInputCommand = new Command(ConvertUserInput);
            ClearStackCommand = new Command(ClearStack);
            ShowStackCommand = new Command(async () => await ShowStack()); ;

            _conversionResult = "Conversion Results";

            // Get rounding for Conversion Results display
            RetrieveResultRounding("LengthConversion");

        }

	
		private void ClearInputField()
		{
			_convertFromUserInput = "";
			_fractionInchPickerSelectedIndex = 0;
			_inchPickerSelectedIndex = 0;

			OnPropertyChanged(ConvertFromUserInput);

		}


        private void ClearResultField()
		{
			_conversionResult = "";
			OnPropertyChanged(ConversionResult);
		}

		private void ClearRunningTotalField()
		{
			_runningTotal = "";
            _runningTotalDouble = 0.0;
			OnPropertyChanged(RunningTotal);
		}
          
        /// <summary>
        /// 1.  Checks data has been entered - display alert if no data entered.
        /// 2.  Check numerical input entered, also check integer value entered for feet - display alert for string values.
        /// 3.  Perform conversion and show in results and running total field
        /// </summary>
        private async void ConvertUserInput()
        {
            string userInput = this.ConvertFromUserInput;
            double result = 0.0;

            // Check user has enterd a converion
            if (_selectedLengthConversionIndex < 0)
            {
                await _pageService.DisplayAlert("Selection Error", "No conversion chosen.  Please choose a conversion", "ok");
                return; 
            }    
               

			// Check if they have entered anything at all
			// No entry is allowed for Feet to Metres conversion as user may only select inches or fraction  Inches to convert
			if (userInput.Equals(null) || (userInput.Length == 0 && SelectedLengthConversion.conversionType != LengthConversion.CONVERSION_TYPE.FEET_TO_METRES))
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
                _numericalDoubleInput = CalculateDecimalFeet();
            }
           
            result = _numericalDoubleInput * SelectedLengthConversion.ConversionFactor; 

           
            ClearResultField();  // This is here for the Conversion to show in the result field ??? 

            // Round to input or default specified rounding 
            result = Math.Round(result, _conversionRounding, MidpointRounding.AwayFromZero);

			_conversionResult = result.ToString() + " " + SelectedLengthConversion.ConvertTo;


			// Add Calculation to stack
			AddCalculationToStack(ConversionCalculationDisplay(result), result, _numericalDoubleInput,
                                  GetAbbreviation(SelectedLengthConversion.ConvertTo), GetAbbreviation(SelectedLengthConversion.ConvertFrom));

			// calculate and show running total
			_runningTotal = CalculateRunningTotal(result) + " " + SelectedLengthConversion.ConvertTo;

            OnPropertyChanged(ConversionResult);
		}
       

        /// <summary>
        /// Returns the input and converted output to a string for display in the Conversion stack
        /// </summary>
        /// <returns>The calculation display.</returns>
        private string ConversionCalculationDisplay(Double conversionResult)
        {
            string calculation = "";
            double userInput = 0.0;

            // Input and Result Output are rounded to User specified roundings
            if (SelectedLengthConversion.conversionType == LengthConversion.CONVERSION_TYPE.FEET_TO_METRES)
            {
                userInput = Math.Round(CalculateDecimalFeet(), _conversionRounding, MidpointRounding.AwayFromZero);
                calculation = userInput.ToString() + "ft = " + _conversionResult + "m";
            }
            else
            {
                userInput = Math.Round(_numericalDoubleInput, _conversionRounding, MidpointRounding.AwayFromZero);
                calculation = userInput.ToString() + GetAbbreviation(SelectedLengthConversion.ConvertFrom) + " = "
                                       + conversionResult.ToString() + GetAbbreviation(SelectedLengthConversion.ConvertTo);
            }

            return calculation;
        }

		private void ClearStack()
		{
            ClearStackCalculationsTable();
		}
        	

		protected async Task ShowStack()
		{
            await _pageService.PushAsync(new ContentPages.ShowConversionStackPage());
		}
            
        /// <summary>
        /// Sets the feet pickers visibility.  Visibility is true when converting from feet to metres otherwise false.
        /// </summary>
		private void SetFeetPickersVisibility()
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
                // allow no input when converting feet to metres as user may just select inches or fractions
                if (input.Length == 0)
                    input = "0";
                
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
            return Convert.ToDouble(_feetInput) + (SelectedInches.InchValue * 1 / 12) + (SelectedFractionInch.FractionInchValue * 1 / 192) ;
        }

        private string CalculateRunningTotal(double result)
        {
            
            _runningTotalDouble += result;
            return _runningTotalDouble.ToString();
        }
    }   

}
