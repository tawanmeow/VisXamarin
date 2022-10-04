using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FreshMvvm;
using VisXamarin.PageModels;
using VisXamarin.Pages;

namespace VisXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var mainPage = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
            MainPage = new FreshNavigationContainer(mainPage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
