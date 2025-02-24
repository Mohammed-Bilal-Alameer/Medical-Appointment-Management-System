using ClinicDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusinessLayer
{
    public class clsAppointmentsBusinessLayer
    {
        public enum enMode { AddMode = 0, UpdateMode = 1 };
        public enMode _Mode = enMode.AddMode;

        public enum enAppointmenStatus { Pending=1, Confirmed=2, Completed=3, Canceled=4 ,Rescheduled=5 ,NoShow=6 }
        public enAppointmenStatus AppointmentStatus=enAppointmenStatus.Pending;
   
        public int? AppointmentID       { set;get; }
        public int?  PatientID            { set;get; }
        public int?  DoctorID            { set;get; }
        public DateTime? StartTime { set;get; }
        public DateTime? EndTime { set;get; }
        public DateTime? AppointmentDateTime { set;get; }
        public int? MedicalRecordID     { set; get; }
        public int? PaymentID            { set;get; }


       public clsAppointmentsBusinessLayer()
        {
            AppointmentID = null;
            PatientID = null;
            DoctorID = null;
            AppointmentDateTime = null;
            StartTime = null;
            EndTime = null;
            MedicalRecordID = null;
            PaymentID = null;
            AppointmentStatus = enAppointmenStatus.Pending;
            _Mode = enMode.AddMode;
        }


        clsAppointmentsBusinessLayer( int? AppointmentID, int? PatientID, int? DoctorID, DateTime? AppointmentDateTime,DateTime? StartTime,DateTime? EndTime, enAppointmenStatus AppointmentStatus ,int? MedicalRecordID, int? PaymentID)
        {
            this.AppointmentID=AppointmentID;
            this.PatientID = PatientID;
            this.DoctorID = DoctorID;
            this.AppointmentDateTime = AppointmentDateTime;
            this.StartTime = StartTime;
            this.EndTime= EndTime;
            this.MedicalRecordID = MedicalRecordID;
            this.PaymentID = PaymentID;
           this.AppointmentStatus = AppointmentStatus;
            _Mode = enMode.UpdateMode;
        }

       static public bool UpdateAppointmentStatus(int AppointmentID, enAppointmenStatus AppointmentStatus)
        {
            return clsAppointmentsData.ChangeAppointmentStatus(AppointmentID,(int)AppointmentStatus);
        }

        

        private bool UpdateAppointment()
        {
            return clsAppointmentsData.UpdateAppointment((int)this.AppointmentID,this.PatientID,this.DoctorID,this.AppointmentDateTime,this.StartTime,this.EndTime,(int)this.AppointmentStatus,this.MedicalRecordID,this.PatientID);
        }


        public static clsAppointmentsBusinessLayer FindAppointmentByID(int AppointmentID)
        {
            bool IsFound = false;
            int? PatientID=null;
            int? DoctorID = null;
            DateTime? AppointmentDateTime = null;
            DateTime? StartTime = null;
            DateTime? EndTime = null;
            int? MedicalRecordID = null;
            int? PaymentID = null;
            int AppointmentStatus=(int)enAppointmenStatus.Pending;

            IsFound = clsAppointmentsData.FindAppointmentByID(AppointmentID, ref PatientID, ref DoctorID,ref AppointmentDateTime,ref StartTime,ref EndTime,ref AppointmentStatus,
               ref MedicalRecordID,ref PaymentID);
            if (IsFound)
            {
                return new clsAppointmentsBusinessLayer(AppointmentID, PatientID, DoctorID, AppointmentDateTime, StartTime, EndTime, (enAppointmenStatus)AppointmentStatus
                     , MedicalRecordID, PaymentID);
            }

            else
            {
                return null;
            }

        }
        private bool _AddNewAppointment()
        {
            this.AppointmentID = clsAppointmentsData.AddNewAppointment(this.PatientID, this.DoctorID, this.AppointmentDateTime,this.StartTime,this.EndTime, (decimal)this.AppointmentStatus, this.MedicalRecordID, this.PaymentID);
            return (this.AppointmentID != null);

        }

        static public bool IsThePatientHasAnAppointment(int PatientID)
        {
            return clsAppointmentsData.IsThePatientHasAnAppointment(PatientID);
        }

        public static DataTable GetAllAppointmentsForToday()
        {
            return clsAppointmentsData.GetAllAppointmentsForToday();
        }

        public static DataTable GetAllAppointments()
        {
            return clsAppointmentsData.GetAllAppointments();
        }
        public bool Save()
        {
            switch (_Mode)
            {

                case enMode.AddMode:
                    {
                        if (_AddNewAppointment())
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
                        return   UpdateAppointment() ;
                    }



            }
            return false;
        }


       static public bool DeleteAppointment(int AppointmentID)
        {
            return clsAppointmentsData.DeleteAppointment(AppointmentID);
        }

    }
}
