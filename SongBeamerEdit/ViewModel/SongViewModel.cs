using System.Collections.Generic;
using System.Collections.ObjectModel;
using SongBeamerEdit.Model;

namespace SongBeamerEdit.ViewModel
{
    class SongViewModel: ViewModelBase
    {
        #region Private Felder
        private string _editText = string.Empty;
        private string _pageText = string.Empty;
        private string _origFileName;
        private bool _isChanged;
        private bool _isNotEmpty = false;
        private ObservableCollection<bool> _languages = new ObservableCollection<bool>{ true, true, true, true };
        private static SongViewModel _svm;
        private int _languageCount;
        private int _selectedNumberPagelines;
        private ObservableCollection<int> _numbersPageLine;
        private Song Song;

        #endregion
        #region Konstruktor
        public SongViewModel()
        {
            if (_svm == null) _svm = this;
        }
        #endregion
        #region Methoden
        public void InitSong(string songText)
        {
            Song = new Song(songText);
            _languages[0] = (Song.SelectedLanguages & Language.Lang0) != 0;
            _languages[1] = (Song.SelectedLanguages & Language.Lang1) != 0;
            _languages[2] = (Song.SelectedLanguages & Language.Lang2) != 0;
            _languages[3] = (Song.SelectedLanguages & Language.Lang3) != 0;
            Languages = _languages;
            Song.SongAnalyse(songText);
            IsNotEmpty = true;
        }
        public void SongEinteilen()
        {
            NumbersPageLines = PageLines(Song.LanguageCount);               //Berechent die möglichen Anzahl von Zeilenkombinationen für die Collection
            Song.AnzahlZeilenProSeite = SelectedNumberPagelines;            //Setzt die Anzahl Zeilen die dargestellt wurden gemäß dem SeletedItem
            EditText =  Song.Vorspann + Song.VerseList.ToString();
        }
        private ObservableCollection<int> PageLines(int anzahlSprachen)
        {
            // Auswahlwerte für darzustellende Zeilenzahl
            List<int> numberLineList = new List<int>();
                for (int i = anzahlSprachen; i < 6; i = i + anzahlSprachen)
            {
                numberLineList.Add(i);
            }
            var test = new ObservableCollection<int>(numberLineList);
            return test;
        }
        #endregion
        #region Öffentliche Eigenschaften

        public ObservableCollection<int> NumbersPageLines
        {
            get { return _numbersPageLine; }
            set { SetProperty<ObservableCollection<int>>(ref _numbersPageLine, value); }
        }
        public int SelectedNumberPagelines
        {
            get { return _selectedNumberPagelines; }
            set { SetProperty<int>(ref _selectedNumberPagelines, value); }
        }
        public string EditText
        {
            get { return _editText; }
            set { SetProperty<string>(ref _editText, value); }
        }
        public string PageText
        {
            get { return _pageText; }
            set { SetProperty<string>(ref _pageText, value); }
        }
        public string OrigFileName
        {
            get { return _origFileName; }
            set { SetProperty<string>(ref _origFileName, value); }
        }
        public int LanguageCount
        {
            get { return _languageCount; }
            set { _languageCount = value; }
        }
        public bool IsChanged
        {
            get { return _isChanged; }
            set { _isChanged = value; }
        }
        public bool IsNotEmpty
        {
            get { return _isNotEmpty; }
            set { SetProperty<bool>(ref _isNotEmpty, value); }
        }
        public ObservableCollection<bool> Languages
        {
            get { return _languages; }
            set { SetProperty<ObservableCollection<bool>>(ref _languages, value); }
        }
        public static SongViewModel SVM     //Hält die SongViewModel Instanz welche beim Programmstart mit dem Aufruf von "InitializeComponent()" im MainWindow erzeugt wird.
        {
            get { return _svm; }
        }
        #endregion

    }
}
