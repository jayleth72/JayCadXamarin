using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
	public class ConversionsMenuViewModel : BaseViewModel
	{
		// View Button commands
		public ICommand GoToLengthConversionPageCommand { get; private set; }
		public ICommand GoToAreaConversionPageCommand { get; private set; }
		public ICommand GoToAngleConversionPageCommand { get; private set; }
		public ICommand GoToMainMenuCommand { get; private set; }

		public ConversionsMenuViewModel(IPageService pageService) : base(pageService)
		{
			GoToLengthConversionPageCommand = new Command(async () => await GoToLengthConversionPage());       // Navigation for Conversions Page
			GoToAreaConversionPageCommand = new Command(async () => await GoToAreaConversionPage());      // Navigation for AngleAddSubtractPage 
			GoToAngleConversionPageCommand = new Command(async () => await GoToAngleConversionPage());       // Navigation for Conversions Page
			GoToMainMenuCommand = new Command(async () => await GoToMainMenu());      // Navigation for AngleAddSubtractPage 
		}

		private async Task GoToLengthConversionPage()
		{
			await _pageService.PushAsync(new ContentPages.LengthConversionPage());
		}

		private async Task GoToAreaConversionPage()
		{
			await _pageService.PushAsync(new ContentPages.AreaConversionPage());
		}

        private async Task GoToAngleConversionPage()
		{
            await _pageService.PushAsync(new MenuPages.AngleConversionsMenuPage());
		}

		private async Task GoToMainMenu()
		{
			await _pageService.PopAsync();
		}
	}
}
