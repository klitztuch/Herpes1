using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;
using Herpes.Enum;
using Herpes.Infrastructure.Service;
using Herpes.Page;
using Herpes.ViewModel;
using Xamarin.Forms;

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

            var navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
            // IEnumerable<(AppPage, Type)> allPages = new List<(AppPage, Type)>();
            // var allPagesEnum = System.Enum.GetValues(typeof(AppPage)).Cast<AppPage>();
            // foreach (var appPage in allPagesEnum)
            // {
            //     var type = Type.GetType($"Herpes.Page.{appPage}");
            //     var toAdd = (appPage, type);
            //     allPages.Append(toAdd);
            // }
            // navigationService.Configure(allPages);
            
            navigationService.Configure(AppPage.MainPage, typeof(MainPage));
            navigationService.Configure(AppPage.DetailsPage, typeof(DetailsPage));
            navigationService.Configure(AppPage.GamePage, typeof(GamePage));

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