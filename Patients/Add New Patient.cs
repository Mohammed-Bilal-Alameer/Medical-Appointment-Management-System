using ClinicBusinessLayer;
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

namespace Simple_Clinic.Patients
{
    public partial class frmAddNewPatient : Form
    {
        public event Action<object,int?, string> SendPatientNameBack;


        enum enMode { AddMode = 0, UpdateMode = 1 };
        enMode _Mode = enMode.AddMode;
        bool _IsCameFromAppointment;

        private int? _PatientID;

        clsPatientBusinessLayer _NewPatient;

        public frmAddNewPatient(bool isCameFromAppointment)
        {
            InitializeComponent();
            _Mode = enMode.AddMode;
            _IsCameFromAppointment = isCameFromAppointment;
        }
        public frmAddNewPatient(int PatientID)
        {
            InitializeComponent();
            _Mode = enMode.UpdateMode;
            _PatientID = PatientID;
        }

        private void _ResetToDefaultValues()
        {

            if (_Mode == enMode.AddMode)
            {
                _NewPatient = new clsPatientBusinessLayer();
                lblHeader.Text = "Add Patient Information";
            }
            else
            {
                lblHeader.Text = "Update Patient Information";


            }



        }
        private void _LoadPatientData()
        {
            _NewPatient = clsPatientBusinessLayer.GetPatientInfo((int)_PatientID);
            if (_NewPatient != null)
            {
                txtAddress.Text = _NewPatient.Address;
                txtAllergies.Text= _NewPatient.Allergies;
                txtEmail.Text = _NewPatient.Email;
                txtFirstName.Text = _NewPatient.FirstName;
                txtLastName.Text = _NewPatient.LastName;
                txtNotes.Text = _NewPatient.Notes;
                txtPhonbeNumber.Text = _NewPatient.PhoneNumber;
                dtpDateOfBirth.Value = _NewPatient.DateOfBirth.Value;
            
                if (_NewPatient.Gender)
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;

                }
            }


        }

        private void chbAllergies_CheckedChanged(object sender, EventArgs e)
        {
            if(chbAllergies.Checked)
            {
                lblAllergies.Visible = true;
                txtAllergies.Visible=true;
            }
            else
            {
                lblAllergies.Visible = false ;
                txtAllergies.Visible = false;

            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddNewPatient_Load(object sender, EventArgs e)
        {
            _ResetToDefaultValues();
            if (_Mode == enMode.UpdateMode)
            {
                _LoadPatientData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _NewPatient.FirstName = txtFirstName.Text;
            _NewPatient.LastName = txtLastName.Text;
            _NewPatient.Email = txtEmail.Text;
            _NewPatient.Notes = txtNotes.Text;
            _NewPatient.PhoneNumber = txtPhonbeNumber.Text;
            _NewPatient.Address= txtAddress.Text;
            if(chbAllergies.Checked )
            {
                _NewPatient.Allergies = txtAllergies.Text;
            }
            else
            {
                _NewPatient.Allergies = null;
            }
            if (rbMale.Checked)
            {
                _NewPatient.Gender = true;
            }
            else
            {
                _NewPatient.Gender = false;
            }
            _NewPatient.DateOfBirth = dtpDateOfBirth.Value;

            if (clsPersonBusinessLayer.IsEmailExist(txtEmail.Text))
            {
                MessageBox.Show("This Email already Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtLastName.Text)|| string.IsNullOrEmpty(txtFirstName.Text)|| string.IsNullOrEmpty(txtPhonbeNumber.Text))
            {
                MessageBox.Show("Some required field is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                if (_NewPatient.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                _PatientID = _NewPatient.PatientID;
                if (_IsCameFromAppointment)
                {
                    SendPatientNameBack.Invoke(this, _NewPatient.PatientID, _NewPatient.FullName);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();
                errorProvider1.SetError(txtFirstName, "This Faild is Empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFirstName, "");

            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                e.Cancel = true;
                txtLastName.Focus();
                errorProvider1.SetError(txtLastName, "This Faild is Empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLastName, "");

            }
        }

        private void txtPhonbeNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhonbeNumber.Text))
            {
                e.Cancel = true;
                txtLastName.Focus();
                errorProvider1.SetError(txtPhonbeNumber, "This Faild is Empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPhonbeNumber, "");

            }
        }
    }
}
