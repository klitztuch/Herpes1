using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Herpes.Model;

namespace Herpes.ViewModel
{
    public class UserSelectionViewModel: ViewModelBase
    {
        #region Properties

        private ObservableCollection<Player> _players;

        /// <summary>
        ///     Sets the players or returns them.
        /// </summary>
        public ObservableCollection<Player> Players
        {
            get => _players ?? (_players = new ObservableCollection<Player>());
            set => Set(nameof(Players), ref _players, value);
        }

        #endregion
    }
}