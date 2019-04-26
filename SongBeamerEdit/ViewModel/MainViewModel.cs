using Microsoft.Win32;
using SongBeamerEdit.Command;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace SongBeamerEdit.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Felder
        private string _fileText = string.Empty;
        private string _editText = string.Empty;
        private string _pageText = string.Empty;
        private string _loadedText;
        private string _defaultPath;
        private string _saveEdidedPath;
        private int    _maxSonglines;
        private CommandBinding _saveCommandBinding;
        private CommandBinding _saveAsCommandBinding;
        private CommandBinding _openCommandBinding;
        private CommandBinding _printCommandBinding;
        private CommandBinding _closeCommandBinding;
        private static MainViewModel _mvm;
        #endregion

        #region Konstruktor
        public MainViewModel()
        {
            if (_mvm == null) _mvm = this;
            // Commands initialisieren
            TextChangedCommand = new RelayCommand(TextChangedExecute, TextChangedCanExecute);
            
            // CommandBindings erzeugen
            _saveCommandBinding   = new CommandBinding(ApplicationCommands.Save, SaveExecuted, SaveCanExecute);
            _saveAsCommandBinding = new CommandBinding(ApplicationCommands.SaveAs, SaveAsExecuted, SaveAsCanExecute);
            _openCommandBinding   = new CommandBinding(ApplicationCommands.Open, OpenExecuted, OpenCanExecute);
            _printCommandBinding  = new CommandBinding(ApplicationCommands.Print, PrintExecuted, PrintCanExecute);
            _closeCommandBinding = new CommandBinding(ApplicationCommands.Close, CloseExecuted, CloseCanExecuted);

            //Standardwerte setzen
            _defaultPath    = Properties.Settings.Default.LoadDefoldPath;
            _saveEdidedPath = Properties.Settings.Default.SaveEditedSongPath;
            _maxSonglines   = Properties.Settings.Default.MaxDisplaySonglines;
        }

        #endregion

        #region Methoden der CommandBindings
        private void SaveCanExecute     (object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SongViewModel.SVM.EditTextIsChanged;
        }
        private void SaveExecuted       (object sender, ExecutedRoutedEventArgs e)
        {
            SaveSong(sender, e);
        }
        private void SaveAsCanExecute   (object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SongViewModel.SVM.EditTextIsChanged;
        }
        private void SaveAsExecuted     (object sender, ExecutedRoutedEventArgs e)
        {
            SaveAsSong(sender, e);
        }
        private void OpenCanExecute     (object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void OpenExecuted       (object sender, ExecutedRoutedEventArgs e)
        {
            if (Directory.Exists(Properties.Settings.Default.LoadLastPath))
            {
                _loadedText = LoadSong(Properties.Settings.Default.LoadLastPath);
            }
            else
            {
                _loadedText = LoadSong(Properties.Settings.Default.LoadDefoldPath);
            }
            SongViewModel.SVM.EditTextIsChanged = false;    //Der Filetext wurde bisher nicht verändert
            FileText = _loadedText;                         //Hält den geladenen oder geaänderten Songtext
            SongViewModel.SVM.InitSong(_loadedText);
        }
        private void PrintCanExecute    (object sender, CanExecuteRoutedEventArgs e)
        {
            if (FileText != string.Empty)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }
        private void PrintExecuted      (object sender, ExecutedRoutedEventArgs e)
        {
            PrintSong(sender, e);
        }
        private void CloseCanExecuted   (object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CloseExecuted      (object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Methoden für Commands
        private bool TextChangedCanExecute(object arg)
        {
            return true;
        }
        private void TextChangedExecute   (object arg)
        {
            SongViewModel.SVM.InitSong(FileText);
            SongViewModel.SVM.EditTextIsChanged = true;
        }
        #endregion

        #region Öffentliche Methoden
        public void CancelViewClosing()
        {
            if (SongViewModel.SVM.EditTextIsChanged)
            {
                MessageBoxResult result = MessageBox.Show("Möchten Sie die Änderungen speichern?", "Speichern?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    SaveExecuted(this, null);
                }
            }
        }
        #endregion

        #region Private Methoden
        private string LoadSong(string path)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Songbeamer Dateien (*.sng)|*.sng|Alle Dateien|*.*";
            fileDialog.DefaultExt = ".sng";
            fileDialog.InitialDirectory = Directory.Exists(path) ? path : @"C:\";
            Nullable<bool> dialogOK = fileDialog.ShowDialog();
            if (dialogOK == true)
            {
                SongViewModel.SVM.OrigFileName = Path.GetFileName(fileDialog.FileName);
                Properties.Settings.Default.LoadLastPath = Path.GetDirectoryName(fileDialog.FileName);
                return File.ReadAllText(fileDialog.FileName, Encoding.Default);
            }
            else
            {
                return string.Empty;
            }
        }
        private void SaveSong  (Object sender, ExecutedRoutedEventArgs e)
        {
            string path = Properties.Settings.Default.LoadLastPath + "\\" + SongViewModel.SVM.OrigFileName;
            if (File.Exists(path))
            {
                MessageBoxResult result = MessageBox.Show(path + " ist bereits vorhanden.\nMöchten Sie das Elemet ersetzen?","Speichern",MessageBoxButton.YesNo,MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    File.WriteAllText(path, SongViewModel.SVM.EditText, Encoding.Default);
                    SongViewModel.SVM.EditTextIsChanged = false;
                }
            }
            else
            {
                SaveAsSong(sender, e);
            }
        }
        private void SaveAsSong (Object sender, ExecutedRoutedEventArgs e)
        {
            MenuItem toSave = new MenuItem();
            var MainWindow = sender as MainWindow;
            string save = string.Empty;
            string parameter = e.Parameter as string;
            switch (parameter)
            {
                case "EditedSong":
                    save = SongViewModel.SVM.EditText;
                    break;
                default:
                    save = FileText;
                    break;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Songbeamer Dateien (*.sng)|*.sng|Alle Dateien|*.*";
            saveFileDialog.DefaultExt = ".sng";
            saveFileDialog.FileName = SongViewModel.SVM.OrigFileName;
            saveFileDialog.InitialDirectory = Properties.Settings.Default.SaveEditedSongPath;
            Nullable<bool> dialogOK = saveFileDialog.ShowDialog();
            if (dialogOK == true) File.WriteAllText(saveFileDialog.FileName, save, Encoding.Default);
        }
        private void PrintSong (Object sender, ExecutedRoutedEventArgs e)
        {
            TextBox toPrint = new TextBox();
            var MainWindow = sender as MainWindow;
            string parameter = e.Parameter as string;
            switch (parameter)
            {
                case "EditedSong":
                    toPrint = MainWindow.TBSong;
                    break;
                default:
                    toPrint = MainWindow.DateiText;
                    break;
            }
            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true))
            {
                string printCaption = SongViewModel.SVM.OrigFileName;
                PrintDialog printDialog = new PrintDialog();
                FlowDocument document = new FlowDocument(new Paragraph(new Run(toPrint.Text)));
                document.PagePadding = new Thickness(80,80,80,80);
                printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, printCaption);
            }
        }
        #endregion

        #region Eigenschaften
        public string FileText
        {
            get { return _fileText; }
            set
            {
                SetProperty<string>(ref _fileText, value);
            }
        }
        public string DefaultPath
        {
            get { return _defaultPath; }
            set
            {
                SetProperty<string>(ref _defaultPath, value);
                Properties.Settings.Default.LoadDefoldPath = _defaultPath;
            }
        }
        public string SaveEdidedPath
        {
            get { return _saveEdidedPath; }
            set
            {
                SetProperty<string>(ref _saveEdidedPath, value);
                Properties.Settings.Default.SaveEditedSongPath= _saveEdidedPath;
            }
        }
        public int    MaxSonglines
        {
            get { return _maxSonglines; }
            set
            {
                SetProperty<int>(ref _maxSonglines, value);
                Properties.Settings.Default.MaxDisplaySonglines = _maxSonglines;
            }
        }

        public static MainViewModel MVM
        {
            get { return _mvm; }
        }
        public ICommand TextChangedCommand { get; private set; }
        #endregion
        #region Eigenschaften für Command Bindings
        public CommandBinding SaveAsCommandBinding
        {
            get { return _saveAsCommandBinding; }
        }
        public CommandBinding SaveCommandBinding
        {
            get { return _saveCommandBinding; }
        }
        public CommandBinding OpenCommandBinding
        {
            get { return _openCommandBinding; }
        }
        public CommandBinding PrintCommandBinding
        {
            get { return _printCommandBinding; }
        }
        public CommandBinding CloseCommandBinding
        {
            get { return _closeCommandBinding; }
        }
        #endregion
    }
}
