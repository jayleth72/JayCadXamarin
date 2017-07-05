using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.Model;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class LengthConversionPage : ContentPage
    {
        LengthConversion selectedConversion;

		public LengthConversionPage()
        {
            InitializeComponent();
            ConvertToUnit.SelectedIndex = 0;
            UpdateLabels();
		}

        async void Handle_Clicked(object sender, System.EventArgs e)
		{
            // Handle all click events here
            // Remove spaces and convert to lowercase

			Button selectedButton = (Button)sender;
            String buttonLabel = selectedButton.Text;
            buttonLabel = buttonLabel.Replace(" ", "");
            buttonLabel = buttonLabel.ToLower();
         
            switch (buttonLabel)
			{
				case "main menu":
					await Navigation.PopToRootAsync();
					break;
				case "back":
					await Navigation.PopAsync();
					break;
                case "clear":
                    // clear the units from user input
                    Clear_ConvertFromUnits();
                    break;
				case "convert":
                    Convert_Units();
					break;
				case "clearresult":
                    Clear_Result();
					break;
				case "clearstack":
                    Clear_Stack();
					break;
				case "showstack":
                    Show_Stack(); 
					break;
				default:
					break;
            }
		}

		void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            UpdateLabels();

            if(selectedConversion.ConvertFrom == "Feet")
            {
                InchesPicker.IsVisible = true;
                FractionsPicker.IsVisible = true;
            }
            else
            {
                InchesPicker.IsVisible = false;
                FractionsPicker.IsVisible = false;
            }    
		}


		/// <summary>
		/// clear the units from user input
		/// </summary>
		private void Clear_ConvertFromUnits() {
            MainUnitEntry.Text = "";
            InchesPicker.SelectedItem = null;
            FractionsPicker.SelectedItem = null;
        }

        /// <summary>
        /// Converts Value from User Input to selected units
        /// </summary>
		private void Convert_Units()
		{
			DisplayAlert("pressed convert", "pressed convert", "OK");
		}

		/// <summary>
		/// Clear the result of the conversion
		/// </summary>
		private void Clear_Result()
		{
			DisplayAlert("pressed clear result", "clear result", "OK");
		}

		/// <summary>
		/// Clear the stack that contains previous user conversions
		/// </summary>
		private void Clear_Stack()
		{
			DisplayAlert("pressed clear stack", "clear stack", "OK");
		}

		/// <summary>
		/// Show the stack page with all previous user conversions
		/// </summary>
		private void Show_Stack()
		{
			DisplayAlert("pressed show stack", "show stack", "OK");
		}

        /// <summary>
        /// Change labels on Convert From and Convert To Placeholder and label
        /// </summary>
        private void UpdateLabels() 
        {
            selectedConversion = (LengthConversion) ConvertToUnit.SelectedItem;
            MainUnitEntry.Placeholder = selectedConversion.ConvertFrom;
            ConvertToResultLbl.Text = selectedConversion.ConvertTo;
        }

    }
}
