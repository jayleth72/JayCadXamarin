using Xamarin.Forms;
using JayCadSurveyXamarin.Persistence;

namespace JayCadSurveyXamarin
{
    public partial class App : Application
    {
        static JayCadLocalDB database;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MenuPages.MainMenuPage());
        }

        public static JayCadLocalDB Database 
        {
            get 
            {
                if (database == null)
                {
                    database = new JayCadLocalDB(DependencyService.Get<ILocalFileHelper>().GetLocalFilePath("Jaycad.db3"));
                }
                return database;
            }
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
