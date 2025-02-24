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

namespace Simple_Clinic.Appointments
{
    public partial class frmAppointmentsList : Form
    {
        static DataTable _dtAppointmentsList ;
        DataView _dataViewAppointmentsList;
        bool _IsAppointmentsForToday;
        public frmAppointmentsList()
        {
            InitializeComponent();
            _dtAppointmentsList = clsAppointmentsBusinessLayer.GetAllAppointments();
            lblHeader.Text = "All The Appointments";
             _dataViewAppointmentsList = _dtAppointmentsList.DefaultView;

        }

        public frmAppointmentsList(bool IsAppointmentsForToday)
        {
            InitializeComponent();
            _IsAppointmentsForToday=IsAppointmentsForToday;
            _dtAppointmentsList=clsAppointmentsBusinessLayer.GetAllAppointmentsForToday();
            lblHeader.Text = "Appointments For Today";
            _dataViewAppointmentsList = _dtAppointmentsList.DefaultView;

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void _RefreshTheList()
        {
            if (_IsAppointmentsForToday)
            {
                dgvAppointmentsList.DataSource = clsAppointmentsBusinessLayer.GetAllAppointmentsForToday();

            }
            else
            {
                dgvAppointmentsList.DataSource = clsAppointmentsBusinessLayer.GetAllAppointments();

            }
        }
        private void frmAppointmentsList_Load(object sender, EventArgs e)
        {
            _RefreshTheList();
            lblAppointmentRecored.Text = dgvAppointmentsList.RowCount.ToString();


        }

        private void pbAddButton_Click(object sender, EventArgs e)
        {
            frmAddAppointment frm=new frmAddAppointment();
            frm.ShowDialog();
            frmAppointmentsList_Load(null, null);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                txtFilter.PlaceholderText = "Search patient name";
            }
            if (cbFilter.SelectedIndex == 1)
            {
                txtFilter.PlaceholderText = "Search doctor name";
            }
            if (cbFilter.SelectedIndex == 2)
            {
                txtFilter.PlaceholderText = "Search by Appointment Status";
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            switch (cbFilter.SelectedItem)
            {
               
                case "Doctor Name":
                    {
                        _dataViewAppointmentsList.RowFilter = $"DoctorName Like '{txtFilter.Text}%'";
                        break;
                    }
                case "Patient Name":
                    {
                        _dataViewAppointmentsList.RowFilter = $"PatientName Like '{txtFilter.Text}%'";
                        break;
                    }
                case "Appointment Status":
                    {
                        _dataViewAppointmentsList.RowFilter = $"AppointmentStatus Like '{txtFilter.Text}%'";
                        break;
                    }

                default:
                    {
                        dgvAppointmentsList.DataSource = _dtAppointmentsList;
                        break;

                    }

            }
        }

        private void updateAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddAppointment frm=new frmAddAppointment((int)dgvAppointmentsList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmAppointmentsList_Load(null, null);

        }

        private void pendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Change The Statu?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsAppointmentsBusinessLayer.UpdateAppointmentStatus((int)dgvAppointmentsList.CurrentRow.Cells[0].Value, clsAppointmentsBusinessLayer.enAppointmenStatus.Pending)
                )
                {

                    frmAppointmentsList_Load(null, null);

                }
            }
            else
            {
                return;

            }


          

        }

        private void confirmedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Change The Statu?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsAppointmentsBusinessLayer.UpdateAppointmentStatus((int)dgvAppointmentsList.CurrentRow.Cells[0].Value, clsAppointmentsBusinessLayer.enAppointmenStatus.Confirmed))
                {

                    frmAppointmentsList_Load(null, null);
                }
            }
            else
            {
                return;

            }

        }

        private void completedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Change The Statu?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsAppointmentsBusinessLayer.UpdateAppointmentStatus((int)dgvAppointmentsList.CurrentRow.Cells[0].Value, clsAppointmentsBusinessLayer.enAppointmenStatus.Completed))
                {

                    frmAppointmentsList_Load(null, null);
                }
            }
            else
            {
                return;

            }
        }

        private void canceledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Change The Statu?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsAppointmentsBusinessLayer.UpdateAppointmentStatus((int)dgvAppointmentsList.CurrentRow.Cells[0].Value, clsAppointmentsBusinessLayer.enAppointmenStatus.Canceled))
                {

                    frmAppointmentsList_Load(null, null);
                }
            }
            else
            {
                return;

            }
        }

        private void rescheduledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Change The Statu?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsAppointmentsBusinessLayer.UpdateAppointmentStatus((int)dgvAppointmentsList.CurrentRow.Cells[0].Value, clsAppointmentsBusinessLayer.enAppointmenStatus.Rescheduled))
                {

                    frmAppointmentsList_Load(null, null);
                }
            }
            else
            {
                return;

            }
        }

        private void noShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Change The Statu?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsAppointmentsBusinessLayer.UpdateAppointmentStatus((int)dgvAppointmentsList.CurrentRow.Cells[0].Value, clsAppointmentsBusinessLayer.enAppointmenStatus.NoShow))
                {

                    frmAppointmentsList_Load(null, null);
                }
            }
            else
            {
                return;

            }
        }

        private void deleteAppointmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Delete the Appointment?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsAppointmentsBusinessLayer.DeleteAppointment((int)dgvAppointmentsList.CurrentRow.Cells[0].Value))
                {

                    frmAppointmentsList_Load(null, null);
                }
            }
            else
            {
                return;

            }
        }
    }
}
