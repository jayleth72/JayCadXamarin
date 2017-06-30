using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace JayCadSurveyXamarin.MenuPages
{
    public partial class AngleConversionsMenuPage : ContentPage
    {
        public AngleConversionsMenuPage()
        {
            InitializeComponent();
        }

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			Button selectedButton = (Button)sender;

			switch (selectedButton.Text)
			{
				case "Decimal Conversion":
                    await Navigation.PushAsync(new ContentPages.DecimalAngleConversionPage());
					break;
				case "DEG/MIN/SEC Conversion":
                    await Navigation.PushAsync(new ContentPages.DegreesMinutesSecondsConversionPage());
					break;
				case "Main Menu":
					await Navigation.PopToRootAsync();
					break;
				default:
					break;
			}
		}
    }
}
