using ClinicBusinessLayer;
using Simple_Clinic.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Clinic.Medical_Records
{
    public partial class frmAddEditMedicalRecord : Form
    {
        public enum enMode { AddMode = 0, UpdateMode = 1 };
        public enMode _Mode = enMode.AddMode;
        int _MedicalRecordID;
        clsMedicalRecordBusniessLayer _MedicalRecord;
        clsAppointmentsBusinessLayer _Appointment;
        public frmAddEditMedicalRecord(clsAppointmentsBusinessLayer Appointment)
        {
            _Appointment=Appointment;
            InitializeComponent();
            _Mode=enMode.AddMode;
        }

        public frmAddEditMedicalRecord(int MedicalRecordID, clsAppointmentsBusinessLayer Appointment)
        {
            _Appointment = Appointment;

            InitializeComponent();
            _MedicalRecordID= MedicalRecordID;
            _Mode = enMode.UpdateMode;

        }

        private void _ResetToDefaultValues()
        {

            if (_Mode == enMode.AddMode)
            {
                _MedicalRecord = new clsMedicalRecordBusniessLayer();
               
                lblHeader.Text = "Add New Medical Record";
            }
            else
            {
                lblHeader.Text = "Update Medical Record";


            }



        }


        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _MedicalRecord.Diagnosis = txtDiagnosis.Text;
            _MedicalRecord.VisitDescription = txtVisitDescription.Text;
            _MedicalRecord.AdditionalNotes = txtAdditionalNotes.Text;

            if (_MedicalRecord.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAdd.Enabled = false;
                lblMedicalRecordID.Text=_MedicalRecord.MedicalRecordID.ToString();
            
                _Appointment.MedicalRecordID = _MedicalRecord.MedicalRecordID;
                _Appointment.Save();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void _LoadMedicalRecordData()
        {
            _MedicalRecord = clsMedicalRecordBusniessLayer.GetMedicalRecordInfo(_MedicalRecordID);
            if (_MedicalRecord != null)
            {
               txtAdditionalNotes.Text= _MedicalRecord.AdditionalNotes;
               txtDiagnosis.Text= _MedicalRecord.Diagnosis;
               txtVisitDescription.Text= _MedicalRecord.VisitDescription;
                lblMedicalRecordID.Text=_MedicalRecord.MedicalRecordID.ToString() ;


            }


        }


        private void frmAddEditMedicalRecord_Load(object sender, EventArgs e)
        {
            _ResetToDefaultValues();
            if(_Mode == enMode.UpdateMode)
            {
                btnAdd.Text = "Update";
                _LoadMedicalRecordData();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
