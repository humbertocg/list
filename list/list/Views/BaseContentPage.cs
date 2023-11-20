using System;
using list.ViewModels;
using Xamarin.Forms;

namespace list.Views
{
	public class BaseContentPage: ContentPage
	{
		private BaseVM _baseVM;
		public BaseContentPage()
		{
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _baseVM = (BaseVM)BindingContext;
            _baseVM?.OnViewAppearing();
        }

        protected override void OnDisappearing()
        {
            _baseVM = (BaseVM)BindingContext;
            _baseVM?.OnViewDisappearing();
            base.OnDisappearing();
        }
    }
}

