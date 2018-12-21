using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SongBeamerEdit.FlagsValueConverter;
using SongBeamerEdit.Model;

namespace SongBeamerEdit.ViewModel
{
    public class SongViewModel: ViewModelBase
    {
        #region Private Felder
        private string _editText = string.Empty;
        private string _pageText = string.Empty;
        private string _origFileName;
        private Language _langProp;
        private Language _langVisible;
        private bool _isChanged;
        private bool _isNotEmpty = false;
        private ObservableCollection<bool> _languages = new ObservableCollection<bool>{ true, true, true, true };
        private static SongViewModel _svm;
        private int _selectedNumberPagelines;
        private ObservableCollection<int> _numbersPageLine;
        private Song mySong;
        #endregion

        #region Konstruktor
        public SongViewModel()
        {
            if (_svm == null) _svm = this;
        }
        #endregion
        #region Methoden
        public void InitSong(string songText)       //Wird nach dem Laden einer neuen Datei und nach einer manuellen Änderung am geladenen Text aufgerufen
        {
            mySong = new Song(songText);
            LangProp = Song.SelectedLanguages;
            LangVisible = Song.SelectedLanguages;
            IsNotEmpty = true;
        }
        public void SongEinteilen()
        {
            NumbersPageLines = PageLines();                                     //Berechnet die möglichen Anzahl von Zeilenkombinationen für die Collection
            mySong.AnzahlZeilenProSeite = SelectedNumberPagelines;              //Setzt die Anzahl Zeilen die dargestellt wurden gemäß dem SeletedItem
            EditText =  mySong.Vorspann + mySong.VerseList.ToString();
        }
        private ObservableCollection<int> PageLines()
        {
            int anzahlSprachen = FlagCount(Song.SelectedLanguages);
            ObservableCollection<int> numberLineList = new ObservableCollection<int>();
            for (int i = anzahlSprachen; i < 10; i = i + anzahlSprachen)
            {
                numberLineList.Add(i);
            }
            return numberLineList;
        }
        public int FlagCount(Language enumValue)
        {
            var hasFlagAttribute = enumValue.GetType().GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0;
            if (!hasFlagAttribute) return 1;
            var count = 0;
            var value = Convert.ToInt16(enumValue);
            while (value != 0)
            {
                if ((value & 1) == 1) count++;
                value >>= 1;
            }
            return count;
        }
        #endregion

        #region Öffentliche Eigenschaften
        public ObservableCollection<int> NumbersPageLines                   //Auswahl möglicher Zeilenanzahlen in Abhänigkeit der gewählten Max Zeilenzahl
        {
            get { return _numbersPageLine; }
            set { SetProperty<ObservableCollection<int>>(ref _numbersPageLine, value); }
        }
        public int SelectedNumberPagelines                                  //Speichert die gewählte Anzahl darzustellender Zeilen
        {
            get { return _selectedNumberPagelines; }
            set { SetProperty<int>(ref _selectedNumberPagelines, value); }
        }
        public string EditText                                              //Speichert den modifizierten Songtext
        {
            get { return _editText; }
            set { SetProperty<string>(ref _editText, value); }
        }
        public string PageText
        {
            get { return _pageText; }
            set { SetProperty<string>(ref _pageText, value); }
        }
        public string OrigFileName                                          //Speichert den Filenamen
        {
            get { return _origFileName; }
            set { _origFileName = value; }
        }
        public Language LangProp                                            //Bitmaske für die verfügbaren Sprachen
        {
            get { return _langProp; }
            set
            {
                SetProperty<Language>(ref _langProp, value);
                //_langProp = value;
                mySong.GenerateSelectedVerseList(LangProp);
                EditText = mySong.Vorspann + mySong.SelectedVerseList.ToString();
            }
        }
        public Language LangVisible
        {
            get { return _langVisible; }
            set
            {
                SetProperty<Language>(ref _langVisible, value);
            }
        }                                      //Bitmaske für Anzeige der Checkboxen zur Auswahl der Sprachen
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
