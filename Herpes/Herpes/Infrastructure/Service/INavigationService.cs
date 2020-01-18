using System;
using Herpes.Enum;
using Xamarin.Forms;

namespace Herpes.Infrastructure.Service
{
    public interface INavigationService
    {
        void GoBack();
        void NavigateTo(AppPages pageKey);
        void NavigateTo(AppPages pageKey, object parameter);
        void Configure(AppPages pageKey, Type pageType);
        void Initialize(NavigationPage navigation);
    }
}