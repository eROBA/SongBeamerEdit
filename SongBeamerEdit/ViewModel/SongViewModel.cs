using SongBeamerEdit.Converting;

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
        private SongEdit edit = new SongEdit();
        #endregion
        #region Konstruktor
        public SongViewModel()
        {
            if (_svm == null) _svm = this;
        }
        #endregion
        #region Methoden
        public void Erkennen(string songText)
        {
            EditText = edit.ConvertToVMFormat(songText);
        }
        #endregion
        #region Öffentliche Eigenschaften
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
            set { _origFileName = value; }
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
