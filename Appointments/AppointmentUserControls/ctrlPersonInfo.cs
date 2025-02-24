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

namespace Simple_Clinic.Appointments.AppointmentUserControls
{
    public partial class ctrlPersonInfo : UserControl
    {
        public ctrlPersonInfo()
        {
            InitializeComponent();
        }
        clsPersonBusinessLayer _Person;


        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPersonBusinessLayer.Find(PersonID);
            if (_Person != null)
            {
                lblAdress.Text = _Person.Address;
                lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
                lblName.Text = _Person.FullName;
                lblEmail.Text = _Person.Email;
                lblPhoneNumber.Text = _Person.PhoneNumber;
                if (_Person.Gender)
                {
                    lblGender.Text = "Male";
                }
                else
                {
                    lblGender.Text = "Female";

                }
            }
        }
    }
}
