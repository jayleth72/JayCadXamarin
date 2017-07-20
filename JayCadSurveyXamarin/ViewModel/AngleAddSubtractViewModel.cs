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
        private Angle _angle1;                   // angle object to hold degrees, minutes and second data.
        private Angle _angle2;                   // angle object to hold degrees, minutes and second data.
		
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

			OnPropertyChanged(Seconds1Input);
			OnPropertyChanged(Seconds2Input);

		}

		private void ClearResults()
		{
			_result= "";
	
			OnPropertyChanged(Result);

		}
        	    


        private void AddAngle()
        {
            // Check Input for Errors
            // Convert Null entries to zero values
            // Create Angles
            AngleOperationPreparation("add");

            OnPropertyChanged(Result);
        }

		private void SubtractAngle()
		{
			// Check Input for Errors
			// Convert Null entries to zero values
			// Create Angles
			AngleOperationPreparation("subtract");

            OnPropertyChanged(Result);
        }
              
		/// <summary>
		/// Prepare for Anglular addition or Subtraction.
		/// This method performs the following actions.
		/// 1. ClearResults Field.
		/// 2. Checks Input for errors.
		/// 3. Convert Null entries to zero and Create Angles if there is no input errors.
        /// 4. Performs addition or subtraction of angles according to operation parameter passed
		/// </summary>
		private async void AngleOperationPreparation(string operation)
        {
			INPUT_VALIDATION_FLAG inputFlag;

			ClearResults();

			// Check input for errors
			inputFlag = CheckAllInput();

            if (inputFlag == INPUT_VALIDATION_FLAG.INPUT_OK)
            {
				// Convert empty fields to zero
				ConvertNullToZero();

				// Create angle objects so we can do angular subtraction or addition
				CreateAngles();

                if (operation == "add")
                    _result = _angle1.AddAngle(_angle2);
                else
                    _result = _angle1.SubtractAngle(_angle2);
            }
            else
            {
				// Check for no entry of data
				if (inputFlag == INPUT_VALIDATION_FLAG.NO_INPUT_ENTERED)
				{
					await _pageService.DisplayAlert("No Data Entered", "Please enter some data", "Ok");
					return;
				}

				// Check for Numeric data errors
				if (inputFlag == INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED)
				{
					await _pageService.DisplayAlert("Numerical Data Error", "Please enter Integer numerical data (No decimals)", "Ok");
					return;
				}

				// Check for Degrees out of range  errors
				if (inputFlag == INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_DEGREES)
				{
					await _pageService.DisplayAlert("Out of Range Error", "Please enter a value between 0 and 359 in the degrees field", "Ok");
					return;
				}

				// Check for Degrees out of range  errors
				if (inputFlag == INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_MINUTES)
				{
					await _pageService.DisplayAlert("Out of Range Error", "Please enter a value between 0 and 59 in the minutes field", "Ok");
					return;
				}

				// Check for Degrees out of range  errors
				if (inputFlag == INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_SECONDS)
				{
					await _pageService.DisplayAlert("Out of Range Error", "Please enter a value between 0 and 59 in the seconds field", "Ok");
					return;
				}
			}        
					
        }

		/// <summary>
		/// Converts empty fields to zero so we can create Angle Objects
		/// </summary>
		private void ConvertNullToZero()
		{
			// Assign zero to empty fields
			_degreesInt1 = String.IsNullOrEmpty(_degrees1) ? 0 : _degreesInt1;
			_degreesInt2 = String.IsNullOrEmpty(_degrees2) ? 0 : _degreesInt2;

			_minutesInt1 = String.IsNullOrEmpty(_minutes1) ? 0 : _minutesInt1;
			_minutesInt2 = String.IsNullOrEmpty(_minutes2) ? 0 : _minutesInt2;

			_secondsInt1 = String.IsNullOrEmpty(_seconds1) ? 0 : _secondsInt1;
			_secondsInt2 = String.IsNullOrEmpty(_seconds2) ? 0 : _secondsInt2;
		}

		private void CreateAngles()
		{
			_angle1 = new Angle(_degreesInt1, _minutesInt1, _secondsInt1);
			_angle2 = new Angle(_degreesInt2, _minutesInt2, _secondsInt2);
		}

		/// <summary>
		/// Checks all input for Null entry data and Non-numerical data.
		/// Returns INPUT_VALIDATION_FLAG enum to indicate the status of the input
		/// If data passes all the tests then INPUT_OK is returned
		/// </summary>
		/// <returns>INPUT_VALIDATION_FLAG.</returns>
		private INPUT_VALIDATION_FLAG CheckAllInput()
		{
			INPUT_VALIDATION_FLAG inputFlag = INPUT_VALIDATION_FLAG.INPUT_OK;

			// check for no data entered
			if (NoDataEnteredAngle(_degrees1, _minutes1, _seconds1) && NoDataEnteredAngle(_degrees2, _minutes2, _seconds2))
				return INPUT_VALIDATION_FLAG.NO_INPUT_ENTERED;

			inputFlag = CheckInputForErrors(_degrees1, ref _degreesInt1, 0, 360, INPUT_FIELD.DEGREES);

			if (inputFlag != INPUT_VALIDATION_FLAG.INPUT_OK)
				return inputFlag;

			inputFlag = CheckInputForErrors(_degrees2, ref _degreesInt2, 0, 360, INPUT_FIELD.DEGREES);

			if (inputFlag != INPUT_VALIDATION_FLAG.INPUT_OK)
				return inputFlag;

			inputFlag = CheckInputForErrors(_minutes1, ref _minutesInt1, 0, 60, INPUT_FIELD.MINUTES);

			if (inputFlag != INPUT_VALIDATION_FLAG.INPUT_OK)
				return inputFlag;

			inputFlag = CheckInputForErrors(_minutes2, ref _minutesInt2, 0, 60, INPUT_FIELD.MINUTES);

			if (inputFlag != INPUT_VALIDATION_FLAG.INPUT_OK)
				return inputFlag;

			inputFlag = CheckInputForErrors(_seconds1, ref _secondsInt1, 0, 60, INPUT_FIELD.SECONDS);

			if (inputFlag != INPUT_VALIDATION_FLAG.INPUT_OK)
				return inputFlag;

            inputFlag = CheckInputForErrors(_seconds2, ref _secondsInt2, 0, 60, INPUT_FIELD.SECONDS);

			if (inputFlag != INPUT_VALIDATION_FLAG.INPUT_OK)
				return inputFlag;

			return inputFlag;
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
	}
}
