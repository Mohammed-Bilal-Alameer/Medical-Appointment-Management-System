using ClinicDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusinessLayer
{
    public class clsDoctorBusinessLayer:clsPersonBusinessLayer
    {
        enum enMode { AddMode = 0, UpdateMode = 1 };
        enMode _Mode = enMode.AddMode;

        public Nullable<int> DoctorID { set; get; }

        public string Username { set; get; }
        public string Password { set; get; }
        public string Specialization { set; get; }
        
        public clsDoctorBusinessLayer()
        {
            DoctorID = null;

            Specialization = null;
            Username = null;
            Password = null;
            _Mode = enMode.AddMode;





        }
        private clsDoctorBusinessLayer(int DoctorID,int PersonID, string Specialization,string Username,string Password, string FirstName,string LastName, DateTime? DateOfBirth, bool Gender, string PhoneNumber, string Email, string Address)
        {
            this.DoctorID = DoctorID;
            this.PersonID = PersonID;
            this.Specialization = Specialization;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Address = Address;
            this.Username = Username;
            this.Password = Password;

            _Mode = enMode.UpdateMode;

        }



        private bool _AddNewDoctor()
        {
            this.DoctorID = clsDoctorsData.AddNewDoctor((int)PersonID,Specialization,Username,Password);
            return (this.PersonID != null);

        }

        private bool _UpdateDoctor()
        {
            return clsDoctorsData.UpdateDoctorInfo((int)this.DoctorID,(int)this.PersonID,this.Specialization);
        }



        public static clsDoctorBusinessLayer GetDoctorInfo(int DoctorID)
        {
            bool IsFound;
            Nullable<int> PersonID = null;
            string Specialization = null;
            string Username = null;
            string Password = null;

            IsFound = clsDoctorsData.Find(DoctorID, ref PersonID, ref Specialization, ref Username, ref Password);
            if (IsFound)
            {
                clsPersonBusinessLayer PersonInfo = clsPersonBusinessLayer.Find((int)PersonID);

                return new clsDoctorBusinessLayer(DoctorID,(int) PersonID, Specialization, Username, Password, PersonInfo.FirstName, PersonInfo.LastName,
                    PersonInfo.DateOfBirth, PersonInfo.Gender, PersonInfo.PhoneNumber,
                    PersonInfo.Email, PersonInfo.Address);
            }
            else
            {
                return null;
            }


        }
        public static clsDoctorBusinessLayer GetDoctorInfo(string Username)
        {
            bool IsFound;
            Nullable<int> PersonID = null;
            string Specialization = null;
            Nullable<int> DoctorID = null;
            string Password = null;

            IsFound = clsDoctorsData.FindByUserName(ref DoctorID, ref PersonID, Username , ref Password, ref Specialization);
            if (IsFound)
            {
                clsPersonBusinessLayer PersonInfo = clsPersonBusinessLayer.Find((int)PersonID);

                return new clsDoctorBusinessLayer((int)DoctorID, (int)PersonID, Specialization, Username, Password, PersonInfo.FirstName, PersonInfo.LastName,
                    PersonInfo.DateOfBirth, PersonInfo.Gender, PersonInfo.PhoneNumber,
                    PersonInfo.Email, PersonInfo.Address);
            }
            else
            {
                return null;
            }


        }


        public static bool IsUsernameExist(string Username)
        {
            return clsDoctorsData.IsUsernameExist(Username);
        }


        public bool ChangePassword(string Password,int DoctorID)
        {
            return clsDoctorsData.ChangePassword(Password, DoctorID);
        }

        public bool Save()
        {
            base._Mode = (clsPersonBusinessLayer.enMode)_Mode;
            if (!base.Save())
                return false;



            switch (_Mode)
            {

                case enMode.AddMode:
                    {
                        if (_AddNewDoctor())
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
                        return _UpdateDoctor();
                    }



            }
            return false;
        }



    }
}
