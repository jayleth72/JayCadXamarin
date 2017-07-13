﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
  
	public class BaseViewModel : INotifyPropertyChanged
	{
		protected readonly IPageService _pageService;         // This is here to enable Page Navigation and DispalyAlerts

		public event PropertyChangedEventHandler PropertyChanged;

		// View Buttons
		public ICommand BackToPreviousPageCommand { get; private set; }     // Enable Back navigation on pages
		public ICommand BackToMainMenuCommand { get; private set; }         // Enable Back navigation on pages

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingField, value))
				return;

			backingField = value;

			OnPropertyChanged(propertyName);
		}

        public BaseViewModel(IPageService pageService)
        {
			BackToPreviousPageCommand = new Command(async () => await BackToPreviousPage());    // Navigation Back Buttom
			BackToMainMenuCommand = new Command(async () => await BackToMainMenu());            // Navigation for Button to Main Menu

			_pageService = pageService;                                                         // Enable navigation and DisplayAlerts from View Model
		}

		protected async Task BackToPreviousPage()
		{
			await _pageService.PopAsync();
		}

		protected async Task BackToMainMenu()
		{
			await _pageService.PopToRootAsync();
		}
              
	}

}
