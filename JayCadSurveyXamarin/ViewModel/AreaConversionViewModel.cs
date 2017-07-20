﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JayCadSurveyXamarin.Model;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JayCadSurveyXamarin.ViewModel
{
    public class AreaConversionViewModel: BaseViewModel
    {
		private AreaConversion _selectedConversion;         // Selected Conversion from Conversion picker
		private Roods _selectedRood;                        // Selected Conversion from Roods picker
		private Perches _selectedPerches;                   // Selected Perch from Perch picker
		private string _conversionResult = "";              // Result from a user selected conversion
		private string _convertFromUserInput = "";          // User entered value to be converted
		private string _userInputPlaceholder;               // Placeholder for userInput value to be converted
		private bool _isAcresPickersVisible;                // Visibility modifier for Perches and Roods pickers
		private int _acresInput = 0;                        // Variable to hold value of user input value when converting from Acres to Hectares
		private double _numericalDoubleInput = 0.0;         // Variable to hold value of user input value when converting from other conversions
		private int _PerchesPickerSelectedIndex;
		private int _roodsPickerSelectedIndex;
		private int _selectedAreaConversionIndex = -1;

		/// <summary>
		/// Gets or sets the selected length conversion from the Conversion Picker on the AreaConversion View.
		/// </summary>
		/// <value>The selected length conversion. For examle Metres to Feet</value>
		public AreaConversion SelectedAreaConversion
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
					SetAcresPickersVisibility();
					ClearResultField();
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
		public Roods SelectedRoods
		{
			get { return _selectedRood; }
			set { SetValue(ref _selectedRood, value); }
		}

        public Perches SelectedPerches
		{
			get { return _selectedPerches; }
			set { SetValue(ref _selectedPerches, value); }
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
			get { return _convertFromUserInput; }
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
		/// <see cref="T:JayCadSurveyXamarin.ViewModel.AreaConversionViewModel"/> is feet pickers visible.
		/// </summary>
		/// <value><c>true</c> if is feet pickers visible; otherwise, <c>false</c>.</value>
		public bool IsAcresPickersVisible
		{
			get
			{
				return _isAcresPickersVisible;
			}
			set
			{
				if (_isAcresPickersVisible != value)
				{
					_isAcresPickersVisible = value;
					_PerchesPickerSelectedIndex = 0;
					_roodsPickerSelectedIndex = 0;
					OnPropertyChanged();

				}

			}
		}
        		
		/// <summary>
		/// Gets or sets the index of the fraction inch picker.
		/// </summary>
		/// <value>The index of the fraction inch picker.</value>
		public int PerchesPickerIndex
		{
			get { return _PerchesPickerSelectedIndex; }
			set { SetValue(ref _PerchesPickerSelectedIndex, value); }
		}

		/// <summary>
		/// Gets or sets the index of the inch picker.
		/// </summary>
		/// <value>The index of the inch picker.</value>
		public int RoodsPickerIndex
		{
			get { return _roodsPickerSelectedIndex; }
			set { SetValue(ref _roodsPickerSelectedIndex, value); }
		}

		/// <summary>
		/// Gets or sets the index of the Length Conversion Picker.
		/// </summary>
		/// <value>The index of the inch picker.</value>
		public int SelectedAreaConversionIndex
		{
			get { return _selectedAreaConversionIndex; }
			set { SetValue(ref _selectedAreaConversionIndex, value); }
		}


		public ICommand ClearInputFieldCommand { get; private set; }
		public ICommand ClearResultFieldCommand { get; private set; }
		public ICommand ConvertUserInputCommand { get; private set; }
		public ICommand ClearStackCommand { get; private set; }
		public ICommand ShowStackCommand { get; private set; }
	

		public AreaConversionViewModel(IPageService pageService) : base(pageService)
		{
			// Initialis buttons in View
			ClearInputFieldCommand = new Command(ClearInputField);
			ClearResultFieldCommand = new Command(ClearResultField);
			ConvertUserInputCommand = new Command(ConvertUserInput);
			ClearStackCommand = new Command(ClearStack);
			ShowStackCommand = new Command(async () => await ShowStack());

			_conversionResult = "Conversion Results";

			// Get rounding for Conversion Results display
			RetrieveResultRounding("AreaConversion");
		}

		private void ClearInputField()
		{
			_convertFromUserInput = "";
			_PerchesPickerSelectedIndex = 0;
			_roodsPickerSelectedIndex = 0;

			OnPropertyChanged(ConvertFromUserInput);

		}

		private void ClearResultField()
		{
			_conversionResult = "";
			OnPropertyChanged(ConversionResult);
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
			if (_selectedAreaConversionIndex < 0)
			{
				await _pageService.DisplayAlert("Selection Error", "No conversion chosen.  Please choose a conversion", "ok");
				return;
			}


			// Check if they have entered anything at all
			// No entry is allowed for Acres to Hectares conversion as user may only select Roods or Perches to convert
			if (userInput.Equals(null) || (userInput.Length == 0 && SelectedAreaConversion.conversionType != AreaConversion.CONVERSION_TYPE.ACRES_TO_HECTARES))
			{
				// No data entered display error message
				await _pageService.DisplayAlert("Input Error", "No data entered, please enter numerical value", "ok");
				return;
			}

			// check for valid numerical input
			if (!InputCheckValid(userInput))
			{
				// Check if user input is valid for Acres to Hectares conversion - only enter integer values and no-decimal values
				if (SelectedAreaConversion.conversionType == AreaConversion.CONVERSION_TYPE.ACRES_TO_HECTARES)
					await _pageService.DisplayAlert("Input Error", "You must enter an integer value with no decimals", "ok");
				else
					await _pageService.DisplayAlert("Input Error", "You must enter a numerical value", "ok");

				return;
			}

			if (SelectedAreaConversion.conversionType == AreaConversion.CONVERSION_TYPE.ACRES_TO_HECTARES)
            {
                _numericalDoubleInput = CalculateDecimalAcres();
            }    
			 			
			result = _numericalDoubleInput * SelectedAreaConversion.ConversionFactor;

            ClearResultField();  // This is here for the Conversion to show in the result field ??? 

			// Round to User specified rounding (Via Settings/roundings) or default rounding which is zero
			result = Math.Round(result, _conversionRounding, MidpointRounding.AwayFromZero);
			_numericalDoubleInput = Math.Round(_numericalDoubleInput, _conversionRounding, MidpointRounding.AwayFromZero);

			_conversionResult = result.ToString() + " " + SelectedAreaConversion.ConvertTo;

			// Add Calculation to stack
			AddCalculationToStack(ConversionCalculationDisplay(result), result, _numericalDoubleInput,
								  GetAbbreviation(SelectedAreaConversion.ConvertTo), GetAbbreviation(SelectedAreaConversion.ConvertFrom));
            			
			OnPropertyChanged();
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
			userInput = Math.Round(_numericalDoubleInput, _conversionRounding, MidpointRounding.AwayFromZero);
			calculation = userInput.ToString() + GetAbbreviation(SelectedAreaConversion.ConvertFrom) + " = "
							   + conversionResult.ToString() + GetAbbreviation(SelectedAreaConversion.ConvertTo);
			
			return calculation;
		}


		private void ClearStack()
		{
            ClearStackCalculationsTable();
		}


		async private Task ShowStack()
		{
            await _pageService.PushAsync(new ContentPages.ShowConversionStackPage());
		}

		/// <summary>
		/// Sets the feet pickers visibility.  Visibility is true when converting from feet to metres otherwise false.
		/// </summary>
		private void SetAcresPickersVisibility()
		{
			if (SelectedAreaConversion.conversionType == AreaConversion.CONVERSION_TYPE.ACRES_TO_HECTARES)
				IsAcresPickersVisible = true;
			else
				IsAcresPickersVisible = false;

		}

		private bool InputCheckValid(string input)
		{
			bool isValid = false;

			// Check for Selected Conversion
			if (SelectedAreaConversion.conversionType == AreaConversion.CONVERSION_TYPE.ACRES_TO_HECTARES)
			{
				// allow no input when converting feet to metres as user may just select inches or fractions
				if (input.Length == 0)
					input = "0";

				// Input for Feet to Metres should be an int (no decimal place etc)
				if (Int32.TryParse(input, out _acresInput))
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

		private double CalculateDecimalAcres()
		{
			return Convert.ToDouble(_acresInput) + (SelectedPerches.perchesValue * 0.00625) + (SelectedRoods.RoodsValue * 0.249999998616);
		}

        /// <summary>
        /// Converts  decimal acres to acres,roods and perches.
        /// </summary>
        /// <returns>The decimal acres to acres roods perches.</returns>
        /// <param name="input">Input.</param>
        private string ConvertDecimalAcresToAcresRoodsPerches(double input)
        {
			double fractionalPart = 0.0;
			double integralPart = 0.0;
			int acresPart = 0;
			int roodPart = 0;
			double perchesPart = 0;
			double calc = 0.0;
			string formattedAcresMeasure = "";

			fractionalPart = input % 1;
			integralPart = input - fractionalPart;
			acresPart = (int)integralPart;

			// Get roods
			calc = fractionalPart / (0.25);

			fractionalPart = calc % 1;
			integralPart = calc - fractionalPart;
			roodPart = (int)integralPart;

			// Get perches in decimal format
			calc = fractionalPart / 0.025;

			perchesPart = calc;

			if (acresPart == 0)
				formattedAcresMeasure = "0A ";
			else
				formattedAcresMeasure = acresPart + "A ";

			if (roodPart == 0)
				formattedAcresMeasure += " 0R ";
			else
				formattedAcresMeasure += roodPart + "R ";

			if (perchesPart > 0)
				formattedAcresMeasure += (Math.Round(perchesPart, 1, MidpointRounding.AwayFromZero)) + "P";

			return formattedAcresMeasure;
		
        }

    }
}
