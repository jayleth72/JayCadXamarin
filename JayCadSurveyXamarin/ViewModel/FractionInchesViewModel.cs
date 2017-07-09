using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JayCadSurveyXamarin.Model;

namespace JayCadSurveyXamarin.ViewModel
{
    public class FractionInchesViewModel : INotifyPropertyChanged
    {
        private FractionInch _selectedFractionInch;

        /// <summary>
        /// Gets or sets the selected fraction inches from the Fraction Inch Picker.
        /// </summary>
        /// <value>The selected inches.</value>
        public FractionInch SelectedFractionInch
        {
            get
            {
                return _selectedFractionInch;
            }
            set
            {
                if (_selectedFractionInch != value)
                {

                    _selectedFractionInch = value;
                    OnPropertyChanged();

                }
            }
        }

        public FractionInchesViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    } 
}
