using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace JayCadSurveyXamarin.MenuPages
{
    public partial class MainMenuPage : ContentPage
    {
		public MainMenuPage()
		{
			InitializeComponent();
		}

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            Button selectedButton = (Button)sender;

            switch (selectedButton.Text)
            {
                case "Conversions":
                   await Navigation.PushAsync(new MenuPages.ConversionsMenuPage());
                    break;
				case "Angle Add/Subtract":
					await Navigation.PushAsync(new ContentPages.AngleAddSubtractPage());
					break;
				case "About":
					await Navigation.PushAsync(new ContentPages.AboutPage());
					break;
                default:
                    break;
            }
        }
               
    }
}
