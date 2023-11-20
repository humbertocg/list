using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using list.Models.API;
using Xamarin.Forms;

namespace list.ViewModels
{
    public class GetDataViewModel : BaseVM
    {
        private string _count;
        private Models.API.Entry _selectedItem;

        public string Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (value != null && value != _count)
                {
                    _count = value;
                    OnPropertyChanged(nameof(Count));
                }
            }
        }

        public ObservableCollection<Models.API.Entry> Entries { get; set; }

        public Models.API.Entry SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if(value != null && value != _selectedItem)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    Debug.WriteLine(value.Description);
                }
            }
        }

        public GetDataViewModel(INavigation navigation) : base(navigation)
        {
            Entries = new ObservableCollection<Models.API.Entry>();
        }

        public override async Task OnViewAppearing()
        {
            try
            {
                var result = await ServiceConsumer.GetAsync<ApiEntries>("/entries");
                Count = result.Count.ToString();
                Entries = new ObservableCollection<Models.API.Entry>(result.Entries.ToList());
                OnPropertyChanged(nameof(Entries));
            }
            catch(HttpRequestException Ex)
            {
                Count = Ex.Message;
                Debug.WriteLine(Ex.Message);
            }
            await base.OnViewAppearing();
        }
    }
}


