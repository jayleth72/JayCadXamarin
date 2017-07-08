﻿﻿using System;
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
        private string _conversionResult;   // Result from a user selected conversion
        private string _convertFromUserInput;   // User entered value to be converted
        private string _userInputPlaceholder;   // Placeholder for userInput value to be converted
        private bool _isFeetPickersVisible;     // Visibility modifier for Inches and FractionInches pickers
       
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
            BackToPreviousPageCommand = new Command(async () => await BackToPreviousPage());
            BackToMainMenuCommand = new Command(async () => await BackToMainMenu());

            // Enable navigation from View Model
            _pageService = pageService;
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
          

        private void ConvertUserInput()
        {
            
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

		private void setFeetPickersVisibility()
		{
			if (SelectedLengthConversion.conversionType == LengthConversion.CONVERSION_TYPE.FEET_TO_METRES)
				IsFeetPickersVisible = true;
			else
				IsFeetPickersVisible = false;

		}
    }   

}
