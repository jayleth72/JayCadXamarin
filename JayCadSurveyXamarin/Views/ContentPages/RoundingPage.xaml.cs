using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.Model;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class RoundingPage : ContentPage
    {
        public RoundingPage()
        {
            InitializeComponent();

            BindingContext = new RoundingViewModel(new PageService());
        }
    }
}
