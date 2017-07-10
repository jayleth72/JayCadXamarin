using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class DecimalAngleConversionPage : ContentPage
    {
        public DecimalAngleConversionPage()
        {
            InitializeComponent();
			BindingContext = new DecimalAngleConversionViewModel(new PageService());
        }

		
		
    }
}
