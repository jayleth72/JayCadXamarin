using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
    public class CalculatorViewModel : BaseViewModel
    {
        private string _outputResult;                       // Result displayed to screen. 

		public string OutputResult
		{
			get { return _outputResult; }
			set { SetValue(ref _outputResult, value); }
		}

		// View Button commands
		public ICommand OperatorCommand { get; private set; }            // Plus, minus, division etc buttons
		public ICommand OperandCommand { get; private set; }             // Number buttons
		public ICommand ScientificOperatorCommand { get; private set; }  // Scientific buttons Cos, Sin, Tan etc   

        public CalculatorViewModel(IPageService pageService) : base(pageService)
        {
			OperatorCommand = new Command(Operator);                                   
			OperandCommand = new Command(Operand);                                    
			ScientificOperatorCommand = new Command(ScientificOperator);

            _outputResult = "0";
		}

        private void Operator() 
        {
            _outputResult = "";

            OnPropertyChanged(OutputResult);

			_outputResult = "Operator";

			OnPropertyChanged(OutputResult);

        }

		private void Operand()
		{
			_outputResult = "";

			OnPropertyChanged(OutputResult);

            _outputResult = "Operand";

			OnPropertyChanged(OutputResult);
		}

		private void ScientificOperator()
		{
			_outputResult = "";

			OnPropertyChanged(OutputResult);

            _outputResult = "Science";

			OnPropertyChanged(OutputResult);
		}
    }
}
