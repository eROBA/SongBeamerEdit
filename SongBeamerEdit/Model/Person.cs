using System;

//namespace MVVM_Sample.Model
namespace SongBeamerEdit.Model
{
    public class Person 
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private DateTime? _birthDate;
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _details;
        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }
    }
}
