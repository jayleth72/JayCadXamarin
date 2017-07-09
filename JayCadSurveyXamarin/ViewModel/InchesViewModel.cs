using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JayCadSurveyXamarin.Model;

namespace JayCadSurveyXamarin.ViewModel
{
    public class InchesViewModel: INotifyPropertyChanged
    {
		private Inches _selectedInches;     // Selected Inch from Inch picker
															
		public Inches SelectedInches
		{
			get
			{
				return _selectedInches;
			}
			set
			{
				if (_selectedInches != value)
				{

					_selectedInches = value;
					OnPropertyChanged();

				}
			}
		}



		public InchesViewModel()
        {
        }

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
    }
}
