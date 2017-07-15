using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class AboutPage : ContentPage
    {
		public AboutPage()
		{
			InitializeComponent();

            BindingContext = new AboutPageViewModel(new PageService());
		}

    }
}
