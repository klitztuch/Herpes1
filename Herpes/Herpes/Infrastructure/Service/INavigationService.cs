using System;
using System.Collections.Generic;
using Herpes.Enum;
using Xamarin.Forms;

namespace Herpes.Infrastructure.Service
{
    public interface INavigationService : GalaSoft.MvvmLight.Views.INavigationService
    {
        void NavigateTo(AppPage pageKey, object parameter = null);
        void Configure(AppPage pageKey, Type pageType);
        void Configure(IEnumerable<(AppPage pageKey, Type pageType)> appPages);
        void Initialize(NavigationPage navigation);
    }
}