using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RestConsumer;
using Xamarin.Forms;

namespace list.ViewModels
{
	public class BaseVM : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
		public INavigation Navigation { get; private set; }
		public IApiRestConsumer ServiceConsumer { get; private set; }

		public BaseVM(INavigation navigation)
		{
			Navigation = navigation;
			ServiceConsumer = new ApiRestConsumer();
			ServiceConsumer.SetUrl("https://api.publicapis.org");
		}

		public void OnPropertyChanged([CallerMemberName]string propertyName= null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public virtual async Task OnViewAppearing()
		{

		}

        public virtual async Task OnViewDisappearing()
        {

        }
    }
}

