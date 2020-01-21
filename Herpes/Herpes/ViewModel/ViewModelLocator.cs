using GalaSoft.MvvmLight.Ioc;
using Herpes.Infrastructure.Service;

namespace Herpes.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<INavigationService, NavigationService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DetailsViewModel>();
            SimpleIoc.Default.Register<GameViewModel>();
        }

        public INavigationService NavigationService
        {
            get { return SimpleIoc.Default.GetInstance<INavigationService>(); }
        }

        public MainViewModel MainViewModel
        {
            get { return SimpleIoc.Default.GetInstance<MainViewModel>(); }
        }

        public DetailsViewModel DetailsViewModel
        {
            get { return SimpleIoc.Default.GetInstance<DetailsViewModel>(); }
        }

        public GameViewModel GameViewModel
        {
            get { return SimpleIoc.Default.GetInstance<GameViewModel>(); }
        }
    }
}