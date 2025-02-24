using ClinicBusinessLayer;
using Simple_Clinic.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Simple_Clinic.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
       private string LoginKeyPath= "HKEY_CURRENT_USER\\SOFTWARE\\Remeber Me Clinic Info";
        private void frmLogin_Load(object sender, EventArgs e)
        {

            string RememberMeValue = "Is Remember Me Checked";
            string RememberMeData = Registry.GetValue(LoginKeyPath, RememberMeValue, null) as string;
            if (RememberMeData == "Yes")
            {
                _LoadLoginInfo();
            }
           

            


        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2PictureBox1_MouseHover(object sender, EventArgs e)
        {
            guna2PictureBox1.BackColor = Color.SeaGreen;
        }

        private void guna2PictureBox1_MouseLeave(object sender, EventArgs e)
        {
         

        }

        private void btnCreateNewAccount_Click(object sender, EventArgs e)
        {
            frmSignUp frm=new frmSignUp();
            frm.SendUserNameBackToLoginScreen += _FillUsernameFromDeleget;
            frm.ShowDialog();

        }
        private void _FillUsernameFromDeleget(object sender,string obj)
        {
            txtUserNameOrEmail.Text = obj;
            txtPassword.Text = null;
        }
        private void _SaveLoginInfo()
        {

            string ValueName = "Last User Name";
            string UserDate;

            string ValuePasword = "Last Password";
            string PasswordData;


            string RememberMeValue = "Is Remember Me Checked";
            string RememberMeData;
            if (cbRememberMe.Checked)
            {
                RememberMeData = "Yes";
                PasswordData = txtPassword.Text.Trim();
                UserDate = txtUserNameOrEmail.Text.Trim();
            }
            else
            {
                RememberMeData = "No";
                PasswordData = "";
                UserDate = "";
            }

            try
            {



                Registry.SetValue(LoginKeyPath, ValueName, UserDate, RegistryValueKind.String);
                Registry.SetValue(LoginKeyPath, ValuePasword,clsGlobal.Encrypt(PasswordData,clsGlobal.Key), RegistryValueKind.String);
                Registry.SetValue(LoginKeyPath, RememberMeValue, RememberMeData, RegistryValueKind.String);

            }
            catch(Exception x)
            {
               // string sourceName = "MyClinic";


                // Create the event source if it does not exist
                if (!EventLog.SourceExists("MyClinic"))
                {
                    EventLog.CreateEventSource("MyClinic", "Application");
                }


                // Log an Error event
                EventLog.WriteEntry("MyClinic", "The Registry Faild.", EventLogEntryType.Error);

            }

        }

        private void _LoadLoginInfo()
        {
            string ValueName = "Last User Name";
            string ValuePasword = "Last Password";

            try
            {
             
                    string UserData = Registry.GetValue(LoginKeyPath, ValueName, null) as string;
                    string PasswordData = Registry.GetValue(LoginKeyPath, ValuePasword, null) as string;

                    txtUserNameOrEmail.Text = UserData;
                    txtPassword.Text = clsGlobal.Decrypt(PasswordData, clsGlobal.Key);
                    cbRememberMe.Checked = true;
    






            }
            catch (Exception x)
            {

            }
        }

     
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPassword.Text)|| string.IsNullOrEmpty(txtUserNameOrEmail.Text))
            {
                return;
            }


            clsDoctorBusinessLayer Doctor = clsDoctorBusinessLayer.GetDoctorInfo(txtUserNameOrEmail.Text.Trim());

            if(Doctor == null)
            {
                MessageBox.Show("Username is not Exist");
                return;
            }
            if(Doctor.Password !=clsGlobal.ComputeHash(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Password is Wrong!");
                return;
            }


//if(Doctor.Username == txtUserNameOrEmail.Text.Trim() && Doctor.Password== clsGlobal.ComputeHash(txtPassword.Text.Trim()))
           // {              
                clsGlobal._CurrentDoctor = Doctor;
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
                _SaveLoginInfo();



           // }



        }

        private void llbForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmForgotPassword frm = new frmForgotPassword();
            frm.ShowDialog();
        }

        private void cbRememberMe_CheckedChanged(object sender, EventArgs e)
        {
          
        }
    }
}
