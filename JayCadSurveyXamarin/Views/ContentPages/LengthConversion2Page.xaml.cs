using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.Model;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class LengthConversion2Page : ContentPage
    {
        public LengthConversion2Page()
        {
            InitializeComponent();

			BindingContext = new LengthConversionViewModel(new PageService());
        }
    }
}
