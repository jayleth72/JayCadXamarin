using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.Views.ContentPages
{
    public partial class ConfigureSettingsPage : ContentPage
    {
        public ConfigureSettingsPage()
        {
            InitializeComponent();

            BindingContext = new ConfigureSettingsViewModel(new PageService());
        }
    }
}
