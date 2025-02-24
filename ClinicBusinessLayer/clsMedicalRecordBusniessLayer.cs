using ClinicDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusinessLayer
{
    public class clsMedicalRecordBusniessLayer
    {
        enum enMode { AddMode = 0, UpdateMode = 1 };
        enMode _Mode = enMode.AddMode;

        public Nullable<int> MedicalRecordID { set; get; }

        public string VisitDescription { set; get; }
        public string Diagnosis { set; get; }
        public string AdditionalNotes { set; get; }

        public clsMedicalRecordBusniessLayer()
        {
            MedicalRecordID = null;

            VisitDescription = null;
            Diagnosis = null;
            AdditionalNotes = null;
            _Mode = enMode.AddMode;





        }
        private clsMedicalRecordBusniessLayer(int MedicalRecordID,  string VisitDescription, string Diagnosis, string AdditionalNotes)
        {
            this.MedicalRecordID = MedicalRecordID;
            this.VisitDescription = VisitDescription;
            this.Diagnosis = Diagnosis;
            this.AdditionalNotes = AdditionalNotes;
        

            _Mode = enMode.UpdateMode;

        }



        private bool _AddNewMedicalRecord()
        {
            this.MedicalRecordID = clsMedicalRecordData.AddNewMedicalRecord( VisitDescription, Diagnosis, AdditionalNotes);
            return (this.MedicalRecordID != null);

        }

        private bool _UpdateMedicalRecord()
        {
            return clsMedicalRecordData.UpdateMedicalRecordInfo((int)this.MedicalRecordID, this.VisitDescription,this.Diagnosis,this.AdditionalNotes);
        }



        public static clsMedicalRecordBusniessLayer GetMedicalRecordInfo(int MedicalRecordID)
        {
            bool IsFound;
            string VisitDescription = null;
            string Diagnosis = null;
            string AdditionalNotes = null;

            IsFound = clsMedicalRecordData.FindMedicalRecordByID(MedicalRecordID, ref VisitDescription, ref Diagnosis, ref AdditionalNotes);
            if (IsFound)
            {

                return new clsMedicalRecordBusniessLayer(MedicalRecordID, VisitDescription, Diagnosis, AdditionalNotes);
            }
            else
            {
                return null;
            }


        }
      

        public static bool IsMedicalRecordExist(int MedicalRecordID)
        {
            return clsMedicalRecordData.IsMedicalRecordExistByID(MedicalRecordID);
        }


   

        public bool Save()
        {
         
            switch (_Mode)
            {

                case enMode.AddMode:
                    {
                        if (_AddNewMedicalRecord())
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
                        return _UpdateMedicalRecord();
                    }



            }
            return false;
        }




    }
}
