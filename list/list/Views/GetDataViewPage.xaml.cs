using System;
using System.Collections.Generic;
using list.ViewModels;
using Xamarin.Forms;

namespace list.Views
{	
	public partial class GetDataViewPage : BaseContentPage
	{	
		public GetDataViewPage ()
		{
			InitializeComponent ();
			BindingContext = new GetDataViewModel(Navigation);
		}
	}
}

