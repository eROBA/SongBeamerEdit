using SongBeamerEdit.Converting;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SongBeamerEdit.ViewModel
{
    class SongViewModel: ViewModelBase
    {
        #region Private Felder
        private string _editText = string.Empty;
        private string _pageText = string.Empty;
        private string _origFileName;
        private bool _isChanged;
        private static SongViewModel _svm;
        private int _languageCount;
        private int _selectedNumberPagelines;
        private ObservableCollection<int> _numbersPageLine;
        private SongEdit Song;

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
            Song = new SongEdit(songText);
            Song.ConvertToVMFormat(songText);
        }
        public void SongEinteilen()
        {
            NumbersPageLines = PageLines(Song.AnzahlSprachen);              //Berechent die möglichen Anzahl von Zeilenkombinationen für die Collection
            Song.AnzahlZeilenProSeite = SelectedNumberPagelines;            //Setzt die Anzahl Zeilen die dargestellt wurden gemäß dem SeletedItem
            EditText = Song.SeitenaufteilungSong(Song.AnzahlSprachen);      //Aufruf zum Aufteilen
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
        public static SongViewModel SVM     //Hält die SongViewModel Instanz welche beim Programmstart mit dem Aufruf von "InitializeComponent()" im MainWindow erzeugt wird.
        {
            get { return _svm; }
        }
        #endregion

    }
}
