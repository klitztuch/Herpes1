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

        private RelayCommand _navigateCommand;

        /// <summary>
        ///     Gets the command to navigate.
        /// </summary>
        public RelayCommand NavigateCommand
        {
            get { return _navigateCommand ?? (_navigateCommand = new RelayCommand(Navigate)); }
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
        
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //TODO: change following to loaded event
            Players = new ObservableCollection<Player>();
        }

        #region Methods

        private void Navigate()
        {
            _navigationService.NavigateTo(AppPages.DetailsPage);
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