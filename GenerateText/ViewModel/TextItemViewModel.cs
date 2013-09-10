using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace GenerateText.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TextItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the TextItemViewModel class.
        /// </summary>
        public TextItemViewModel()
        {
        }

        #region Properties

        #region Count
        /// <summary>
        /// The <see cref="Count" /> property's name.
        /// </summary>
        public const string CountPropertyName = "Count";

        private int _count;

        /// <summary>
        /// Sets and gets the Count property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }

            set
            {
                if (_count == value)
                {
                    return;
                }

                RaisePropertyChanging(CountPropertyName);
                _count = value;
                RaisePropertyChanged(CountPropertyName);
            }
        }
        #endregion

        #region Display
        /// <summary>
        /// The <see cref="Display" /> property's name.
        /// </summary>
        public const string DisplayPropertyName = "Display";

        private string _display;

        /// <summary>
        /// Sets and gets the Display property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Display
        {
            get
            {
                return _display;
            }

            set
            {
                if (_display == value)
                {
                    return;
                }

                RaisePropertyChanging(DisplayPropertyName);
                _display = value;
                RaisePropertyChanged(DisplayPropertyName);
            }
        }
        #endregion
	
        #endregion

        #region Commands
        private RelayCommand _copy;

        /// <summary>
        /// Gets the Copy.
        /// </summary>
        public RelayCommand Copy
        {
            get
            {
                return _copy
                    ?? (_copy = new RelayCommand(
                                          () =>
                                          {
                                              System.Windows.Clipboard.SetText(_display);
                                          }));
            }
        }
        #endregion
    }
}