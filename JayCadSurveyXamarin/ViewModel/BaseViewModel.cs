﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SQLite;
using JayCadSurveyXamarin.Persistence;
using JayCadSurveyXamarin.Model;
using System.Collections.ObjectModel;

namespace JayCadSurveyXamarin.ViewModel
{

	public class BaseViewModel : INotifyPropertyChanged
	{
		protected readonly IPageService _pageService;                           // This is here to enable Page Navigation and DispalyAlert.
		protected int _conversionRounding;                                      // Rounding for conversion results stored in SQLite db.  This is changed via a picker via Settings/Roundings2Page.  
        protected SQLiteAsyncConnection _connection;

		protected enum INPUT_VALIDATION_FLAG                                    // Used to indicate error status of input.
		{
			NO_INPUT_ENTERED,
			NON_NUMERICAL_DATA_ENTERED,
			NUMBER_OUT_OF_RANGE,
			NUMBER_OUT_OF_RANGE_DEGREES,
			NUMBER_OUT_OF_RANGE_MINUTES,
			NUMBER_OUT_OF_RANGE_SECONDS,
			INPUT_OK
		}

		protected enum INPUT_FIELD     // Used to specify the Input Entry Field                        
		{
			DEGREES,
			MINUTES,
			SECONDS
		}

		public event PropertyChangedEventHandler PropertyChanged;

		// View Buttons
		public ICommand BackToPreviousPageCommand { get; private set; }     // Enable Back navigation on pages
		public ICommand BackToMainMenuCommand { get; private set; }         // Enable Main menu navigation on pages
		

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingField, value))
				return;

			backingField = value;

			OnPropertyChanged(propertyName);
		}

		
		public BaseViewModel(IPageService pageService)
		{
			BackToPreviousPageCommand = new Command(async () => await BackToPreviousPage());    // Navigation Back Buttom
			BackToMainMenuCommand = new Command(async () => await BackToMainMenu());            // Navigation for Button to Main Menu

			_pageService = pageService;                                                         // Enable navigation and DisplayAlerts from View Model

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
		}

		protected async Task BackToPreviousPage()
		{
			await _pageService.PopAsync();
		}

		protected async Task BackToMainMenu()
		{
			await _pageService.PopToRootAsync();
		}

		/// <summary>
		/// Check for null input entry.
		/// </summary>
		/// <returns><c>true</c>, if no data entered, <c>false</c> otherwise.</returns>
		/// <param name="input">Input.</param>
		protected bool NoDataEntered(string input)
		{
			if (String.IsNullOrEmpty(input))
				return true;
			else
				return false;
		}

		/// <summary>
		/// Check for Integer data entered in one field
		/// If valid numerical data is entered, private integer  variable is initialised  with valid integer.
		/// </summary>
		/// <returns><c>true</c>, if numerical data entered was noned, <c>false</c> otherwise.</returns>
		/// <param name="input">Input.</param>
		/// <param name="outputNum">Output number.</param>
		protected bool NonNumericalDataEntered(string input, ref int outputNum)
		{
			if (int.TryParse(input, out outputNum))
				return false;
			else
				return true;
		}

		/// <summary>
		/// Check for double data entered in one field
		/// If valid numerical data is entered, private integer  variable is initialised  with valid integer.
		/// </summary>
		/// <returns><c>true</c>, if numerical data entered was noned, <c>false</c> otherwise.</returns>
		/// <param name="input">Input.</param>
		/// <param name="outputNum">Output number.</param>
		protected bool NonNumericalDoubleDataEntered(string input, ref double outputNum)
		{
			if (double.TryParse(input, out outputNum))
				return false;
			else
				return true;
		}

		/// <summary>
		/// Test for input between max and min numbers
		/// </summary>
		/// <returns><c>true</c>, if input out of range, <c>false</c> otherwise.</returns>
		/// <param name="max">Max.</param>
		/// <param name="min">Minimum.</param>
		/// <param name="input">Input.</param>
		protected bool NumberOutOfRange(int max, int min, int input)
		{
			if (input < min || input > max)
				return true;
			else
				return false;
		}

		/// <summary>
		/// Checks the input for errors.
		/// Check for Non Numerical Data and Input out of range errors
		/// </summary>
		/// <returns>INPUT_VALIDATION_FLAG</returns>
		/// <param name="input">User Input as a string.</param>
		/// <param name="outputNum">Output number is the number that gets initialised if input is an integer.</param>
		/// <param name="minNum">Minimum number.</param>
		/// <param name="maxNum">Max number.</param>
		protected INPUT_VALIDATION_FLAG CheckInputForErrors(string input, ref int outputNum, int minNum, int maxNum, INPUT_FIELD inputField)
		{
			INPUT_VALIDATION_FLAG inputFlag = INPUT_VALIDATION_FLAG.INPUT_OK;

			// Check degrees1 if data has been entered
			if (!NoDataEntered(input))
			{
				if (NonNumericalDataEntered(input, ref outputNum))
				{
					inputFlag = INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;
				}
				else if (NumberOutOfRange(maxNum, minNum, outputNum))
				{
					if (inputField == INPUT_FIELD.DEGREES)
						inputFlag = INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_DEGREES;
					else if (inputField == INPUT_FIELD.MINUTES)
						inputFlag = INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_MINUTES;
					else
						inputFlag = INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_SECONDS;
				}

			}

			return inputFlag;
		}

		/// <summary>
		/// Retrieves the result rounding from SQLite db.  If none retrieved then rounding set to 0.
		/// </summary>
		/// <param name="roundingName">Rounding name.</param>
		async protected void RetrieveResultRounding(string roundingName)
		{
			RoundingForDisplay selectedRounding = null;

			// Get database connection
			var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
			await connection.CreateTableAsync<RoundingForDisplay>();

			// See if RoundingsForDisplay Table exists
			if (await connection.Table<RoundingForDisplay>().CountAsync() > 0)
			{
				selectedRounding = await connection.Table<RoundingForDisplay>().Where(rd => rd.RoundingName.Equals(roundingName)).FirstOrDefaultAsync();

				// see if rounding retrieved from SQLitedb
				if (selectedRounding != null)
					_conversionRounding = selectedRounding.RoundingValue;
				else
					_conversionRounding = 0;

			}
			else
			{
				// set rounding to default = 0
				_conversionRounding = 0;
			}

		}

        /// <summary>
        /// Adds the calculation to stack.
        /// </summary>
        /// <param name="convertFrom">Convert from.</param>
        /// <param name="convertTo">Convert to.</param>
        async protected void AddCalculationToStack(string convertFrom, string convertTo)
        {
            var convertCalculation = new ConversionCalculation
            {
                ConvertFrom = convertFrom,
                ConvertTo = convertTo
            };

			await _connection.CreateTableAsync<ConversionCalculation>();
            await _connection.InsertAsync(convertCalculation);
        }
	}

}