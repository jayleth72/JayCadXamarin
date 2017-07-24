using System.Collections.ObjectModel;
using JayCadSurveyXamarin.Model;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;
using JayCadSurveyXamarin.Persistence;
using SQLite;

namespace JayCadSurveyXamarin.ContentPages
{
	public partial class ShowConversionStackPage : ContentPage
	{
		private SQLiteAsyncConnection _connection;
		private ObservableCollection<ConversionCalculation> _calculations;      // List used to populate The Conversion Calculations  Stack
        private int _conversionRounding;                                             // Specified rounding for decimal place fugures
		public SQLiteAsyncConnection Connection { get => _connection; set => _connection = value; }


		public ShowConversionStackPage(int conversionRounding)
		{
			InitializeComponent();
            _conversionRounding = conversionRounding;
			Connection = DependencyService.Get<ISQLiteDb>().GetConnection();
		}


		protected override async void OnAppearing()
		{
			await Connection.CreateTableAsync<ConversionCalculation>();

			var calcs = await Connection.Table<ConversionCalculation>().ToListAsync();
			_calculations = new ObservableCollection<ConversionCalculation>(calcs);
			stackList.ItemsSource = _calculations;

            convertFromTotal.Text = RunningTotals("convertFrom");
            convertToTotal.Text = RunningTotals("convertTo");
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
			await Connection.DeleteAsync(convertedCalulation);

            // Recalc totals
			convertFromTotal.Text = RunningTotals("convertFrom");
			convertToTotal.Text = RunningTotals("convertTo");
		}

        private string RunningTotals(string totalType)
        {
            double convertFrom = 0.0;
            double convertTo = 0.0;
            string convertFromUnit = "";
            string convertToUnit = "";

            foreach (ConversionCalculation item in _calculations)
            {
                convertTo += item.ConversiontToValue;
                convertFrom += item.ConversiontFromValue;

				convertFromUnit = item.ConverFromUnit;
				convertToUnit = item.ConverToUnit;
            }

            if (totalType.Equals("convertFrom"))
                return RoundDecimalFigures(convertFrom) + convertFromUnit;
            else
                return RoundDecimalFigures(convertTo) + convertToUnit;
        }


		/// <summary>
		/// Rounds the result to specified number of decimal figures stored in SQLite database.
		/// For example if length conversion roundings is set to 5 decimal figures then 1234.55555 will be displayed.
		/// </summary>
		/// <returns>The result formatted to specidfied number of figures after the decimal point</returns>
		/// <param name="result">Result.</param>
		protected string RoundDecimalFigures(double result)
		{
			string formattedResult;

			switch (_conversionRounding)
			{
				case 1:
					formattedResult = string.Format("{0:0.0}", result);
					break;
				case 2:
					formattedResult = string.Format("{0:0.00}", result);
					break;
				case 3:
					formattedResult = string.Format("{0:0.000}", result);
					break;
				case 4:
					formattedResult = string.Format("{0:0.0000}", result);
					break;
				case 5:
					formattedResult = string.Format("{0:0.00000}", result);
					break;
				default:
					formattedResult = string.Format("{0:0}", result);
					break;
			}

			// Remove any trailing zeros e.g. 541.167000 will be displayed as 541.167
			formattedResult = formattedResult.TrimEnd('0', '.');

			return formattedResult;
		}

	}
}

