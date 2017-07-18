using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JayCadSurveyXamarin.Persistence;
using JayCadSurveyXamarin.Model;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SQLite;

namespace JayCadSurveyXamarin.ViewModel
{
    public class RoundingViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection _connection;
        private int _lengthConversionSelectedIndex;     // Index for length conversion picker for selecting decimal roundings
        private int _areaConversionSelectedIndex;       // Index for area conversion picker for selecting decimal roundings
        private int _decimalAngleConversionIndex;            // Index for deciaml angle conversion picker for selecting decimal roundings
        private string _dummyEntry;

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
            get { return _decimalAngleConversionIndex; }
            set { SetValue(ref _decimalAngleConversionIndex, value); }
        }

        public string DummyEntry
        {
            get { return _dummyEntry; }
            set { SetValue(ref _dummyEntry, value); }
        }
                
        public ICommand SaveRoundingsCommand { get; private set; }
        //public ICommand DefaultRoundingsCommand { get; private set; }

        public RoundingViewModel(IPageService pageService) : base(pageService)
        {
            SaveRoundingsCommand = new Command(SaveRoundings);
            //DefaultRoundingsCommand = new Command(DefaultRoundings);

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            RetrieveRoundings();
           
        }

        async private void SaveRoundings()
        {
            await _connection.CreateTableAsync<RoundingForDisplay>();

            var lengthConversionRounding = new RoundingForDisplay
            {
                RoundingId = 1,
                RoundingName = "LengthConversion",
                RoundingValue = _lengthConversionSelectedIndex
            };

            var areaConversionRounding = new RoundingForDisplay
            {
                RoundingId = 2,
                RoundingName = "AreaConversion",
                RoundingValue = _areaConversionSelectedIndex
            };
           
            var decimalAngleConversionRounding = new RoundingForDisplay
            {
                RoundingId = 3,
                RoundingName = "DecimalAngleConversion",
                RoundingValue = _decimalAngleConversionIndex
            };

            // Check if the Roundings Table exist 
            if (await _connection.Table<RoundingForDisplay>().CountAsync() > 0)
            {
                UpdateRoundings(lengthConversionRounding, areaConversionRounding, decimalAngleConversionRounding);
            }
            else
            {
                // RoundingForDisplay does not exist so we will save the tables
                await _connection.InsertAsync(lengthConversionRounding);
                await _connection.InsertAsync(areaConversionRounding);
                await _connection.InsertAsync(decimalAngleConversionRounding);
            }    
        }
               
        /// <summary>
        /// Initialises the roundings to zero or stored values.
        /// </summary>
        private void InitialiseRoundings(int selectedLengthRounding, int selectedAreaRounding, int selectedDecimalRounding)
        {
            _lengthConversionSelectedIndex = selectedLengthRounding;
            _areaConversionSelectedIndex = selectedAreaRounding;
            _decimalAngleConversionIndex = selectedDecimalRounding;
            _dummyEntry = "";

            OnPropertyChanged(DummyEntry);
        }
      
        /// <summary>
        /// Retrieves the roundings if they exist in SQLite database.
        /// </summary>
        async public void RetrieveRoundings()
        {
                await _connection.CreateTableAsync<RoundingForDisplay>();

                if (await _connection.Table<RoundingForDisplay>().CountAsync() > 0)
                {
                    RoundingForDisplay length = await _connection.Table<RoundingForDisplay>().Where(rd => rd.RoundingId == 1).FirstOrDefaultAsync(); 
                    RoundingForDisplay area = await _connection.Table<RoundingForDisplay>().Where(rd => rd.RoundingId == 2).FirstOrDefaultAsync();
                    RoundingForDisplay decimalAngle = await _connection.Table<RoundingForDisplay>().Where(rd => rd.RoundingId == 3).FirstOrDefaultAsync();

                    InitialiseRoundings(length.RoundingValue, area.RoundingValue, decimalAngle.RoundingValue);
                }
                else
                {
                    InitialiseRoundings(0, 0, 0);
                }

        }

        async public void UpdateRoundings(RoundingForDisplay lengthRounding, RoundingForDisplay areaRounding, RoundingForDisplay decimalAngleRounding)
        {
            await _connection.UpdateAsync(lengthRounding);
            await _connection.UpdateAsync(areaRounding);
            await _connection.UpdateAsync(decimalAngleRounding);
        }
    }
}
