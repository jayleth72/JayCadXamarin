using System;
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
            NO_DISTANCE_INPUT_ENTERED,
            NO_BEARING_DATA_ENTERED,
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
        public ICommand GoToCalculatorPageCommand { get; private set; }     // Enable Calculator navigation

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
            GoToCalculatorPageCommand = new Command(async () => await GoToCalculatorPage());    // Navigation for Calculator

			_pageService = pageService;                                                         // Enable navigation and DisplayAlerts from View Model

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

       
        protected virtual async Task BackToPreviousPage()
        {
            await _pageService.PopAsync();
        }

        protected virtual async Task BackToMainMenu()
        {
            await _pageService.PopToRootAsync();
        }

		protected virtual async Task GoToCalculatorPage()
		{
            await _pageService.PushAsync(new ContentPages.CalculatorPage());
		}

        #region Data Validation
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
        #endregion

        #region Roundings
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
        /// Rounds the result to specified number of decimal figures stored in SQLite database.
        /// For example if length conversion roundings is set to 5 decimal figures then 1234.55555 will be displayed.
        /// Also trims any trailing zeros, for example 150.12300 will be converted to 150.123
        /// </summary>
        /// <returns>The result formatted to specidfied number of figures after the decimal point</returns>
        /// <param name="result">Result.</param>
        protected string RoundDecimalFigures(double result)
        {
            string formattedResult;

            switch (_conversionRounding)
            {
                case 1:
                    formattedResult = string.Format("{0:0.#}", result);
                    break;
                case 2:
                    formattedResult = string.Format("{0:0.##}", result);
                    break;
                case 3:
                    formattedResult = string.Format("{0:0.###}", result);
                    break;
                case 4:
                    formattedResult = string.Format("{0:0.####}", result);
                    break;
                case 5:
                    formattedResult = string.Format("{0:0.#####}", result);
                    break;
                default:
                    formattedResult = string.Format("{0:0}", result);
                    break;
            }

            return formattedResult;
        }
        #endregion

        #region Stack methods
        /// <summary>
        /// Adds the calculation to stack.
        /// </summary>
        /// <param name="conversionDisplayValue">Displays Input dimensio and result of the Conversion.</param>
        /// <param name="convertToValue">Conversion result used to calculate a running total</param>
        async protected void AddCalculationToStack(string conversionDisplayValue, double convertToValue, double convertFromValue, string convertToUnit, string convertFromUnit)
        {
            var convertCalculation = new ConversionCalculation
            {
                ConversionDisplayValue = conversionDisplayValue,
                ConversiontToValue = convertToValue,
                ConverToUnit = convertToUnit,
                ConverFromUnit = convertFromUnit,
                ConversiontFromValue = convertFromValue
            };

            await _connection.CreateTableAsync<ConversionCalculation>();
            await _connection.InsertAsync(convertCalculation);
        }


        /// <summary>
        ///  Clears the ConversionCalculation Table of all data
        /// </summary>
        async protected void ClearStackCalculationsTable()
        {
            await _connection.CreateTableAsync<ConversionCalculation>();

            await _connection.DropTableAsync<ConversionCalculation>();
        }


        /// <summary>
        /// Gets the abbreviation for the selected measurement (e.g metres = m).
        /// </summary>
        /// <returns>The abbreviation.</returns>
        /// <param name="measurment">Measurment.</param>
        protected string GetAbbreviation(string measurment)
        {
            string abbreviation;

            switch (measurment)
            {
                case "Metres":
                    abbreviation = "m";
                    break;
                case "Feet":
                    abbreviation = "ft";
                    break;
                case "Links":
                    abbreviation = " links";
                    break;
                case "Hectares":
                    abbreviation = "ha";
                    break;
                case "Acres":
                    abbreviation = "ac";
                    break;
                default:
                    abbreviation = "";
                    break;
            }

            return abbreviation;
        }
        #endregion
    }

}