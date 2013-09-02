using GalaSoft.MvvmLight;
using GenerateText.Model;

namespace GenerateText.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly Services.IQaStringGeneratorService _generator;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService, Services.IQaStringGeneratorService generator)
        {
            _dataService = dataService;
            _generator = generator;

            this.PropertyChanged += MainViewModel_PropertyChanged;

            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    Pattern = "Pattern";
                    NumberOfCharacters = 10;
                });
        }

        void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ResultsPropertyName)
                return;

            this.Results = _generator.Generate(this.NumberOfCharacters, this.Pattern, this.QAApproved);
        }

        #region properties
        #region Pattern
        /// <summary>
        /// The <see cref="Pattern" /> property's name.
        /// </summary>
        public const string PatternPropertyName = "Pattern";

        private string _pattern = string.Empty;

        /// <summary>
        /// Sets and gets the Pattern property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Pattern
        {
            get
            {
                return _pattern;
            }

            set
            {
                if (_pattern == value)
                {
                    return;
                }

                RaisePropertyChanging(PatternPropertyName);
                _pattern = value;
                RaisePropertyChanged(PatternPropertyName);
            }
        }

        #endregion

        #region NumberOfCharacters
        /// <summary>
        /// The <see cref="NumberOfCharacters" /> property's name.
        /// </summary>
        public const string NumberOfCharactersPropertyName = "NumberOfCharacters";

        private int _numberOfCharacters = 0;

        /// <summary>
        /// Sets and gets the NumberOfCharacters property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int NumberOfCharacters
        {
            get
            {
                return _numberOfCharacters;
            }

            set
            {
                if (_numberOfCharacters == value)
                {
                    return;
                }

                RaisePropertyChanging(NumberOfCharactersPropertyName);
                _numberOfCharacters = value;
                RaisePropertyChanged(NumberOfCharactersPropertyName);
            }
        }
        #endregion 

        #region QAApproved
        /// <summary>
        /// The <see cref="QAApproved" /> property's name.
        /// </summary>
        public const string QAApprovedPropertyName = "QAApproved";

        private bool _qAApproved = false;

        /// <summary>
        /// Sets and gets the QAApproved property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool QAApproved
        {
            get
            {
                return _qAApproved;
            }

            set
            {
                if (_qAApproved == value)
                {
                    return;
                }

                RaisePropertyChanging(QAApprovedPropertyName);
                _qAApproved = value;
                RaisePropertyChanged(QAApprovedPropertyName);
            }
        }
        #endregion

        #region Results
        /// <summary>
        /// The <see cref="Results" /> property's name.
        /// </summary>
        public const string ResultsPropertyName = "Results";

        private string _results = string.Empty;

        /// <summary>
        /// Sets and gets the Results property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Results
        {
            get
            {
                return _results;
            }

            set
            {
                if (_results == value)
                {
                    return;
                }

                RaisePropertyChanging(ResultsPropertyName);
                _results = value;
                RaisePropertyChanged(ResultsPropertyName);
            }
        }
        #endregion
        #endregion
    }
}