using System;
using System.Collections.ObjectModel;
using JayCadSurveyXamarin.Model;

namespace JayCadSurveyXamarin.ViewModel
{
    public class ShowStackViewModel : BaseViewModel
    {
        
		private  ObservableCollection<ConversionCalculation> _calculations;    // Collection to hold a series of sequential conversion calculations

		/// <summary>
		/// Returns the list of calculations
		/// </summary>
		/// <value>The calculations list.</value>
		public ObservableCollection<ConversionCalculation> StackCalculations
		{

			get { return _calculations; }
			set { SetValue(ref _calculations, value); }
		}


		public ShowStackViewModel(IPageService pageService) : base(pageService)
        {
            _calculations = new ObservableCollection<ConversionCalculation>(){
                new ConversionCalculation{ CalculationId = 1, ConvertFrom = "10 metres", ConvertTo = "20 feet" },
                new ConversionCalculation{ CalculationId = 1, ConvertFrom = "10 metres", ConvertTo = "20 feet" },
                new ConversionCalculation{ CalculationId = 1, ConvertFrom = "10 metres", ConvertTo = "20 feet" },
                new ConversionCalculation{ CalculationId = 1, ConvertFrom = "10 metres", ConvertTo = "20 feet" }
            };
        }
    }
}
