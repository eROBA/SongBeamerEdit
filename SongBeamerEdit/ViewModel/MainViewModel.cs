using Microsoft.Win32;
using SongBeamerEdit.Command;
using SongBeamerEdit.Model;
using System;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;

namespace SongBeamerEdit.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Felder
        private string _fileText = String.Empty;
        private string _editText = String.Empty;
        private string _pageText = String.Empty;
        private CommandBinding _saveCommandBinding;
        private CommandBinding _saveAsCommandBinding;
        private CommandBinding _openCommandBinding;
        private static MainViewModel _mvm;
        #endregion

        #region Konstruktor
        public MainViewModel()
        {
            if (_mvm == null) _mvm = this;
            // Commands initialisieren
            FirstCommand = new RelayCommand(FirstExecute, BackCanExecute);

            // CommandBindings erzeugen
            _saveCommandBinding = new CommandBinding(ApplicationCommands.Save, SaveExecuted, SaveCanExecute);
            _saveAsCommandBinding = new CommandBinding(ApplicationCommands.SaveAs, SaveAsExecuted, SaveAsCanExecute);
            _openCommandBinding = new CommandBinding(ApplicationCommands.Open, OpenExecuted, OpenCanExecute);
        }
        #endregion

        #region Methoden der CommandBindings
        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
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
        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveSong();
        }
        private void SaveAsCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SongViewModel.SVM.IsChanged ? true : false;
        }
        private void SaveAsExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveAsSong();
        }
        private void OpenCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (Directory.Exists(Properties.Settings.Default.LoadLastPath))
            {
                FileText = LoadSong(Properties.Settings.Default.LoadLastPath);
            }
            else
            {
                FileText = LoadSong(Properties.Settings.Default.LoadDefoldPath);
            }
            SongViewModel.SVM.Erkennen(FileText); 
            SongViewModel.SVM.IsChanged = false;

        }
        #endregion

        #region Methoden für Commands
        private bool BackCanExecute(object obj)
        {
            return true;
        }
        private void FirstExecute(object obj)
        {
            //_personsView.MoveCurrentToFirst();
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
            //if (Directory.Exists(Properties.Settings.Default.LoadLastPath))
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
        #endregion

        #region Eigenschaften
        public string FileText
        {
            get { return _fileText; }
            set
            {
                SetProperty<string>(ref _fileText, value);
                _fileText = value;
            }
        }
        public static MainViewModel MVM     //Hält die MainViewModel Instanz welche beim Programmstart mit dem Aufruf von "InitializeComponent()" im MainWindow erzeugt wird.
        {
            get { return _mvm; }
        }
        public ICommand FirstCommand { get; private set; }
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
        #endregion
    }
}
