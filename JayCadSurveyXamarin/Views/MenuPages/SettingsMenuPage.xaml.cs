using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.MenuPages
{
    public partial class SettingsMenuPage : ContentPage
    {
        public SettingsMenuPage()
        {
            InitializeComponent();

            BindingContext = new SettingsMenuViewModel(new PageService());
        }
    }
}
