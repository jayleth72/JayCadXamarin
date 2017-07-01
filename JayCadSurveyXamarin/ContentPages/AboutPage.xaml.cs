using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class AboutPage : ContentPage
    {
		public AboutPage()
		{
			InitializeComponent();
		}

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

      
    }
}
