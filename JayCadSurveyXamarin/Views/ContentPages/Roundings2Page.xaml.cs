using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class Roundings2Page : ContentPage
    {
        public Roundings2Page()
        {
            InitializeComponent();

            BindingContext = new RoundingViewModel(new PageService());
        }
    }
}
