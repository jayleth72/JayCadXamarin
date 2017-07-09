using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.Model;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class LengthConversionPage : ContentPage
    {
        
		public LengthConversionPage()
        {
            InitializeComponent();
           
            BindingContext = new LengthConversionViewModel(new PageService());
              
		}


    }
}
