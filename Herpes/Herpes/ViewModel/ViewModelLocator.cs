using GalaSoft.MvvmLight.Ioc;

namespace Herpes.ViewModel
{
    public class ViewModelLocator
    {
        #region Ctor

        public ViewModelLocator()
        {
            SimpleIoc.Default.Reset();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DetailsViewModel>();
            SimpleIoc.Default.Register<GameViewModel>();
            SimpleIoc.Default.Register<UserSelectionViewModel>();
        }

        #endregion

        #region Properties

        public const string MainPage = "MainPage";
        public const string DetailsPage = "DetailsPage";
        public const string GamePage = "GamePage";

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

        public UserSelectionViewModel UserSelectionViewModel
        {
            get { return SimpleIoc.Default.GetInstance<UserSelectionViewModel>(); }
        }

        #endregion
    }
}