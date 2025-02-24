using Simple_Clinic.Appointments;
using Simple_Clinic.Login;
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

namespace Simple_Clinic
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;

        public frmMain(frmLogin frmLogin)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
        }

        private void pbAddButton_Click(object sender, EventArgs e)
        {
            frmAddNewPatient NewFrm=new frmAddNewPatient(false);
            NewFrm.ShowDialog();
        }

        private void AllPatientsButton_Click(object sender, EventArgs e)
        {
            frmListOfAllPatients frmListOfAllPatients = new frmListOfAllPatients(false);
            frmListOfAllPatients.ShowDialog();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            frmAddAppointment frmAddAppointment = new frmAddAppointment();
            frmAddAppointment.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            frmAppointmentsList frm=new frmAppointmentsList(true);
            frm.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            frmAppointmentsList frm = new frmAppointmentsList();
            frm.ShowDialog();
        }

        private void lblAddNewPatient_Click(object sender, EventArgs e)
        {
            frmAddAppointment frmAddAppointment = new frmAddAppointment();
            frmAddAppointment.ShowDialog();
        }
    }
}
