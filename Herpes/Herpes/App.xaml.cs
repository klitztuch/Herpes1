using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Herpes.Enum;
using Herpes.Infrastructure.Service;
using Herpes.Page;
using Herpes.ViewModel;
using Xamarin.Forms;
using XF.Material.Forms.UI;

// using Xamarin.Forms.Xaml;

// [assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Herpes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this, "Material.Configuration");

            var navigationService = new NavigationService();
            
            navigationService.Configure(ViewModelLocator.MainPage, typeof(MainPage));
            navigationService.Configure(ViewModelLocator.DetailsPage, typeof(DetailsPage));
            navigationService.Configure(ViewModelLocator.GamePage, typeof(GamePage));
            
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            
            var firstPage = new NavigationPage(new MainPage());
            navigationService.Initialize(firstPage);

            MainPage = firstPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}