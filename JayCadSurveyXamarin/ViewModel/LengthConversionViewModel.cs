﻿﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JayCadSurveyXamarin.Model;
using Xamarin.Forms;
using System.Collections.Generic;

namespace JayCadSurveyXamarin.ViewModel
{
    public class LengthConversionViewModel: INotifyPropertyChanged
    {
        public LengthConversionViewModel()
        {
            // Initialis buttons in View
            ClearInputFieldCommand = new Command(ClearInputField);
            ClearResultFieldCommand = new Command(ClearResultField);
            ConvertUserInputCommand = new Command(ConvertUserInput);
            ClearStackCommand = new Command(ClearStack);
            ShowStackCommand = new Command(ShowStack);

        }

        private LengthConversion _selectedConversion;
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

        private string _conversionResult;   // Result from a user selected conversion
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

		private string _convertFromUserInput;   // User entered value to be converted
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

		private string _userInputPlaceholder;   // Placeholder for userInput value to be converted
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

        private bool _isFeetPickersVisible;
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

        private void setFeetPickersVisibility()
        {
            if (SelectedLengthConversion.conversionType == LengthConversion.CONVERSION_TYPE.FEET_TO_METRES)
                IsFeetPickersVisible = true;
            else
                IsFeetPickersVisible = false;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        public String Convert_Length_Measurement(double convertValue, LengthConversion chosenLengthConversion)
        {
            string result = "";

            switch (chosenLengthConversion.conversionType)
            {
                case LengthConversion.CONVERSION_TYPE.METRES_TO_FEET:
                    result = Convert_Metres_to_Feet(convertValue, chosenLengthConversion);
                    break;
                default:
                    break;
            }

            return result;
        }

        private String Convert_Metres_to_Feet(double convertValue, LengthConversion chosenLengthConversion)
        {
            double result;

            result = convertValue * chosenLengthConversion.ConversionFactor;
            return result.ToString() + " " + chosenLengthConversion.ConvertTo;
        }

        public string ass()
        {
            LengthConversion selected = SelectedLengthConversion;
            return ConvertFromUserInput;
        }

        public ICommand ClearInputFieldCommand { get; private set; }

        private void ClearInputField()
        {
            _convertFromUserInput = "";
            OnPropertyChanged(ConvertFromUserInput);
        }

        public ICommand ClearResultFieldCommand { get; private set; }

		private void ClearResultField()
		{
			_conversionResult = "";
			OnPropertyChanged(ConversionResult);
		}

        public ICommand ConvertUserInputCommand { get; private set; }

        private void ConvertUserInput()
        {
            
        }

		public ICommand ClearStackCommand { get; private set; }

		private void ClearStack()
		{

		}

		public ICommand ShowStackCommand { get; private set; }

		private void ShowStack()
		{

		}

    }   

}
