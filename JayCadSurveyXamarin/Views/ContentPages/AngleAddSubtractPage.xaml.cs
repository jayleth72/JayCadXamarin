using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class AngleAddSubtractPage : ContentPage
    {
        public AngleAddSubtractPage()
        {
            InitializeComponent();
        }

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PopAsync();
		}
    }
}
