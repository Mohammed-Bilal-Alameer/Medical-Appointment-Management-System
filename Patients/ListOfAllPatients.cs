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
    public partial class frmListOfAllPatients : Form
    {
      static  DataTable _dtPatientsList=clsPatientBusinessLayer.GetAllPatients();
        DataView _dataViewPatients = _dtPatientsList.DefaultView;

        bool _IsFromAppointment;
        int _PatientID;
  
        public frmListOfAllPatients(bool IsFromAppointment)
        {
            _IsFromAppointment=IsFromAppointment;
            InitializeComponent();
            _dtPatientsList = clsPatientBusinessLayer.GetAllPatients();
            _dataViewPatients = _dtPatientsList.DefaultView;
        }
        public event Action<object, int, string> SendPatienNameAndIDBack;

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilter.SelectedIndex == 0)
            {
                txtFilter.PlaceholderText = "Search by first name";
            }
            if (cbFilter.SelectedIndex == 1)
            {
                txtFilter.PlaceholderText = "Search by last name";
            }
            if (cbFilter.SelectedIndex == 2)
            {
                txtFilter.PlaceholderText = "Search by number";
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            switch (cbFilter.SelectedItem)
            {
                case "Contact Number":
                    {
                        if (int.TryParse(txtFilter.Text, out int ID))
                        {
                            _dataViewPatients.RowFilter = $"[PhoneNumber]  = '{ID}'";


                        }
                        break;
                    }
                case "Patient Name":
                    {
                        _dataViewPatients = _dtPatientsList.DefaultView;
                        _dataViewPatients.RowFilter = $"PatientName Like '{txtFilter.Text}%'";
                        break;
                    }
          


                default:
                    {
                        dgvPatientsList.DataSource = _dataViewPatients;
                        break;

                    }


            }
        }

        private void frmListOfAllPatients_Load(object sender, EventArgs e)
        {
            dgvPatientsList.DataSource=clsPatientBusinessLayer.GetAllPatients();
            lblPatientrecords.Text= dgvPatientsList.RowCount.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   

        private void dgvPatientsList_DoubleClick(object sender, EventArgs e)
        {
            if (_IsFromAppointment)
            {
                //clsPatientBusinessLayer _Patient=clsPatientBusinessLayer

                SendPatienNameAndIDBack.Invoke(this, (int)dgvPatientsList.CurrentRow.Cells[0].Value, dgvPatientsList.CurrentRow.Cells[1].Value.ToString());

                this.Close();
            }
            
        
        }

        private void updatePatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewPatient frm = new frmAddNewPatient((int)dgvPatientsList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListOfAllPatients_Load(null, null);
        }

        private void deletePatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if( MessageBox.Show("Are You Sure You Want To Delete This Patient?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK){

                if (clsAppointmentsBusinessLayer.IsThePatientHasAnAppointment((int)dgvPatientsList.CurrentRow.Cells[0].Value)) 
                {
                    MessageBox.Show("You Can't Delete This Patient Becuse he has Unfinished Appointment","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }


                if (clsPatientBusinessLayer.DeletePatientAndPerson((int)dgvPatientsList.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Patient Deleted Successfully.");
                    frmListOfAllPatients_Load(null, null);

                }

            }
            else
            {
                MessageBox.Show("Patient is Not Deleted");


            }

        }

        private void showPatientDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPatientInfo frm=new frmShowPatientInfo((int)dgvPatientsList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
