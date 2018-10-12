using SongBeamerEdit.Model;
using System.ComponentModel;

namespace SongBeamerEdit.ViewModel
{
    public class PersonViewModel : ViewModelBase
    {
        #region "Private Felder"
        private Person _person;
        private PString _firstName;
        private PString _lastName;
        private PString _city;
        private PDateTime _birthDate;
        private PString _details;
        private string _changed = string.Empty;
        #endregion

        #region "Konstruktor"
        public PersonViewModel(Person person)
        {
            if (person != null)
            {
                _person = person;
                initializeFields();
            }
            else
            {
                _person = new Person();
                initializeFields();
                // die folgenden Anweisungen werden benötigt, um beim Hinzufügen einer neuen Person die Änderung in der View visualisieren zu können
                _firstName.HasChanged = true;
                _lastName.HasChanged = true;
                _city.HasChanged = true;
                _details.HasChanged = true;
                _birthDate.HasChanged = true;
                Changed = "*";
            }
            _firstName.PropertyChanged += person_PropertyChanged;
            _lastName.PropertyChanged += person_PropertyChanged;
            _city.PropertyChanged += person_PropertyChanged;
            _birthDate.PropertyChanged += person_PropertyChanged;
            _details.PropertyChanged += person_PropertyChanged;
        }
        #endregion

        #region "Private Methoden"
        private void initializeFields()
        {
            _lastName = new PString(_person.LastName);
            _firstName = new PString(_person.FirstName);
            _birthDate = new PDateTime(_person.BirthDate);
            _city = new PString(_person.City);
            _details = new PString(_person.Details);
        }

        // Ereignishandler
        private void person_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_firstName.HasChanged || _lastName.HasChanged || _city.HasChanged || _birthDate.HasChanged || _details.HasChanged) 
                Changed = "*";
            else 
                Changed = string.Empty;
        }

        #endregion

        #region "Öffentliche Eigenschaften"

        public PString FirstName
        {
            get { return _firstName; }
        }

        public PString LastName
        {
            get { return _lastName; }
        }

        public PString City
        {
            get { return _city; }
        }

        public PString Details
        {
            get { return _details; }
        }

        public PDateTime BirthDate
        {
            get { return _birthDate; }
        }

        public string Changed
        {
            get { return _changed; }
            set
            {
                SetProperty<string>(ref _changed, value);
            }
        }

        #endregion

        #region "Methoden"

        public void UndoChanges()
        {
            _firstName.UndoChanges();
            _lastName.UndoChanges();
            _city.UndoChanges();
            _birthDate.UndoChanges();
            _details.UndoChanges();
        }

        public void AcceptChanges()
        {
            FirstName.AcceptChanges();
            LastName.AcceptChanges();
            City.AcceptChanges();
            Details.AcceptChanges();
            BirthDate.AcceptChanges();
        }

        #endregion
    }
}
