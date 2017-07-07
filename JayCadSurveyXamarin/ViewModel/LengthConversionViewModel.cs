﻿﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JayCadSurveyXamarin.Model;

namespace JayCadSurveyXamarin.ViewModel
{
    public class LengthConversionViewModel: INotifyPropertyChanged
    {

        //public LengthConversion SelectedLengthConversion { get; set; }
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

        public string Set_New_Placeholder()
        {
            this._userInputPlaceholder = SelectedLengthConversion.ConvertFrom;
            return _userInputPlaceholder;
        }

    }   

}
