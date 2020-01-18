using GalaSoft.MvvmLight;
using Herpes.Infrastructure.Service;

namespace Herpes.ViewModel
{
    public class DetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        #region ctor

        public DetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion
    }
}