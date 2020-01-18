using GalaSoft.MvvmLight.Ioc;

namespace Herpes.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DetailsViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get { return SimpleIoc.Default.GetInstance<MainViewModel>(); }
        }

        public DetailsViewModel DetailsViewModel
        {
            get { return SimpleIoc.Default.GetInstance<DetailsViewModel>(); }
        }
    }
}