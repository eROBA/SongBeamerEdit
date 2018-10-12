using System;

namespace SongBeamerEdit.ViewModel
{
    public class PString : ViewModelBase, IComparable
    {
        private string _currentValue;
        private string _originalValue;
        private bool _hasChanged;

        public bool HasChanged
        {
            get { return _hasChanged; }
            set
            {
                SetProperty<bool>(ref _hasChanged, value);
            }
        }

        public PString(string value)
        {
            if (value == string.Empty) value = null;
            _currentValue = value;
            _originalValue = value;
        }

        public string Value
        {
            get { return _currentValue; }
            set
            {
                if (value == "") value = null;
                SetProperty<string>(ref _currentValue, value);
                HasChanged = _currentValue != _originalValue;
            }
        }

        public void UndoChanges()
        {
            Value = _originalValue;
            HasChanged = false;
        }

        public void AcceptChanges()
        {
            _originalValue = _currentValue;
            HasChanged = false;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (object.ReferenceEquals(this, obj)) return 0;
            PString item = obj as PString;
            return String.Compare(_currentValue, item._currentValue);
        }
    }
}
