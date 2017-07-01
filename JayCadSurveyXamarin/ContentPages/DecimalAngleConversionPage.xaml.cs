using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class DecimalAngleConversionPage : ContentPage
    {
        public DecimalAngleConversionPage()
        {
            InitializeComponent();
        }

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			Button selectedButton = (Button)sender;

			switch (selectedButton.Text)
			{
				case "Main Menu":
					await Navigation.PopToRootAsync();
					break;
				case "Back":
					await Navigation.PopAsync();
					break;
				default:
					break;
			}
		}
    }
}
