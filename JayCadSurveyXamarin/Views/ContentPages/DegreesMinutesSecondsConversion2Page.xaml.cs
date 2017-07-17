using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class DegreesMinutesSecondsConversion2Page : ContentPage
    {
        public DegreesMinutesSecondsConversion2Page()
        {
            InitializeComponent();

            BindingContext = new DegreesMinutesSecondsConversionViewModel(new PageService());
        }
    }
}
