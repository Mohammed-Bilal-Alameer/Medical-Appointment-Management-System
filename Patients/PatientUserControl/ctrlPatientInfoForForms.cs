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

namespace Simple_Clinic.Patients.PatientUserControl
{
    public partial class ctrlPatientInfoForForms : UserControl
    {
        clsPersonBusinessLayer _Person;
        public ctrlPatientInfoForForms()
        {
            InitializeComponent();
        }


        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPersonBusinessLayer.Find(PersonID);
            if (_Person != null)
            {
                lblAddress.Text = _Person.Address;
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
