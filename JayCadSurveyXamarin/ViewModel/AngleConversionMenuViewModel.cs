using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
	public class AngleConversionMenuViewModel : BaseViewModel
	{
		// View Button commands
		public ICommand GoToDecimalConversionPageCommand { get; private set; }
		public ICommand GoToDegMinSecConversionPageCommand { get; private set; }
		public ICommand BackPageCommand { get; private set; }
		public ICommand GoToMainMenuCommand { get; private set; }

		public AngleConversionMenuViewModel(IPageService pageService) : base(pageService)
		{
			GoToDecimalConversionPageCommand = new Command(async () => await GoToDecimalConversionPage());       // Navigation for Conversions Page
			GoToDegMinSecConversionPageCommand = new Command(async () => await GoToDegMinSecConversionPage());      // Navigation for AngleAddSubtractPage 
			BackPageCommand = new Command(async () => await BackPage());       // Navigation for Conversions Page
			GoToMainMenuCommand = new Command(async () => await GoToMainMenu());      // Navigation for AngleAddSubtractPage 
		}

		private async Task GoToDecimalConversionPage()
		{
            await _pageService.PushAsync(new ContentPages.DecimalAngleConversionPage());
		}

		private async Task GoToDegMinSecConversionPage()
		{
			await _pageService.PushAsync(new ContentPages.DegreesMinutesSecondsConversionPage());
		}

		private async Task BackPage()
		{
			await _pageService.PopAsync();
		}

		private async Task GoToMainMenu()
		{
			await _pageService.PopToRootAsync();
		}
	}
}
