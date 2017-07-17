using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class DecimalAngleConversion2Page : ContentPage
    {
        public DecimalAngleConversion2Page()
        {
            InitializeComponent();

            BindingContext = new DecimalAngleConversionViewModel(new PageService());
        }
    }
}
