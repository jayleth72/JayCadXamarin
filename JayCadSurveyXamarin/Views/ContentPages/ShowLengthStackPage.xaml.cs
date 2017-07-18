using System.Collections.ObjectModel;
using JayCadSurveyXamarin.Model;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class ShowLengthStackPage : ContentPage
    {
        public ShowLengthStackPage()
        {
            InitializeComponent();

            BindingContext = new ShowStackViewModel(new PageService());
                    
        }
    }
}
