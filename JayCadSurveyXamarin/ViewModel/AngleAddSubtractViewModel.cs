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
    public class AngleAddSubtractViewModel : BaseViewModel
    {
        private string _degrees1;                // User input for degrees for angle1.
        private string _degrees2;                // User input for degrees for angle2.
		private string _minutes1;                // User input for minutes1 for angle1.
		private string _minutes2;                // User input for minutes2 for angle2.
		private string _seconds1;                // User input for seconds1 for angle1.
		private string _seconds2;                // User input for seconds2 for angle2.
        private string _result  ;                // result for angular addition and subtraction.
        private int _degreesInt1;                // Hold Valid Integer Input for degrees1.   
        private int _degreesInt2;                // Hold Valid Integer Input for degrees2.
		private int _minutesInt1;                // Hold Valid Integer Input for minutes1.   
		private int _minutesInt2;                // Hold Valid Integer Input for minutes2.
		private int _secondsInt1;                // Hold Valid Integer Input for seconds1.   
		private int _secondsInt2;                // Hold Valid Integer Input for seconds2.

		private enum INPUT_VALIDATION_FLAG
        {
            NO_INPUT_ENTERED,
            NON_NUMERICAL_DATA_ENTERED,
            INPUT_OK
        }

		/// <summary>
		/// Gets or sets Degrees1 field.
		/// </summary>
		/// <value>degress1 input.</value>
		public string Degrees1Input
		{
			get { return _degrees1; }
			set { SetValue(ref _degrees1, value); }
		}

		/// <summary>
		/// Gets or sets Degrees2 field.
		/// </summary>
		/// <value>degress2 input.</value>
		public string Degrees2Input
		{
			get { return _degrees2; }
			set { SetValue(ref _degrees2, value); }
		}

		/// <summary>
		/// Gets or sets minutes1 field.
		/// </summary>
		/// <value>minutes2 input.</value>
		public string Minutes1Input
		{
			get { return _minutes1; }
			set { SetValue(ref _minutes1, value); }
		}

		/// <summary>
		/// Gets or sets minutes2 field.
		/// </summary>
		/// <value>minutes2 input.</value>
		public string Minutes2Input
		{
			get { return _minutes2; }
			set { SetValue(ref _minutes2, value); }
		}

		/// <summary>
		/// Gets or sets seconds field.
		/// </summary>
		/// <value>minutes2 input.</value>
		public string Seconds1Input
		{
			get { return _seconds1; }
			set { SetValue(ref _seconds1, value); }
		}

		/// <summary>
		/// Gets or sets seconds2 field.
		/// </summary>
		/// <value>minutes2 input.</value>
		public string Seconds2Input
		{
			get { return _seconds2; }
			set { SetValue(ref _seconds2, value); }
		}

		/// Gets or sets seconds2 field.
		/// </summary>
		/// <value>minutes2 input.</value>
		public string Result
		{
			get { return _result; }
			set { SetValue(ref _result, value); }
		}

		// View Button commands
		public ICommand ClearDegreesInputCommand { get; private set; }
		public ICommand ClearMinutesInputCommand { get; private set; }
        public ICommand ClearSecondsInputCommand { get; private set; }
        public ICommand ClearResultsCommand { get; private set; }
        public ICommand AddAngleCommand { get; private set; }
		public ICommand SubtractAngleCommand { get; private set; }

		public AngleAddSubtractViewModel(IPageService pageService) : base(pageService)
        {
			// Initialis buttons in View
			ClearDegreesInputCommand = new Command(ClearDegreesInput);
            ClearMinutesInputCommand = new Command(ClearMinutesInput);
            ClearSecondsInputCommand = new Command(ClearSecondsInput);
            ClearResultsCommand = new Command(ClearResults);
            AddAngleCommand = new Command(AddAngle);
            SubtractAngleCommand = new Command(SubtractAngle);

		}

	    private void ClearDegreesInput()
        {
            _degrees1 = "";
            _degrees2 = "";  

            OnPropertyChanged(Degrees1Input);
            OnPropertyChanged(Degrees2Input);

            // Have to press button twice for below to work - weird.
            //clear(ref _degrees1, Degrees1Input); 
            //clear(ref _degrees2, Degrees2Input);
		}

		private void ClearMinutesInput()
		{
			_minutes1 = "";
			_minutes2 = "";

			OnPropertyChanged(Minutes1Input);
			OnPropertyChanged(Minutes2Input);

		}

		private void ClearSecondsInput()
		{
			_seconds1 = "";
			_seconds2= "";

			OnPropertyChanged(Degrees1Input);
			OnPropertyChanged(Degrees2Input);

		}

		private void ClearResults()
		{
			_result= "";
	
			OnPropertyChanged(Result);

		}

        /// <summary>
        /// Clear the specified input and caller.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="caller">Caller.</param>
        private void clear(ref string input, string caller)
        {
            // Have to press button twice for this to work.
            // Haven't got time to figure out why have to press button twice
            // SO not using this method for now
            input = "";
            OnPropertyChanged(caller);
        }
	    
        /// <summary>
        /// Check for null input entry.
        /// </summary>
        /// <returns><c>true</c>, if no data entered, <c>false</c> otherwise.</returns>
        /// <param name="input">Input.</param>
        private bool NoDataEntered (string input)
        {
            if (String.IsNullOrEmpty(input))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check for no data entry on an angle.
        /// Returns true if no data entered for degrees, minutes or seconds fields.
        /// </summary>
        /// <returns><c>true</c>, if no data entered for an angle, <c>false</c> otherwise.</returns>
        /// <param name="input1">Input1.</param>
        /// <param name="input2">Input2.</param>
        /// <param name="input3">Input3.</param>
        private bool NoDataEnteredAngle(string input1, string input2, string input3)
        {
            if (NoDataEntered(input1) && NoDataEntered(input2) && NoDataEntered(input3))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check for no data entered in one field
        /// If valid numerical data is entered, private integer  variable is initialised  with valid integer.
        /// </summary>
        /// <returns><c>true</c>, if numerical data entered was noned, <c>false</c> otherwise.</returns>
        /// <param name="input">Input.</param>
        /// <param name="outputNum">Output number.</param>
        private bool NonNumericalDataEntered(string input, ref int outputNum)
        {
            if (int.TryParse(input, out outputNum))
                return false;
            else
                return true;
        }

		/// <summary>
		/// Checks all input for Null entry data and Non-numerical data.
		/// Returns INPUT_VALIDATION_FLAG enum to indicate the status of the input
		/// If data passes all the tests then INPUT_OK is returned
		/// </summary>
		/// <returns>INPUT_VALIDATION_FLAG.</returns>
		private INPUT_VALIDATION_FLAG CheckAllInput()
        {
            // check for no data entered
            if (NoDataEnteredAngle(_degrees1, _minutes1, _seconds1) && NoDataEnteredAngle(_degrees2, _minutes2, _seconds2))
                return INPUT_VALIDATION_FLAG.NO_INPUT_ENTERED;

            // Check degrees1 if data has been entered
            if (NonNumericalDataEntered(_degrees1, ref _degreesInt1 ) && !NoDataEntered(_degrees1))
                return INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;

			// Check degrees2 if data has been entered
			if (NonNumericalDataEntered(_degrees2, ref _degreesInt2) && !NoDataEntered(_degrees2))
				return INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;

			// Check minutes1 if data has been entered
			if (NonNumericalDataEntered(_minutes1, ref _minutesInt1) && !NoDataEntered(_minutes1))
				return INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;

			// Check minutes2 if data has been entered
			if (NonNumericalDataEntered(_minutes2, ref _minutesInt2) && !NoDataEntered(_minutes2))
				return INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;

			// Check seconds1 if data has been entered
			if (NonNumericalDataEntered(_seconds1, ref _secondsInt1) && !NoDataEntered(_seconds1))
				return INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;

			// Check seconds2 if data has been entered
			if (NonNumericalDataEntered(_seconds2, ref _secondsInt2) && !NoDataEntered(_seconds2))
				return INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;
            
            return INPUT_VALIDATION_FLAG.INPUT_OK;
        }

        private async void AddAngle()
        {
            
            // Check for no entry of data
            if (CheckAllInput() == INPUT_VALIDATION_FLAG.NO_INPUT_ENTERED)
            {
				await _pageService.DisplayAlert("No Data Entered", "Please enter some data", "Ok");
				return;
            }

            // Check for Numeric data errors
			if (CheckAllInput() == INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED)
			{
				await _pageService.DisplayAlert("Numerical Data Error", "Please enter Integer numerical data (No decimals)", "Ok");
				return;
			}

            // Data has passed all tests

        }

		private async void SubtractAngle()
		{
			// Check for no entry of data
			if (CheckAllInput() == INPUT_VALIDATION_FLAG.NO_INPUT_ENTERED)
			{
				await _pageService.DisplayAlert("No Data Entered", "Please enter some data", "Ok");
				return;
			}

			// Check for Numeric data errors
			if (CheckAllInput() == INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED)
			{
				await _pageService.DisplayAlert("Numerical Data Error", "Please enter Integer numerical data (No decimals)", "Ok");
				return;
			}

			// Data has passed all tests

		}

        private double ConvertToDecimalAngle()
        {

            return 0.0;
        }

	}
}
