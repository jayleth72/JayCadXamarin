﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
    public class PageService : IPageService
    {
        public PageService()
        {
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

		public async Task PopToRootAsync()
		{
			await Application.Current.MainPage.Navigation.PopToRootAsync();
			
		}

		public async Task PopAsync()
		{
			await Application.Current.MainPage.Navigation.PopAsync();

		}
    }
}
