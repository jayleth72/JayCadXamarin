using System;
using System.Collections.ObjectModel;
using JayCadSurveyXamarin.Model;
using SQLite;

namespace JayCadSurveyXamarin.ViewModel
{
    public class ShowStackViewModel : BaseViewModel
    {
        
		private  ObservableCollection<ConversionCalculation> _calculations;    // Collection to hold a series of sequential conversion calculations
        private string _ass;
        ObservableCollection<ConversionCalculation> _butt;
		/// <summary>
		/// Returns the list of calculations
		/// </summary>
		/// <value>The calculations list.</value>
		public ObservableCollection<ConversionCalculation> StackCalculations
		{

			get { return _calculations; }
			set { SetValue(ref _calculations, value); }
		}

		public string Ass
		{

			get { return _ass; }
			set { SetValue(ref _ass, value); }
		}

		public ShowStackViewModel(IPageService pageService) : base(pageService)
        {


            //RetrieveCalculations();
            GH();
			_ass = "butt";
            OnPropertyChanged();
        }

        private void GH()
        {
            RetrieveCalculations();
        }

        async private void RetrieveCalculations()
        {
           await _connection.CreateTableAsync<ConversionCalculation>();

            //var calcs = await _connection.Table<ConversionCalculation>().ToListAsync();
			//_calculations = new ObservableCollection<ConversionCalculation>(calcs);

			_calculations = new ObservableCollection<ConversionCalculation> {
				new ConversionCalculation{ CalculationId=20, ConvertFrom="ass", ConvertTo="bum"},
				new ConversionCalculation{ CalculationId=20, ConvertFrom="ass", ConvertTo="bum"},
				new ConversionCalculation{ CalculationId=20, ConvertFrom="ass", ConvertTo="bum"},
				new ConversionCalculation{ CalculationId=20, ConvertFrom="ass", ConvertTo="bum"}
			};
        }
    }
}
