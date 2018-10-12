using System;

namespace SongBeamerEdit.ViewModel
{
    public class PDateTime : ViewModelBase, IComparable
    {
        private DateTime? _currentValue;
        private DateTime? _originalValue;
        private bool _hasChanged;
        private bool _hasError;
        private string _errorValue;

        public PDateTime(DateTime? date)
        {
            _currentValue = date;
            _originalValue = date;
        }

        public string Value
        {
            get 
            {
                if (HasError) return _errorValue;
                if (!_currentValue.HasValue) return null; 
                return _currentValue.Value.ToShortDateString();
            }

            set
            {
                DateTime newValue;
                HasError = false;
                if (value == string.Empty)
                    SetProperty(ref _currentValue, null);
                else if (DateTime.TryParse(value, out newValue))
                    SetProperty(ref _currentValue, newValue);
                else
                {
                    _errorValue = value;
                    HasError = true;
                }
                HasChanged = _currentValue != _originalValue;
            }
        }

        public bool HasChanged
        {
            get { return _hasChanged; }
            set
            {
                SetProperty<bool>(ref _hasChanged, value);
            }
        }

        public bool HasError
        {
            get { return _hasError; }
            set
            {
                SetProperty<bool>(ref _hasError, value);
            }
        }

        public void UndoChanges()
        {
            _currentValue = _originalValue;
            HasChanged = false;
            if (_originalValue == null)
                Value = string.Empty;
            else
                Value = _currentValue.Value.ToShortDateString();
        }

        public DateTime? CurrentValue
        {
            get { return _currentValue; }
        }

        public void AcceptChanges()
        {
            _originalValue = _currentValue;
            HasChanged = false;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            PDateTime item = obj as PDateTime;
            if (item == null) throw new ArgumentOutOfRangeException("Falscher Datentyp.");
            if (_currentValue == null && item._currentValue == null) return 0;
            if (_currentValue == null) return -1;
            if (item._currentValue == null) return 1;

            return ((IComparable)_currentValue).CompareTo(item._currentValue);
        }
    }
}
