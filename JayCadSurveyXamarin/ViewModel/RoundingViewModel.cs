using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JayCadSurveyXamarin.Persistence;
using JayCadSurveyXamarin.Model;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace JayCadSurveyXamarin.ViewModel
{
    public class RoundingViewModel : BaseViewModel
    {
           
		private int _lengthConversionSelectedIndex;     // Index for length conversion picker for selecting decimal roundings
		private int _areaConversionSelectedIndex;       // Index for area conversion picker for selecting decimal roundings
		private int _decimalAngleConversionIndex;            // Index for deciaml angle conversion picker for selecting decimal roundings

	    /// <summary>
        /// Gets or sets the length conversion selected index for the decimal roundings picker.
        /// </summary>
        /// <value>selected index from the picker</value>
		public int LengthConversionSelected
		{
			get { return _lengthConversionSelectedIndex; }
			set { SetValue(ref _lengthConversionSelectedIndex, value); }
		}

		/// <summary>
		/// Gets or sets the Area conversion selected index for the decimal roundings picker.
		/// </summary>
		/// <value>selected index from the picker</value>
		public int AreaConversionSelected
		{
			get { return _areaConversionSelectedIndex; }
			set { SetValue(ref _areaConversionSelectedIndex, value); }
		}

		/// <summary>
		/// Gets or sets the Area conversion selected index for the decimal roundings picker.
		/// </summary>
		/// <value>selected index from the picker</value>
		public int DecimalAngleConversionSelected
		{
			get { return _areaConversionSelectedIndex; }
			set { SetValue(ref _areaConversionSelectedIndex, value); }
		}
        		
		public ICommand SaveRoundingsCommand { get; private set; }
        //public ICommand DefaultRoundingsCommand { get; private set; }

		public RoundingViewModel(IPageService pageService) : base(pageService)
        {
			SaveRoundingsCommand = new Command(SaveRoundings);
			//DefaultRoundingsCommand = new Command(DefaultRoundings);

            RetrieveRoundings();
           
		}

        async private void SaveRoundings()
        {
            var lengthConversionRounding = new RoundingForDisplay
            {
                RoundingId = 1,
                RoundingName = "LengthConversion",
                RoundingValue = _lengthConversionSelectedIndex
            };

			await App.Database.SaveRoundingDisplayAsync(lengthConversionRounding);

			var areaConversionRounding = new RoundingForDisplay
			{
				RoundingId = 2,
				RoundingName = "AreaConversion",
				RoundingValue = _areaConversionSelectedIndex
			};

			await App.Database.SaveRoundingDisplayAsync(areaConversionRounding);

			var decimalAngleConversionRounding = new RoundingForDisplay
			{
				RoundingId = 3,
				RoundingName = "DecimalAngleConversion",
				RoundingValue = _lengthConversionSelectedIndex
			};

			await App.Database.SaveRoundingDisplayAsync(decimalAngleConversionRounding);
        }
               
        /// <summary>
        /// Initialises the roundings to zero or stored values.
        /// </summary>
        private void InitialiseRoundings(int selectedLengthRounding, int selectedAreaRounding, int selectedDecimalRounding)
        {
            _areaConversionSelectedIndex = selectedAreaRounding;
            _lengthConversionSelectedIndex = selectedAreaRounding;
            _decimalAngleConversionIndex = selectedDecimalRounding;

            OnPropertyChanged();
        }
      
        async public void RetrieveRoundings()
        {
    
            if (await App.Database.GetRoundingDisplayCount() > 0)
            {
				RoundingForDisplay length = await App.Database.GetRoundingForDIsplay(1);
				RoundingForDisplay area = await App.Database.GetRoundingForDIsplay(2);
				RoundingForDisplay decimalAngle = await App.Database.GetRoundingForDIsplay(3);

				InitialiseRoundings(length.RoundingValue, area.RoundingValue, decimalAngle.RoundingValue);
			}
            else
            {
                InitialiseRoundings(0, 0, 0);
            }
        }
    }
}
