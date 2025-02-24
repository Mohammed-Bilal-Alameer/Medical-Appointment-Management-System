using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClinicDataLayer;
namespace ClinicBusinessLayer
{
    public class clsPersonBusinessLayer
    {
       public enum enMode { AddMode=0,UpdateMode=1};
        public enMode _Mode=enMode.AddMode;


        public Nullable<int> PersonID { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string FullName
        {
            get { return FirstName+ " " + LastName; }
        }
        public Nullable<DateTime> DateOfBirth { set; get; }
        public bool Gender { set; get; }

        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }

        private clsPersonBusinessLayer( int? PersonID, string FirstName, string LastName, DateTime? DateOfBirth, bool Gender, string PhoneNumber, string Email, string Address)
        {
            _Mode = enMode.UpdateMode;
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Address = Address;
        }

        public clsPersonBusinessLayer() {
            PersonID = null;
            FirstName = null;
            LastName = null;
            DateOfBirth = null;
            Gender = true;
            PhoneNumber = null;
            Email = null;
            Address = null;
            _Mode = enMode.AddMode;

        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.LastName, this.DateOfBirth, this.Gender, this.PhoneNumber, this.Email, this.Address);
            return (this.PersonID != null);

        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson((int)this.PersonID, this.FirstName, this.LastName, this.DateOfBirth, this.Gender, this.PhoneNumber, this.Email, this.Address);
        }


        public static clsPersonBusinessLayer Find(int PersonID)
        {
            bool IsFound = false;
            string FirstName = null;
            string LastName = null;
            DateTime? DateOfBirth = null;
            bool Gender = false;
            string PhoneNumber = null;
            string Email = null;
            string Address = null;

            IsFound = clsPersonData.Find(PersonID, ref FirstName, ref LastName, ref DateOfBirth, ref Gender, ref PhoneNumber, ref Email, ref Address);
                if(IsFound)
            {
                return new clsPersonBusinessLayer(PersonID, FirstName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address);
            }

            else
            {
                return null;
            }

        }
        public static bool IsEmailExist(string Email)
        {
            return clsPersonData.IsEmailExist(Email);
        }

       

        public bool Save()
        {
            switch (_Mode)
            {

                case enMode.AddMode:
                    {
                        if (_AddNewPerson())
                        {

                      
                        _Mode = enMode.UpdateMode;
                        return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.UpdateMode:
                    {
                        return _UpdatePerson();
                    }



            }
            return false;
        }

    }
}
