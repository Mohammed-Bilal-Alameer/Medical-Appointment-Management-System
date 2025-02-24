using ClinicBusinessLayer;
using Simple_Clinic.Global_Classes;
using Simple_Clinic.Medical_Records;
using Simple_Clinic.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace Simple_Clinic.Appointments
{
    public partial class frmAddAppointment : Form
    {
        public enum enMode { AddMode = 0, UpdateMode = 1 };
        public enMode _Mode = enMode.AddMode;

        clsAppointmentsBusinessLayer _NewAppointment;
        clsPatientBusinessLayer _NewPatient;
        clsPersonBusinessLayer _PersonInfo;
        int _AppointmentID;
        int _PatientID ;
        int _DoctorID;


        public frmAddAppointment()
        {
            InitializeComponent();
            _Mode = enMode.AddMode;
            _DoctorID =(int) clsGlobal._CurrentDoctor.DoctorID;
        }
        public frmAddAppointment(int AppointmentID)
        {
            InitializeComponent();
            _Mode = enMode.UpdateMode;
            _AppointmentID=AppointmentID;

        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ResetToDefaultValues()
        {

            if (_Mode == enMode.AddMode)
            {
                _NewAppointment = new clsAppointmentsBusinessLayer();
                _NewPatient = new clsPatientBusinessLayer();
                dptEndTime.MinDate = DateTime.Now;
                dtpStartTime.MinDate = DateTime.Now;
                dtpAppointmentTime.MinDate = DateTime.Now;
                lblHeader.Text = "Add New Appointment";
                ctrlDoctorInfo.LoadPersonInfo((int)clsGlobal._CurrentDoctor.PersonID);
                _DoctorID = (int)clsGlobal._CurrentDoctor.DoctorID;
            }
            else
            {
                lblHeader.Text = "Update Appointment";


            }



        }



        private void frmAddAppointment_Load(object sender, EventArgs e)
        {
           
            dptEndTime.Format = DateTimePickerFormat.Time;
            dptEndTime.CustomFormat = "HH/mm";
            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.CustomFormat = "HH/mm";
            dptEndTime.ShowUpDown = true;
            dtpStartTime.ShowUpDown = true;
           
            _ResetToDefaultValues();
            if (_Mode == enMode.UpdateMode)
            {
                comboBox1.Enabled = false;

                _LoadAppointmentData();
            }

        }

        private void _LoadAppointmentData()
        {
            _NewAppointment = clsAppointmentsBusinessLayer.FindAppointmentByID(_AppointmentID);
            if (_NewAppointment != null)
            {
                clsDoctorBusinessLayer _Doctor = clsDoctorBusinessLayer.GetDoctorInfo((int)_NewAppointment.DoctorID);
                clsPatientBusinessLayer _Patient = clsPatientBusinessLayer.GetPatientInfo((int)_NewAppointment.PatientID);
                ctrlDoctorInfo.LoadPersonInfo((int)_Doctor.PersonID);
                ctrlPatientInfo.LoadPersonInfo((int)_Patient.PersonID);
                _PatientID =(int) _NewAppointment.PatientID;
                _DoctorID = (int)_NewAppointment.DoctorID;
                dtpAppointmentTime.Value=_NewAppointment.AppointmentDateTime.Value;
                dtpStartTime.Value=_NewAppointment.StartTime.Value;
                dptEndTime.Value = _NewAppointment.EndTime.Value;
                dtpAppointmentTime.MinDate = DateTime.Today;
                btnAddNewMedicalRecord.Enabled = true;

                if (_NewAppointment.MedicalRecordID != null)
                {
                    
                        btnAddNewMedicalRecord.Text = "Update";
                    
                }
                
                else
                {
                    btnAddNewMedicalRecord.Text = "Add New";

                }

            }


        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                frmAddNewPatient frm= new frmAddNewPatient(true);
                frm.SendPatientNameBack += _FillPatientNameFromDeleget;
                frm.ShowDialog();
                btnAddNewMedicalRecord.Enabled = true;
            }
            if(comboBox1.SelectedIndex == 1)
            {
                frmListOfAllPatients frm= new frmListOfAllPatients(true);
                frm.SendPatienNameAndIDBack += _FillPatientNameAndIDFromDeleget;
                frm.ShowDialog();

            }
        }

        private void _FillPatientNameFromDeleget(object sender, int? obj1,string obj2)
        {
            _PatientID =(int) obj1;
            _NewPatient = clsPatientBusinessLayer.GetPatientInfo(_PatientID);   
            ctrlPatientInfo.LoadPersonInfo((int)_NewPatient.PersonID);

        }

        private void _FillPatientNameAndIDFromDeleget(object sender,int PatientID, string obj2)
        {
            _PatientID = PatientID;
            _NewPatient=clsPatientBusinessLayer.GetPatientInfo(_PatientID);
            ctrlPatientInfo.LoadPersonInfo((int)_NewPatient.PersonID);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            _NewAppointment.DoctorID =_DoctorID;
            _NewAppointment.PatientID = _PatientID;
            _NewAppointment.AppointmentDateTime=dtpAppointmentTime.Value;
            _NewAppointment.StartTime = dtpStartTime.Value;
            _NewAppointment.EndTime = dptEndTime.Value;
            _NewAppointment.AppointmentStatus = clsAppointmentsBusinessLayer.enAppointmenStatus.Pending;
            _NewAppointment.MedicalRecordID = null;
            _NewAppointment.PaymentID = null;
            if (_NewAppointment.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                comboBox1.Enabled = false;
                btnAddNewMedicalRecord.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewMedicalRecord_Click(object sender, EventArgs e)
        {
            if (_NewAppointment.MedicalRecordID!=null)
            {
                frmAddEditMedicalRecord Updatefrm = new frmAddEditMedicalRecord((int)_NewAppointment.MedicalRecordID,_NewAppointment);
                Updatefrm.ShowDialog();
            }
            else
            {
                frmAddEditMedicalRecord Newfrm = new frmAddEditMedicalRecord(_NewAppointment);
                Newfrm.ShowDialog();

            }


        }
    }
}
