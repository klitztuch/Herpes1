using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace Herpes.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        private ImageSource _diceImage;

        /// <summary>
        ///     Gets the DiceImage or sets it.
        /// </summary>
        public ImageSource DiceImage
        {
            get => _diceImage;
            set => Set(nameof(DiceImage), ref _diceImage, value);
        }
    }
}