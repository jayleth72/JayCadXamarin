using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace JayCadSurveyXamarin.MenuPages
{
    public partial class ConversionsMenuPage : ContentPage
    {
        public ConversionsMenuPage()
        {
            InitializeComponent();
        }

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			Button selectedButton = (Button)sender;

			switch (selectedButton.Text)
			{
				case "Length Conversion":
					await Navigation.PushAsync(new ContentPages.LengthConversionPage());
					break;
				case "Area Conversion":
					await Navigation.PushAsync(new ContentPages.AreaConversionPage());
					break;
				case "Angle Conversions":
                    await Navigation.PushAsync(new MenuPages.AngleConversionsMenuPage());
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
