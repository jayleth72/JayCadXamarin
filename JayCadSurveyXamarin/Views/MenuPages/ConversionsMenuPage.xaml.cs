using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.MenuPages
{
    public partial class ConversionsMenuPage : ContentPage
    {
        public ConversionsMenuPage()
        {
            InitializeComponent();

            BindingContext = new ConversionsMenuViewModel(new PageService());
        }
        	
    }
}
