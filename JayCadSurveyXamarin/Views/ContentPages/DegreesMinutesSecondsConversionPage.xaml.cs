using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class DegreesMinutesSecondsConversionPage : ContentPage
    {
        public DegreesMinutesSecondsConversionPage()
        {
            InitializeComponent();
            BindingContext = new DegreesMinutesSecondsConversionViewModel(new PageService());
        }

		
    }
}
