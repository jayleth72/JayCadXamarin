using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.Model;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class AreaConversion2Page : ContentPage
    {
        public AreaConversion2Page()
        {
            InitializeComponent();

            BindingContext = new AreaConversionViewModel(new PageService());
        }
    }
}
