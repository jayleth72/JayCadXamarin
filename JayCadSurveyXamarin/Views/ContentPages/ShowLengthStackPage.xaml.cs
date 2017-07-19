using System.Collections.ObjectModel;
using JayCadSurveyXamarin.Model;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;
using JayCadSurveyXamarin.Persistence;
using SQLite;

namespace JayCadSurveyXamarin.ContentPages
{
    public partial class ShowLengthStackPage : ContentPage
    {
       private SQLiteAsyncConnection _connection;
        private ObservableCollection<ConversionCalculation> _calculations;      // List used to populate The Conversion Calculations  Stack

        public ShowLengthStackPage()
        {
            InitializeComponent();

            //BindingContext = new ShowStackViewModel(new PageService());
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();       
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<ConversionCalculation>();

			var calcs = await _connection.Table<ConversionCalculation>().ToListAsync();
			_calculations = new ObservableCollection<ConversionCalculation>(calcs);
            stackList.ItemsSource = _calculations;
			base.OnAppearing();
        }

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
            await Navigation.PopAsync();
		}

        async void Delete_Clicked(object sender, System.EventArgs e)
        {
            var convertedCalulation = (sender as MenuItem).CommandParameter as ConversionCalculation;
            _calculations.Remove(convertedCalulation);

            // Delete from SQLiteDb as well
            await _connection.DeleteAsync(convertedCalulation);
        }


            
    }
}
