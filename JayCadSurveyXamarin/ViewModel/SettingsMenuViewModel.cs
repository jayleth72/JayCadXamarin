using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
	public class SettingsMenuViewModel : BaseViewModel
	{
		// View Button commands
		public ICommand GoToRoundingPageCommand { get; private set; }
		public ICommand GoToMainMenuCommand { get; private set; }

		public SettingsMenuViewModel(IPageService pageService) : base(pageService)
		{
			GoToRoundingPageCommand = new Command(async () => await GoToRoundingPage());       // Navigation for Conversions Page
			
			GoToMainMenuCommand = new Command(async () => await GoToMainMenu());              // Navigation for Main Menu 
		}

		private async Task GoToRoundingPage()
		{
			await _pageService.PushAsync(new ContentPages.RoundingPage());
		}

		private async Task GoToMainMenu()
		{
			await _pageService.PopAsync();
		}
	}

}
