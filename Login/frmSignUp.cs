using ClinicBusinessLayer;
using Guna.UI2.WinForms;
using Simple_Clinic.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Clinic.Login
{
    public partial class frmSignUp : Form
    {
        enum enMode { AddMode = 0, UpdateMode = 1 };
        enMode _Mode = enMode.AddMode;


        //public delegate void SendDataBack(object sender,string Username);
        //public event SendDataBack BackUsername;
        public event Action<object, string> SendUserNameBackToLoginScreen;

   
     
       


        private int _DoctorID;

        clsDoctorBusinessLayer _NewAccount;
        public frmSignUp()
        {
            InitializeComponent();
            _Mode = enMode.AddMode;
        }
        public frmSignUp(int DoctorID)
        {
            InitializeComponent();
            _Mode = enMode.UpdateMode;
            _DoctorID = DoctorID;
        }

        private void _ResetToDefaultValues()
        {
         
            if (_Mode == enMode.AddMode)
            {
                _NewAccount = new clsDoctorBusinessLayer();
                lblHeader.Text = "Sign Up";
            }
            else
            {
                lblHeader.Text = "Edit Your Info";

                
            }



        }


        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateNewAccount_Click(object sender, EventArgs e)
        {
            _NewAccount.FirstName = txtFirstName.Text;
            _NewAccount.LastName = txtLastName.Text;
            _NewAccount.Username = txtUsername.Text;           
            _NewAccount.Password = clsGlobal.ComputeHash(txtPassword.Text);
            _NewAccount.Address = txtAddress.Text;
            _NewAccount.Specialization = txtSpecialization.Text;
            _NewAccount.PhoneNumber = txtMobileNumber.Text;
            _NewAccount.Email = txtEmail.Text;
            _NewAccount.DateOfBirth = dtpBirthdayDate.Value;
            if (rbMale.Checked)
            {
                _NewAccount.Gender = true;
            }
            else
            {
                _NewAccount.Gender = false;

            }

            if (clsPersonBusinessLayer.IsEmailExist(txtEmail.Text))
            {
                MessageBox.Show("This Email already Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsDoctorBusinessLayer.IsUsernameExist(txtUsername.Text))
            {
                MessageBox.Show("This Username already Used", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        
            if (_NewAccount.Save())
            {
              
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCreateNewAccount.Enabled = false;
                //BackUsername?.Invoke(this, _NewAccount.Username);
                SendUserNameBackToLoginScreen?.Invoke(this, _NewAccount.Username);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }
        private void _LoadUserData()
        {
            _NewAccount = clsDoctorBusinessLayer.GetDoctorInfo(_DoctorID);
            if( _NewAccount != null)
            {

            }


        }
        private void frmSignUp_Load(object sender, EventArgs e)
        {

            _ResetToDefaultValues();
            if (_Mode == enMode.UpdateMode)
            {
                _LoadUserData();
            }

        }
    }
}
