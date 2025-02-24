using ClinicDataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusinessLayer
{
    public class PrescriptionsBusinsesslayer
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode = enMode.AddNew;
        public Nullable<int> PrescriptionID { get; set; }
        public Nullable<int> MedicalRecordID { get; set; }

        public string MedicationName { get; set; }
        public string Dosage { get; set; }

        public string Frequency { get; set; }
        public Nullable<DateTime> StartDate { get; set; }

        public Nullable<DateTime> EndDate { get; set; }

        public string SpecialInstructions { get; set; }



        public PrescriptionsBusinsesslayer()
        {
            PrescriptionID = null;
            MedicalRecordID = null;
            MedicationName = null;
            Dosage = null;
            Frequency = null;
            StartDate = null;
            EndDate = null;
            SpecialInstructions = null;
            _Mode = enMode.AddNew;
        }

        private PrescriptionsBusinsesslayer(int PrescriptionID, int MedicalRecordID, string MedicationName, string Dosage, string Frequency, DateTime StartDate, DateTime EndDate, string SpecialInstructions)
        {
            this.PrescriptionID = PrescriptionID;
            this.MedicalRecordID = MedicalRecordID;
            this.MedicationName = MedicationName;
            this.Dosage = Dosage;
            this.Frequency = Frequency;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.SpecialInstructions = SpecialInstructions;
            _Mode = enMode.Update;
        }

        private bool _AddNewPrescription()
        {
            this.PrescriptionID = PrescriptionsData.AddNewPrescription((int)MedicalRecordID, MedicationName, Dosage, Frequency, (DateTime)StartDate, (DateTime)EndDate, SpecialInstructions);
            return (this.PrescriptionID != null);

        }


        private bool _UpdatePrescription()
        {
            return PrescriptionsData.UpdatePrescriptionInfo((int)PrescriptionID, (int)this.MedicalRecordID, this.MedicationName, this.Dosage, this.Frequency, (DateTime)this.StartDate, (DateTime)this.EndDate, this.SpecialInstructions);
        }



        public static bool IsPrescriptionExistByMedicalRecordID(int MedicalRecordID)
        {
            return PrescriptionsData.IsPrescriptionExistByID(MedicalRecordID);
        }







        public bool Save()
        {

            switch (_Mode)
            {

                case enMode.AddNew:
                    {
                        if (_AddNewPrescription())
                        {


                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    {
                        return _UpdatePrescription();
                    }



            }
            return false;
        }




    }
}
