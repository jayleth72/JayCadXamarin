using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class AngleAddSubtract2 : ContentPage
    {
        public AngleAddSubtract2()
        {
            InitializeComponent();

            BindingContext = new AngleAddSubtractViewModel(new PageService());
        }
    }
}
