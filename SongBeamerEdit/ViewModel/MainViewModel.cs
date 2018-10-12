using SongBeamerEdit.Command;
using SongBeamerEdit.Model;
using System;
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
        #region "Private Felder"
        private ObservableCollection<PersonViewModel> _personsList;
        private string _actualPosition;
        private ListCollectionView _personsView;
        private CommandBinding _newCommandBinding;
        private CommandBinding _deleteCommandBinding;
        private CommandBinding _saveCommandBinding;
        private CommandBinding _undoCommandBinding;
        private string _personDetails;
        private bool _ListChanged;
        private bool _setFocus;
        private static string[] _sortCriteria = { "LastName", "FirstName", "BirthDate", "City" };
        private string _sortByProperty = _sortCriteria[0];

        #endregion

        #region "Öffentliche Eigenschaften"
        public string[] SortCriteria
        {
            get { return _sortCriteria; }
        }

        public string SortByProperty
        {
            get { return _sortByProperty; }
            set
            {
                if (value != _sortByProperty)
                {
                    SetProperty(ref _sortByProperty, value);
                    UpdateSorting();
                }
            }
        }

        public bool SetFocus
        {
            get { return _setFocus; }
            set { SetProperty<bool>(ref _setFocus, value); }
        }

        public bool ListChanged
        {
            get { return _ListChanged;  }
            set
            {
                if (_ListChanged != value)
                    SetProperty<bool>(ref _ListChanged, value);
            }
        }

        public string PersonDetails
        {
            get { return _personDetails; }
            set { SetProperty<string>(ref _personDetails, value); }
        }

        public ListCollectionView PersonsView
        {
            get { return _personsView; }
        }

        public string ActualPosition
        {
            get { return _actualPosition; }
            private set
            {
                SetProperty<string>(ref _actualPosition, value);
            }
        }

        public ICommand FirstCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand LastCommand { get; private set; }
        public ICommand MouseEnterCommand { get; private set; }
        public ICommand MouseLeaveCommand { get; private set; }

        public CommandBinding NewCommandBinding
        {
            get { return _newCommandBinding; }
        }

        public CommandBinding SaveCommandBinding
        {
            get { return _saveCommandBinding; }
        }

        public CommandBinding DeleteCommandBinding
        {
            get { return _deleteCommandBinding; }
        }

        public CommandBinding UndoCommandBinding
        {
            get { return _undoCommandBinding; }
        }
        #endregion

        #region "Konstruktor"
        public MainViewModel()
        {
            // Personenliste füllen
            _personsList = new ObservableCollection<PersonViewModel>();
            LoadPersons(ref _personsList);

            // ListCollectionView initialisieren
            _personsView = new ListCollectionView(_personsList);
            _personsView.CurrentChanged += _persons_CurrentChanged;

            // Commands initialisieren
            FirstCommand = new RelayCommand(FirstExecute, BackCanExecute);
            PreviousCommand = new RelayCommand(PreviousExecute, BackCanExecute);
            NextCommand = new RelayCommand(NextExecute, ForwardCanExecute);
            LastCommand = new RelayCommand(LastExecute, ForwardCanExecute);

            // CommandBindings erzeugen
            _newCommandBinding = new CommandBinding(ApplicationCommands.New, NewExecuted, NewCanExecute);
            _deleteCommandBinding = new CommandBinding(ApplicationCommands.Delete, DeleteExecuted, DeleteCanExecute);
            _saveCommandBinding = new CommandBinding(ApplicationCommands.Save, SaveExecuted, SaveCanExecute);
            _undoCommandBinding = new CommandBinding(ApplicationCommands.Undo, UndoExecuted, UndoCanExecute);

            MouseEnterCommand = new RelayCommand(MouseEnterExecute, MouseEnterCanExecute);
            MouseLeaveCommand = new RelayCommand(MouseLeaveExecute, MouseLeaveCanExecute);
            UpdateSorting();
            _personsView.MoveCurrentToFirst();
        }
        #endregion

        #region "Methoden der CommandBindings"
        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(ListChanged)
            {
                e.CanExecute = true;
                return;
            }
            foreach (PersonViewModel item in _personsView)
            {
                if (item.Changed == "*")
                {
                    e.CanExecute = true;
                    return;
                }
            }
            e.CanExecute = false;
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ObservableCollection<Person> person = new ObservableCollection<Person>();
            foreach(PersonViewModel item in _personsList)
            {
                Person p = new Person();
                p.FirstName = item.FirstName.Value;
                p.LastName = item.LastName.Value;
                p.City = item.City.Value;
                p.Details = item.Details.Value;
                p.BirthDate = item.BirthDate.CurrentValue;
                person.Add(p);
            }
            FileStream fs = new FileStream("Persons.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Person>));
            serializer.Serialize(fs, person);
            fs.Close();
            AcceptChanges();
            ListChanged = false;
        }

        private void DeleteCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = PersonsView.Count > 0;
        }

        private void DeleteExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CancelEventArgs arg = new CancelEventArgs();
            OnConfirmDeleting(arg);
            if (arg.Cancel == true) return;
            PersonViewModel persDelete = PersonsView.CurrentItem as PersonViewModel;
            if (persDelete != null)
                _personsList.Remove(persDelete);
            _personsView.MoveCurrentToFirst();
            ListChanged = true;
        }

        private void NewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PersonViewModel person = new PersonViewModel(null);
            _personsList.Add(person);
            _personsView.MoveCurrentTo(person);
            SetFocus = true;
        }

        private void UndoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((_personsView).Count == 0)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = ((PersonViewModel)_personsView.CurrentItem).Changed == "*" || ((PersonViewModel)_personsView.CurrentItem).BirthDate.HasError;
        }

        private void UndoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ((PersonViewModel)_personsView.CurrentItem).UndoChanges();
        }
        #endregion

        #region "Methoden für Commands"
        private bool MouseLeaveCanExecute(object arg)
        {
            return true;
        }

        private void MouseLeaveExecute(object obj)
        {
            ((PersonViewModel)_personsView.CurrentItem).Details.Value = _personDetails;
            PersonDetails = string.Empty;
        }

        private bool MouseEnterCanExecute(object arg)
        {
            return true;
        }

        private void MouseEnterExecute(object obj)
        {
            PersonViewModel selPerson = _personsView.CurrentItem as PersonViewModel;
            if (selPerson != null)
                PersonDetails = selPerson.Details.Value;
        }

        private bool BackCanExecute(object obj)
        {
            return _personsView.CurrentPosition > 0;
        }

        private bool ForwardCanExecute(object obj)
        {
            return _personsView.CurrentPosition < _personsView.Count - 1;
        }

        private void FirstExecute(object obj)
        {
            _personsView.MoveCurrentToFirst();
        }

        private void PreviousExecute(object obj)
        {
            _personsView.MoveCurrentToPrevious();
        }

        private void NextExecute(object obj)
        {
            _personsView.MoveCurrentToNext();
        }

        private void LastExecute(object obj)
        {
            _personsView.MoveCurrentToLast();
        }
        #endregion

        #region "Öffentliche Methoden"
        public void CancelViewClosing()
        {
            bool hasChanged = false;
            if (ListChanged)
                hasChanged = true;
            else
                foreach (PersonViewModel item in _personsView)
                    if (item.Changed == "*") hasChanged = true;
            if(hasChanged)
            {
                string message = "Sollen die Änderungen gespeichert werden?";
                string caption = "MVVM_Sample";
                MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    SaveExecuted(this, null);
            }
        }
        #endregion

        #region "Private Methoden"

        private void UpdateSorting()
        {
            PersonsView.SortDescriptions.Clear();
            PersonsView.SortDescriptions.Add(new SortDescription(this.SortByProperty, ListSortDirection.Ascending));
        }

        private void LoadPersons(ref ObservableCollection<PersonViewModel> liste)
        {
            // XML-Datei deserialisieren
            if (File.Exists("Persons.xml"))
            {
                FileStream fs = new FileStream("Persons.xml", FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Person>));
                var tempListe = (ObservableCollection<Person>)serializer.Deserialize(fs);
                foreach (var item in tempListe)
                    liste.Add(new PersonViewModel(item));
                fs.Close();
            }
        }

        private void _persons_CurrentChanged(object sender, EventArgs e)
        {
            ActualPosition = "Datensatz " + (_personsView.CurrentPosition + 1) + " von " + _personsView.Count;
        }

        private void AcceptChanges()
        {
            foreach (PersonViewModel item in _personsView)
                item.AcceptChanges();
        }

        #endregion
    }
}
