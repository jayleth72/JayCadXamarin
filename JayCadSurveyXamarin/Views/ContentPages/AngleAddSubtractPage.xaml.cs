using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class AngleAddSubtractPage : ContentPage
    {
        public AngleAddSubtractPage()
        {
            InitializeComponent();

            BindingContext = new AngleAddSubtractViewModel(new PageService());
        }
        		
    }
}
