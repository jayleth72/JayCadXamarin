using Xamarin.Forms;
using JayCadSurveyXamarin.Persistence;

namespace JayCadSurveyXamarin
{
    public partial class App : Application
    {
        //static JayCadLocalDB database;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MenuPages.MainMenuPage());
        }
              
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
