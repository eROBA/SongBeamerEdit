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
        private Song song = new Song();
        private string _fileText = String.Empty;
        private CommandBinding _saveCommandBinding;
        private CommandBinding _openCommandBinding;
        #endregion

        #region Konstruktor
        public MainViewModel()
        {
            // Commands initialisieren
            FirstCommand = new RelayCommand(FirstExecute, BackCanExecute);

            // CommandBindings erzeugen
            _saveCommandBinding = new CommandBinding(ApplicationCommands.Save, SaveExecuted, SaveCanExecute);
            _openCommandBinding = new CommandBinding(ApplicationCommands.Open, OpenExecuted, OpenCanExecute);
        }
        #endregion
        #region Ereignisse
        public event CancelEventHandler ConfirmDeleting;
        public void OnConfirmDeleting(CancelEventArgs e)
        {
            if (ConfirmDeleting != null)
                ConfirmDeleting(this, e);
            else
                throw new Exception("Das Löschen muss bestätigt werden.");
        }
        #endregion

        #region Eigenschaften
        public string FileText
        {
            get { return _fileText; }
            set { SetProperty<string>(ref _fileText, value);
            }
        }
        public ICommand FirstCommand { get; private set; }
        public CommandBinding SaveCommandBinding
        {
            get { return _saveCommandBinding; }
        }
        public CommandBinding OpenCommandBinding
        {
            get { return _openCommandBinding; }
        }
        #endregion

        #region Methoden der CommandBindings
        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(true)
            {
                e.CanExecute = true;
                return;
            }
            else
            {
                    e.CanExecute = false;
                    return;
            } 
        }
        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //Code für Save Vorgang
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
            fileDialog.Filter = "Songbeamer Files|*.sng";
            fileDialog.DefaultExt = ".sng";
            fileDialog.InitialDirectory = Directory.Exists(path) ? path : @"C:\";
            Nullable<bool> dialogOK = fileDialog.ShowDialog();
            if (dialogOK == true)
            {
                string filetext = File.ReadAllText(fileDialog.FileName, Encoding.Default);
                return filetext;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
