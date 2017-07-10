using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
    public class DecimalAngleConversionViewModel:BaseViewModel
    {
		private readonly IPageService _pageService;         // This is here to enable Page Navigation and DispalyAlerts

		public DecimalAngleConversionViewModel(IPageService pageService)
        {
            BackToPreviousPageCommand = new Command(async () => await BackToPreviousPage());    // Navigation Back Buttom
            BackToMainMenuCommand = new Command(async () => await BackToMainMenu());            // Navigation for Button to Main Menu

			// Enable navigation from View Model
			_pageService = pageService;


        }

		public ICommand BackToPreviousPageCommand { get; private set; }
		public ICommand BackToMainMenuCommand { get; private set; }

		private async Task BackToPreviousPage()
		{
			await _pageService.PopAsync();
		}

		private async Task BackToMainMenu()
		{
			await _pageService.PopToRootAsync();
		}
    }
}
