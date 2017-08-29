using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;

using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();

            BindingContext = new CalculatorViewModel(new PageService());
        }
    }
}
