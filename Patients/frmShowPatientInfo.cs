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

namespace Simple_Clinic.Patients
{
    public partial class frmShowPatientInfo : Form
    {
        public frmShowPatientInfo(int patientID)
        {
            InitializeComponent();
            _PatientID = patientID;
            _Patient=clsPatientBusinessLayer.GetPatientInfo(_PatientID);
        }
        clsPatientBusinessLayer _Patient;
        int _PatientID;

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowPatientInfo_Load(object sender, EventArgs e)
        {
            ctrlPatientInfoForForms1.LoadPersonInfo((int)_Patient.PersonID);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();        }
    }
}
