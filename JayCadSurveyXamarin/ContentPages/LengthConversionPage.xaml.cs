using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class LengthConversionPage : ContentPage
    {
        public LengthConversionPage()
        {
            InitializeComponent();
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
                    await DisplayAlert("butt", "butt", "OK");
                    break;
				case "convert":
					await DisplayAlert("pressed convert", "pressed convert", "OK");
					break;
				case "clearresult":
					await DisplayAlert("pressed clear result", "clear result", "OK");
					break;
				case "clearstack":
					await DisplayAlert("pressed clear stack", "clear stack", "OK");
					break;
				case "showstack":
					await DisplayAlert("pressed show stack", "show stack", "OK");
					break;
				default:
					break;
            }
		}

        private void Clear_ConvertFromUnits() {
            
        }
    }
}
