using System;
using list.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace list
{
    public partial class App : Application
    {
        public static bool IsMinimized { get; set; }
        public App ()
        {
            InitializeComponent();

            MainPage = new GetDataViewPage();
        }

        protected override void OnStart ()
        {
            IsMinimized = false;
        }

        protected override void OnSleep ()
        {

            IsMinimized = true;
        }

        protected override void OnResume ()
        {

            IsMinimized = false;
        }
    }
}

