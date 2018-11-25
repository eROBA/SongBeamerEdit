using Microsoft.Win32;
using SongBeamerEdit.Command;
using System;
using System.Collections.ObjectModel;
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
        private string _fileText = String.Empty;
        private string _editText = String.Empty;
        private string _pageText = String.Empty;
        private string loadedText;
        private CommandBinding _saveCommandBinding;
        private CommandBinding _saveAsCommandBinding;
        private CommandBinding _openCommandBinding;
        private CommandBinding _printCommandBinding;
        private static MainViewModel _mvm;
        #endregion

        #region Konstruktor
        public MainViewModel()
        {
            if (_mvm == null) _mvm = this;
            // Commands initialisieren
            TextChangedCommand = new RelayCommand(TextChangedExecute, TextChangedCanExecute);
            SelectionChangedCommand = new RelayCommand(SelectionChangedExecute, SelectionChangedCanExecute);

            // CommandBindings erzeugen
            _saveCommandBinding = new CommandBinding(ApplicationCommands.Save, SaveExecuted, SaveCanExecute);
            _saveAsCommandBinding = new CommandBinding(ApplicationCommands.SaveAs, SaveAsExecuted, SaveAsCanExecute);
            _openCommandBinding = new CommandBinding(ApplicationCommands.Open, OpenExecuted, OpenCanExecute);
            _printCommandBinding = new CommandBinding(ApplicationCommands.Print, PrintExecuted, PrintCanExecute);
        }
        #endregion

        #region Methoden der CommandBindings
        private void SaveCanExecute     (object sender, CanExecuteRoutedEventArgs e)
        {
            if(SongViewModel.SVM.IsChanged == true)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            } 
        }
        private void SaveExecuted       (object sender, ExecutedRoutedEventArgs e)
        {
            SaveSong();
        }
        private void SaveAsCanExecute   (object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SongViewModel.SVM.IsChanged ? true : false;
        }
        private void SaveAsExecuted     (object sender, ExecutedRoutedEventArgs e)
        {
            SaveAsSong();
        }
        private void OpenCanExecute     (object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void OpenExecuted       (object sender, ExecutedRoutedEventArgs e)
        {
            if (Directory.Exists(Properties.Settings.Default.LoadLastPath))
            {
                loadedText = LoadSong(Properties.Settings.Default.LoadLastPath);
            }
            else
            {
                loadedText = LoadSong(Properties.Settings.Default.LoadDefoldPath);
            }
            SongViewModel.SVM.InitSong(loadedText);
            FileText = loadedText;
            SongViewModel.SVM.IsChanged = false;
        }
        private void PrintCanExecute    (object sender, CanExecuteRoutedEventArgs e)
        {
            if (MainViewModel.MVM.FileText != string.Empty)
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
            PrintSong(sender);
        }
        #endregion

        #region Methoden für Commands
        private bool TextChangedCanExecute(object arg)
        {
            return true;
        }
        private void TextChangedExecute(object arg)
        {
            SongViewModel.SVM.InitSong(FileText);
            SongViewModel.SVM.SongEinteilen();
            SongViewModel.SVM.IsChanged = true;
        }
        private bool SelectionChangedCanExecute(object arg)
        {
            return true;
        }
        private void SelectionChangedExecute(object arg)
        {
            SongViewModel.SVM.SongEinteilen();
        }

        #endregion

        #region Öffentliche Methoden
        public void CancelViewClosing()
        {
            if (SongViewModel.SVM.IsChanged)
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
        private void SaveSong()
        {
            string path = Properties.Settings.Default.LoadLastPath + "\\" + SongViewModel.SVM.OrigFileName;
            if (File.Exists(path))
            {
                MessageBoxResult result = MessageBox.Show(path + " ist bereits vorhanden.\nMöchten Sie das Elemet ersetzen?","Speichern",MessageBoxButton.YesNo,MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    File.WriteAllText(path, SongViewModel.SVM.EditText, Encoding.Default);
                    SongViewModel.SVM.IsChanged = false;
                }
            }
            else
            {
                SaveAsSong();
            }
        }
        private void SaveAsSong()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Songbeamer Dateien (*.sng)|*.sng|Alle Dateien|*.*";
            saveFileDialog.DefaultExt = ".sng";
            saveFileDialog.FileName = SongViewModel.SVM.OrigFileName;
            saveFileDialog.InitialDirectory = Properties.Settings.Default.LoadLastPath;
            Nullable<bool> dialogOK = saveFileDialog.ShowDialog();
            if (dialogOK == true) File.WriteAllText(saveFileDialog.FileName, FileText, Encoding.Default);
        }
        private void PrintSong(Object sender)
        {
            var MainWindow = sender as MainWindow;
            TextBox TBSong = MainWindow.TBSong;
            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true))
            {
                string printCaption = SongViewModel.SVM.OrigFileName;
                PrintDialog printDialog = new PrintDialog();
                FlowDocument document = new FlowDocument(new Paragraph(new Run(TBSong.Text)));
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
        public static MainViewModel MVM
        {
            get { return _mvm; }
        }
        public ICommand TextChangedCommand { get; private set; }
        public ICommand SelectionChangedCommand { get; private set; }
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
        #endregion
    }
}
