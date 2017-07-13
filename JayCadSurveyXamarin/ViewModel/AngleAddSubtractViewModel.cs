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
        private string _degrees1;        // User input for degrees for angle1.
        private string _degrees2;        // User input for degrees for angle2.
		private string _minutes1;        // User input for minutes1 for angle1.
		private string _minutes2;        // User input for minutes2 for angle2.
		private string _seconds1;        // User input for seconds1 for angle1.
		private string _seconds2;        // User input for seconds2 for angle2.
        private string _result  ;        // result for angular addition and subtraction.

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

		public AngleAddSubtractViewModel(IPageService pageService) : base(pageService)
        {
			// Initialis buttons in View
			ClearDegreesInputCommand = new Command(ClearDegreesInput);
            ClearMinutesInputCommand = new Command(ClearMinutesInput);
            ClearSecondsInputCommand = new Command(ClearSecondsInput);
            ClearResultsCommand = new Command(ClearResults);
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
	
    }
}
