using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;


namespace JayCadSurveyXamarin.MenuPages
{
    public partial class AngleConversionsMenuPage : ContentPage
    {
        public AngleConversionsMenuPage()
        {
            InitializeComponent();

            BindingContext = new AngleConversionMenuViewModel(new PageService());
        }

		
    }
}
