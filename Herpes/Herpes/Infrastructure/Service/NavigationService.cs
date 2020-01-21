using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Herpes.Enum;
using Xamarin.Forms;

namespace Herpes.Infrastructure.Service
{
    public class NavigationService : INavigationService
    {
        // Dictionary with registered pages in the app
        private readonly Dictionary<AppPage, Type> _pagesByKey = new Dictionary<AppPage, Type>();

        // Navigation page where MainPage is hosted
        private NavigationPage _navigation;

        /// <summary>
        ///    Navigate to page by string with parameters  
        /// </summary>
        /// <param name="pageKey">page</param>
        /// <param name="parameter">object of parameter(s)</param>
        public void NavigateTo(string pageKey, object parameter)
        {
            try
            {
                System.Enum.TryParse(pageKey, out AppPage appPage);
                NavigateTo(appPage, parameter);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($@"Can't navigate. ""{pageKey}"" not in AppPages.");
            }
        }

        /// <summary>
        ///     Gets the current Page
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (_navigation.CurrentPage == null) return null;

                    var pageType = _navigation.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Key.ToString()
                        : null;
                }
            }
        }

        /// <summary>
        /// Pop page from navigationstack
        /// </summary>
        public void GoBack()
        {
            _navigation.PopAsync();
        }

        /// <summary>
        ///     Navigate to page by string without parameter
        /// </summary>
        /// <param name="pageKey">Page</param>
        public void NavigateTo(string pageKey)
        {
            try
            {
                System.Enum.TryParse(pageKey, out AppPage appPage);
                NavigateTo(appPage, null);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($@"Can't navigate. ""{pageKey}"" not in AppPages.");
            }
        }

        // NavigateTo method to navigate between pages with passing parameter:
        public void NavigateTo(AppPage pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    var type = _pagesByKey[pageKey];
                    ConstructorInfo constructor;
                    object[] parameters;

                    if (parameter == null)
                    {
                        constructor = type.GetTypeInfo()
                            .DeclaredConstructors
                            .FirstOrDefault(c => !c.GetParameters().Any());

                        parameters = new object[]
                        {
                        };
                    }
                    else
                    {
                        constructor = type.GetTypeInfo()
                            .DeclaredConstructors
                            .FirstOrDefault(
                                c =>
                                {
                                    var p = c.GetParameters();
                                    return p.Count() == 1
                                           && p[0].ParameterType == parameter.GetType();
                                });

                        parameters = new[]
                        {
                            parameter
                        };
                    }

                    if (constructor == null)
                        throw new InvalidOperationException(
                            "No suitable constructor found for page " + pageKey);

                    var page = constructor.Invoke(parameters) as Xamarin.Forms.Page;
                    _navigation.PushAsync(page);
                }
                else
                {
                    throw new ArgumentException(
                        $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?",
                        nameof(pageKey));
                }
            }
        }

        /// <summary>
        ///     Register a page and add to the dict
        /// </summary>
        /// <param name="pageKey">Page</param>
        /// <param name="pageType">Type of page</param>
        public void Configure(AppPage pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                    _pagesByKey[pageKey] = pageType;
                else
                    _pagesByKey.Add(pageKey, pageType);
            }
        }

        /// <summary>
        /// Register multiple pages and add them to the dict
        /// </summary>
        /// <param name="appPages"></param>
        public void Configure(IEnumerable<(AppPage pageKey, Type pageType)> appPages)
        {
            foreach (var (pageKey, pageType) in appPages)
            {
                Configure(pageKey, pageType);
            }
        }

        /// <summary>
        /// Initialize the first app page
        /// </summary>
        /// <param name="navigation"></param>
        public void Initialize(NavigationPage navigation)
        {
            _navigation = navigation;
        }
    }
}