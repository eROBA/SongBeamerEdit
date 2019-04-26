using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using SongBeamerEdit.Command;
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
        private bool _editTextIsChanged;
        private bool _isNotEmpty = false;
        private static SongViewModel _svm;
        private int _selectedNumberPagelines;
        private ObservableCollection<int> _numbersPageLines;
        private Song mySong;
        #endregion

        #region Konstruktor
        public SongViewModel()
        {
            if (_svm == null) _svm = this;
            // Commands initialisieren
            LangChangedCommand = new RelayCommand(LangCheckboxesChangedExecute, LangCheckboxesChangedCanExecute);
            SelectionChangedCommand = new RelayCommand(SelectionChangedExecute, SelectionChangedCanExecute);
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Ablaufsteuerung beim Laden einer neuen Songbeamerdatei oder bei Änderung am geladenen Text
        /// </summary>
        /// <param name="songText"></param>
        public void InitSong(string songText)
        {
            mySong = new Song(songText);                            //Erzeugt neues Song Objekt, führt Songerkennung aus, erzeugt Objekte vom Song, Verszeilen und Zeilen,
                                                                    //sowie eine Versliste mit allen verfügbaren Sprachen und der Standardanzahl der darzustellenden Zeilenzahl
            EditText = mySong.Text;                                 //Der bearbeitete Text wird an das Songfenster übergeben
            IsNotEmpty = true;                                      //Siganalisiert dass ein Text geladen wurde (wird für 
            LangVisible = mySong.BitMaskSelectedLang;   //Die aus dem Text ermittelte Bitmaske der Sprachen -> Regelt wie viele Checkboxen angezeigt werden sollen
            LangProp    = mySong.BitMaskSelectedLang;   //Die aus dem Text ermittelte Bitmaske der Sprachen -> Regelt welche Checkboxen gecheckt sein sollen
            NumbersPageLines = PageLines(Properties.Settings.Default.MaxDisplaySonglines, mySong.SelectedLangCount, mySong.SelectedVerseListMaxLines.MaxVerseLinesCount);
        }
        #endregion

        #region Hilfsmethoden
        /// <summary>
        /// Erstellt eine Liste von wählbaren Zeilenzahlen
        /// </summary>
        /// <param name="Standardvorgabe maximaler Anzahl anzuzeigender Zeilen"></param>
        /// <param name="Anzahl gewaelter Sprachen"></param>
        /// <param name="Maximale Anzahl Zeilen im Vers"></param>
        /// <returns></returns>
        public ObservableCollection<int> PageLines(int vorgabeMaxZeilenanzahl, int anzahlGewaelterSprachen, int maxAnzahlZeilenImVers)
        {
            ObservableCollection<int> numberLineList = new ObservableCollection<int>();
            if (anzahlGewaelterSprachen < 1) 
            {
                numberLineList.Add(0);
                SelectedNumberPagelines = 0;
                return numberLineList;
            }
            for (int i = anzahlGewaelterSprachen; i <= maxAnzahlZeilenImVers; i = i + anzahlGewaelterSprachen)
            {
                numberLineList.Add(i);
            }
            if (maxAnzahlZeilenImVers % anzahlGewaelterSprachen > 0)
            {
                numberLineList.Add(maxAnzahlZeilenImVers);
            }
            int number = numberLineList.LastOrDefault(num => num < Properties.Settings.Default.MaxDisplaySonglines);
            int index = numberLineList.IndexOf(number) + 1;  //Setzt die Auswahl auf den Standardwert der maximal gewünschten Zeilenanzahl bzw. dem darauf folgenden Wert.
            if (index > numberLineList.Count-1) index = numberLineList.Count-1;
            SelectedNumberPagelines = numberLineList[index];
            return numberLineList;
        }
        #endregion

        #region Methoden für Commands
        private bool LangCheckboxesChangedCanExecute(object arg)
        {
            return true;
        }
        private void LangCheckboxesChangedExecute(object arg)       //Wird bei Veränderungen an den Checkboxen für die Sprachen ausgeführt
        {
            mySong.Vorspann = Regex.Replace(mySong.Vorspann, "(?<=#LangCount=)[0-4]+", mySong.FlagCount(LangProp).ToString());
            EditText = mySong.GetSongText(LangProp);                //Aufruf zur Estellung der Versliste unter Berücksichtigung der gewählten Sprache
            NumbersPageLines = PageLines(Properties.Settings.Default.MaxDisplaySonglines, mySong.SelectedLangCount, mySong.SelectedVerseListMaxLines.MaxVerseLinesCount);
        }
        private bool SelectionChangedCanExecute(object arg)
        {
            return true;
        }
        private void SelectionChangedExecute(object arg)
        {
                EditText = mySong.GetSongText(SelectedNumberPagelines);
        }
        #endregion

        #region Eigenschaften für Commands
        public ICommand LangChangedCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        #endregion

        #region Eigenschaften für View
        /// <summary>
        /// Comboboxliste möglicher Zeilenanzahlen in Abhängigkeit der gewählten maximalen Zeilenzahl und der Anzahl von Sprachen
        /// </summary>
        public ObservableCollection<int> NumbersPageLines
        {
            get { return _numbersPageLines; }
            set { SetProperty<ObservableCollection<int>>(ref _numbersPageLines, value); }
        }
        /// <summary>
        /// Der in der Combobox gewählte Wert für die maximale Anzahl anzuzeigender Zeilen
        /// </summary>
        public int SelectedNumberPagelines
        {
            get { return _selectedNumberPagelines; }
            set { SetProperty<int>(ref _selectedNumberPagelines, value);
            }
        }
        /// <summary>
        /// Songtext welcher als Basis für die Auswertung der anderen Ansichten dient
        /// </summary>
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
        /// <summary>
        ///Bitmaske der Checkboxen entsprechend der gewählten Sprachen 
        /// </summary>
        public Language LangProp
        {
            get { return _langProp; }
            set { SetProperty<Language>(ref _langProp, value); }
        }
        /// <summary>
        ///Bitmaske für die Sichtabrkeit der Checkboxen entsprechend der verfügbaren Anzahl von Sprachen 
        /// </summary>
        public Language LangVisible
        {
            get { return _langVisible; }
            set { SetProperty<Language>(ref _langVisible, value); }
        }
        /// <summary>
        /// Zeigt an ob ein Songtext gelanden wurde
        /// </summary>
        public bool IsNotEmpty
        {
            get { return _isNotEmpty; }
            set { SetProperty<bool>(ref _isNotEmpty, value); }
        }
        #endregion

        #region Öffentliche Eigenschaften
        /// <summary>
        /// Zeigt an ob der geladene Songtext verändert wurde
        /// </summary>
        public bool EditTextIsChanged
        {
            get { return _editTextIsChanged; }
            set { _editTextIsChanged = value; }
        }
        /// <summary>
        /// Filename der aktuell geladenen Songbeamerdatei
        /// </summary>
        public string OrigFileName
        {
            get { return _origFileName; }
            set { _origFileName = value; }
        }
        /// <summary>
        /// Hält die SongViewModel-Instanz welche beim Programmstart mit dem Aufruf von "InitializeComponent()" im MainWindow erzeugt wird.
        /// </summary>
        public static SongViewModel SVM
        {
            get { return _svm; }
        }
        #endregion
    }
}
