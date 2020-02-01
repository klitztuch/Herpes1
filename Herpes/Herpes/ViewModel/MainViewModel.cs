using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Herpes.Enum;
using Herpes.Infrastructure.Service;
using Herpes.Model;
using Herpes.Resx;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Herpes.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Commands

        private RelayCommand _navigateToDetailsCommand;

        /// <summary>
        ///     Gets the command to navigate.
        /// </summary>
        public RelayCommand NavigateToDetailsCommand
        {
            get
            {
                return _navigateToDetailsCommand ?? (_navigateToDetailsCommand = new RelayCommand(NavigateToDetails));
            }
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

        private readonly INavigationService _navigationService;
        private string _newPlayerName;

        /// <summary>
        ///     Sets the name for a new player or gets them
        /// </summary>
        public string NewPlayerName
        {
            get => _newPlayerName;
            set => Set(nameof(NewPlayerName), ref _newPlayerName, value);
        }

        private UserSelectionViewModel _userSelectionViewModel;

        /// <summary>
        ///     Gets the userselectionviewmodel or sets it.
        /// </summary>
        public UserSelectionViewModel UserSelectionViewModel
        {
            get => _userSelectionViewModel;
            set => Set(nameof(UserSelectionViewModel), ref _userSelectionViewModel, value);
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an new instance of the <see cref="MainViewModel"/>
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="userSelectionViewModel"></param>
        public MainViewModel(INavigationService navigationService, UserSelectionViewModel userSelectionViewModel)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _userSelectionViewModel =
                userSelectionViewModel ?? throw new ArgumentNullException(nameof(userSelectionViewModel));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigate to the <see cref="DetailsViewModel"/>
        /// </summary>
        private void NavigateToDetails()
        {
            _navigationService.NavigateTo(ViewModelLocator.DetailsPage);
        }

        /// <summary>
        /// Navigate to the <see cref="GameViewModel"/>
        /// </summary>
        private async void NavigateToGame()
        {
            if (UserSelectionViewModel.Players.Count == 0)
            {
                await MaterialDialog.Instance.SnackbarAsync(AppResources.NotEnoughPlayers,
                    AppResources.Okay);
                return;
            }

            _navigationService.NavigateTo(ViewModelLocator.GamePage);
        }

        /// <summary>
        ///     Adds a new <see cref="Player"/> to the active players
        /// </summary>
        private async void AddPlayer()
        {
            // Check if input is empty
            if (string.IsNullOrEmpty(_newPlayerName))
            {
                await MaterialDialog.Instance.SnackbarAsync(AppResources.NameNotSet,
                    AppResources.Okay);
                return;
            }

            // Check if players already exists
            if (_userSelectionViewModel.Players == null)
            {
                _userSelectionViewModel.Players = new ObservableCollection<Player>();
            }

            var nextId = _userSelectionViewModel.Players.Any() ? _userSelectionViewModel.Players.Max(o => o.Id) + 1 : 1;
            _userSelectionViewModel.Players.Add(new Player()
            {
                Id = nextId,
                Name = NewPlayerName
            });
            NewPlayerName = string.Empty;
        }

        /// <summary>
        /// Deletes <see cref="Player"/> from the active players
        /// </summary>
        /// <param name="id">id to delete</param>
        private void DeletePlayer(int id)
        {
            var toDelete = _userSelectionViewModel.Players.Where(o => o.Id == id);
            foreach (var player in toDelete)
            {
                _userSelectionViewModel.Players.Remove(player);
            }
        }

        #endregion
    }
}