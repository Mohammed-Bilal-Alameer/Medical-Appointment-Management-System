using ClinicDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusinessLayer
{
    public class clsPatientBusinessLayer: clsPersonBusinessLayer
    {
        enum enMode { AddMode = 0, UpdateMode = 1 };
        enMode _Mode = enMode.AddMode;



        public int? PatientID { get; set; }
        public string Allergies { get; set; }
        public string Notes { get; set; }

        public clsPatientBusinessLayer()
        {
            PatientID = null;

            Allergies = null;
            Notes = null;
     
            _Mode = enMode.AddMode;





        }

        private clsPatientBusinessLayer( int? PersonID,int? PatientID, string Allergies, string Notes,string FirstName,string LastName,DateTime DateOfBirth, bool Gender, string PhoneNumber, string Email, string Address)
        {
            _Mode = enMode.UpdateMode;
            this.PatientID = PatientID;
            this.Allergies = Allergies;
            this.Notes = Notes;


            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Address = Address;



        }


        private bool _AddNewPatient()
        {
            this.PatientID = clsPatientsData.AddNewPatient(PersonID, Allergies, Notes);
            return (this.PatientID != null);

        }

        public static clsPatientBusinessLayer GetPatientInfo(int PatientID)
        {
            bool IsFound;
            
            int PersonID = -1;
            string Allergies = null;
            string Notes = null;

            IsFound = clsPatientsData.FindPatientByID(PatientID,ref PersonID, ref Allergies,ref Notes);
            if (IsFound)
            {
                clsPersonBusinessLayer PersonInfo = clsPersonBusinessLayer.Find((int)PersonID);

                return new clsPatientBusinessLayer((int)PersonID, PatientID, Allergies, Notes,  PersonInfo.FirstName, PersonInfo.LastName,
                    (DateTime)PersonInfo.DateOfBirth, PersonInfo.Gender, PersonInfo.PhoneNumber,
                    PersonInfo.Email, PersonInfo.Address);
            }
            else
            {
                return null;
            }


        }



        public static DataTable GetAllPatients()
        {
            return clsPatientsData.GetAllPatients();
        }


        private bool _UpdatePatient()
        {
            return clsPatientsData.UpdatePatient((int)PatientID,PersonID, Allergies, Notes);
        }


        public static bool DeletePatientAndPerson(int PatientID)
        {
            return clsPatientsData.DeletePatient(PatientID);
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
                        if (_AddNewPatient())
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
                        return _UpdatePatient();
                    }



            }
            return false;
        }




    }
}
