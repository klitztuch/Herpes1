using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Herpes.Enum;
using Herpes.Infrastructure.Service;
using Herpes.Model;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Herpes.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        #region Commands

        private RelayCommand _navigateToDetailsCommand;

        /// <summary>
        ///     Gets the command to navigate.
        /// </summary>
        public RelayCommand NavigateToDetailsCommand
        {
            get { return _navigateToDetailsCommand ?? (_navigateToDetailsCommand = new RelayCommand(NavigateToDetails)); }
        }

        private RelayCommand _navigateToGameCommand;

        public RelayCommand NavigateToGameCommand
        {
            get { return _navigateToGameCommand ?? (_navigateToGameCommand = new RelayCommand(NavigateToGame)); }
        }

        private RelayCommand _addPlayerCommand;

        public RelayCommand AddPlayerCommand
        {
            get { return _addPlayerCommand ?? (_addPlayerCommand = new RelayCommand(AddPlayer)); }
        }

        #endregion

        #region Properties

        private ObservableCollection<Player> _players;

        /// <summary>
        ///     Sets the players or returns them.
        /// </summary>
        public ObservableCollection<Player> Players
        {
            get => _players;
            set => Set(nameof(Players), ref _players, value);
        }

        private string _newPlayerName;

        /// <summary>
        ///     Sets the name for a new player or gets them
        /// </summary>
        public string NewPlayerName
        {
            get => _newPlayerName;
            set => Set(nameof(NewPlayerName), ref _newPlayerName, value);
        }

        #endregion

        #region Ctor

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            //TODO: change following to loaded event
            Players = new ObservableCollection<Player>();
        }

        #endregion

        #region Methods

        private void NavigateToDetails()
        {
            _navigationService.NavigateTo(AppPage.DetailsPage);
        }

        private void NavigateToGame()
        {
            _navigationService.NavigateTo(AppPage.GamePage);
        }

        private async void AddPlayer()
        {
            if (string.IsNullOrEmpty(_newPlayerName))
            {
                await MaterialDialog.Instance.AlertAsync(message: "Player name not set.",
                    title: "Error");
                return;
            }

            var nextId = Players.Any() ? Players.Max(o => o.Id) + 1 : 1;
            Players.Add(new Player()
            {
                Id = nextId,
                Name = NewPlayerName
            });
            NewPlayerName = string.Empty;
        }

        #endregion
    }
}