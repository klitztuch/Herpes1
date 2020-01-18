using GalaSoft.MvvmLight.Ioc;
using Herpes.Enum;
using Herpes.Infrastructure.Service;
using Herpes.Page;
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

            // MainPage = new MainPage();
            INavigationService navigationService;

            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                navigationService = new NavigationService();

                navigationService.Configure(AppPages.MainPage, typeof(MainPage));
                navigationService.Configure(AppPages.DetailsPage, typeof(DetailsPage));

                SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            }

            else
            {
                navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
            }

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