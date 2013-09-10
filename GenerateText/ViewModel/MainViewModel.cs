using GalaSoft.MvvmLight;
using GenerateText.Model;
using System.Linq;

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

            _generatedTexts = new System.Collections.ObjectModel.ObservableCollection<TextItemViewModel>();

            this.PropertyChanged += MainViewModel_PropertyChanged;

            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    _pattern = item.LastPattern;
                    _numberOfCharacters = item.LastCount;
                    _qAApproved = item.LastQa;
                    _countList = item.LastCountList;

                    _results = _generator.Generate(this.NumberOfCharacters, this.Pattern, this.QAApproved);

                    generateCountList();
                });
        }

        void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ResultsPropertyName)
                return;

            switch (e.PropertyName)
            {
                case PatternPropertyName:
                    this.Results = _generator.Generate(this.NumberOfCharacters, this.Pattern, this.QAApproved);
                    generateCountList();
                    saveSettings();
                    break;

                case NumberOfCharactersPropertyName:
                    this.Results = _generator.Generate(this.NumberOfCharacters, this.Pattern, this.QAApproved);
                    saveSettings();
                    break;

                case QAApprovedPropertyName:
                    this.Results = _generator.Generate(this.NumberOfCharacters, this.Pattern, this.QAApproved);
                    generateCountList();
                    saveSettings();
                    break;

                case CountListPropertyName:
                    generateCountList();
                    saveSettings();
                    break;
            }
            
        }

        private void saveSettings()
        {
            var item = new DataItem()
            {
                LastCount = this.NumberOfCharacters
                , LastPattern = this.Pattern
                , LastQa = this.QAApproved
                , LastCountList = this.CountList
            };

            _dataService.Save(item);

        }

        private void generateCountList()
        {
            var l = new System.Collections.Generic.List<int>();

            var ss = this._countList.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var s in ss)
            {
                var i = 0;

                if (int.TryParse(s, out i))
                {
                    l.Add(i);
                }
            }

            while (_generatedTexts.Count < l.Count)
                _generatedTexts.Add(new TextItemViewModel());

            while (_generatedTexts.Count > l.Count)
                _generatedTexts.RemoveAt(_generatedTexts.Count-1);
            
            for (int i = 0; i < l.Count; i++)
            {
                _generatedTexts[i].Count = l[i];
                _generatedTexts[i].Display = _generator.Generate(l[i], this.Pattern, this.QAApproved);

            }
            
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

        #region CountList
        /// <summary>
        /// The <see cref="CountList" /> property's name.
        /// </summary>
        public const string CountListPropertyName = "CountList";

        private string _countList;

        /// <summary>
        /// Sets and gets the CountList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CountList
        {
            get
            {
                return _countList;
            }

            set
            {
                if (_countList == value)
                {
                    return;
                }

                RaisePropertyChanging(CountListPropertyName);
                _countList = value;
                RaisePropertyChanged(CountListPropertyName);
            }
        }
        #endregion
	

        #region GeneratedTexts
        /// <summary>
        /// The <see cref="GeneratedTexts" /> property's name.
        /// </summary>
        public const string GeneratedTextsPropertyName = "GeneratedTexts";

        private System.Collections.ObjectModel.ObservableCollection<TextItemViewModel> _generatedTexts;

        /// <summary>
        /// Sets and gets the GeneratedTexts property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public System.Collections.ObjectModel.ObservableCollection<TextItemViewModel> GeneratedTexts
        {
            get
            {
                return _generatedTexts;
            }

            set
            {
                if (_generatedTexts == value)
                {
                    return;
                }

                RaisePropertyChanging(GeneratedTextsPropertyName);
                _generatedTexts = value;
                RaisePropertyChanged(GeneratedTextsPropertyName);
            }
        }
        #endregion
	
	
        #endregion
    }
}