using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class AreaConversionPage : ContentPage
    {
        public AreaConversionPage()
        {
            InitializeComponent();
            BindingContext = new LengthConversionViewModel(new PageService());

        }

		
    }
}
