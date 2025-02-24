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

namespace Simple_Clinic.Login
{
    public partial class frmForgotPassword : Form
    {
        clsDoctorBusinessLayer _Doctor;
        public frmForgotPassword()
        {
            InitializeComponent();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFoundEmail.Text) || string.IsNullOrEmpty(txtUsername.Text))
            {
                return;

            }
           


            _Doctor = clsDoctorBusinessLayer.GetDoctorInfo(txtUsername.Text);

                if (_Doctor != null)
                {

                    if (_Doctor.Email == txtFoundEmail.Text && _Doctor.Username == txtUsername.Text)
                    {
                        gbChangePassword.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("We Can't Found Email or Username", "Didn't Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            

           
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            // Do Error Provider
            if (!string.IsNullOrEmpty(txtChangePassword.Text) || !string.IsNullOrEmpty(txtConfirmPassword.Text))
            {

                if (_Doctor.ChangePassword(clsGlobal.ComputeHash(txtChangePassword.Text), (int)_Doctor.DoctorID))
                {
                    MessageBox.Show("The Password Changed Succesfully", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Error", "SomeThing Wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
            }
            else
            {
                return;
            }
        }
    }
}
