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
        private ObservableCollection<ConversionCalculation> _calculations;

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
    }
}
