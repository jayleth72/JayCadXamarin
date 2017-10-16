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
			OperatorCommand = new Command<string> (Operator);                                   
			OperandCommand = new Command<string> (Operand);                                    
			ScientificOperatorCommand = new Command<string> (ScientificOperator);

            _outputResult = "0";
		}

        private void Operator(string value) 
        {
            _outputResult = "";

            OnPropertyChanged(OutputResult);

			_outputResult = value;

			OnPropertyChanged(OutputResult);

        }

		private void Operand(string value)
		{
			_outputResult = "";

			OnPropertyChanged(OutputResult);

            _outputResult = value;

			OnPropertyChanged(OutputResult);
		}

		private void ScientificOperator(string value)
		{
			_outputResult = "";

			OnPropertyChanged(OutputResult);

            _outputResult = value;

			OnPropertyChanged(OutputResult);
		}
    }
}
