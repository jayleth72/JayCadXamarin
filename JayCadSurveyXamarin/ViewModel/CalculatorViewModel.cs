using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;

namespace JayCadSurveyXamarin.ViewModel
{
    public class CalculatorViewModel : BaseViewModel
    {
        private string _outputResult;                           // Result displayed to screen. 
        private List<string> _inputs = new List<string>();      // List to hold user calculator inputs
        private List<string> _operators = new List<string>();   // Operators list for validation without the .
        private List<int> _numbers = new List<int>();           // Numbers for validation
        private string _currentOutput;

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

            _outputResult = "ass";
            _currentOutput = "0";

            // initialise validation lists
            InitialiseLists();
		}

        private void InitialiseLists()
        {
            int num = 0;

            // Fill numbers list
            while (num < 10)
            {
                _numbers.Add(num);
                num++;
            }

            // Fill Operators List
            _operators.Add("+");
            _operators.Add("-");
            _operators.Add("x");
            _operators.Add("÷");
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

            _outputResult =  value;

			OnPropertyChanged(OutputResult);
		}
    }
}
