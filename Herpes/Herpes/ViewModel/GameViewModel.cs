using System;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Herpes.Model;
using Xamarin.Forms;

namespace Herpes.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        #region Properties

        private Random _random = new Random();
        private int _rounds = 0;
        private int _herpesRounds;
        private int _currentPlayerNum = 0;

        private Player _currentPlayer;

        /// <summary>
        ///     Gets the current Player or sets it.
        /// </summary>
        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set => Set(nameof(CurrentPlayer), ref _currentPlayer, value);
        }

        private UserSelectionViewModel _userSelectionViewModel;

        /// <summary>
        ///     Gets the UserSelectionViewModel or sets is
        /// </summary>
        public UserSelectionViewModel UserSelectionViewModel
        {
            get => _userSelectionViewModel;
            set => Set(nameof(UserSelectionViewModel), ref _userSelectionViewModel, value);
        }

        private ImageSource _diceImage;

        /// <summary>
        ///     Gets the DiceImage or sets it.
        /// </summary>
        public ImageSource DiceImage
        {
            get => _diceImage;
            set => Set(nameof(DiceImage), ref _diceImage, value);
        }

        private int _diceOneValue;

        /// <summary>
        ///     Gets the dicevalue or sets it.
        /// </summary>
        public int DiceOneValue
        {
            get => _diceOneValue;
            set => Set(nameof(DiceOneValue), ref _diceOneValue, value);
        }

        private int _diceTwoValue;

        /// <summary>
        ///     Gets the dicevalue or sets it.
        /// </summary>
        public int DiceTwoValue
        {
            get => _diceTwoValue;
            set => Set(nameof(DiceTwoValue), ref _diceTwoValue, value);
        }

        private bool _isHerpes;

        /// <summary>
        ///     Gets dice two visibility or sets it.
        /// </summary>
        public bool IsHerpes
        {
            get => _isHerpes;
            set => Set(nameof(IsHerpes), ref _isHerpes, value);
        }

        private Player _herpesPlayer;

        /// <summary>
        ///     Gets the current Herpes or sets it.
        /// </summary>
        public Player HerpesPlayer
        {
            get => _herpesPlayer;
            set => Set(nameof(HerpesPlayer), ref _herpesPlayer, value);
        }

        #endregion

        #region Commands

        private RelayCommand _rollDiceCommand;

        /// <summary>
        /// Gets the command to roll the dice.
        /// </summary>
        public RelayCommand RollDiceCommand
        {
            get { return _rollDiceCommand ?? (_rollDiceCommand = new RelayCommand(RollDice)); }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an new instance of <see cref="GameViewModel"/>
        /// </summary>
        /// <param name="userSelectionViewModel"></param>
        public GameViewModel(UserSelectionViewModel userSelectionViewModel)
        {
            _userSelectionViewModel =
                userSelectionViewModel ?? throw new ArgumentNullException(nameof(userSelectionViewModel));
            _herpesRounds = userSelectionViewModel.Players.Count;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a new random Number for the Dice 
        /// </summary>
        private void RollDice()
        {
            CurrentPlayer = UserSelectionViewModel.Players.ElementAt(_currentPlayerNum);

            DiceOneValue = GetDiceValue();
            DiceTwoValue = GetDiceValue();
            if (DiceOneValue == 3 && _rounds != UserSelectionViewModel.Players.Count)
            {
                IsHerpes = true;
                HerpesPlayer = CurrentPlayer;
            }
            else if (IsHerpes == true && _rounds != UserSelectionViewModel.Players.Count)
            {
                _rounds++;
            }
            else if (_rounds == UserSelectionViewModel.Players.Count)
            {
                IsHerpes = false;
                HerpesPlayer = null;
            }

            _currentPlayerNum = _currentPlayerNum >= UserSelectionViewModel.Players.Count ? 0 : _currentPlayerNum++;
        }

        private int GetDiceValue()
        {
            return _random.Next(1, 7);
        }

        #endregion
    }
}