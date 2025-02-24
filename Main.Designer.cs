namespace Simple_Clinic
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.gbPatients = new Guna.UI2.WinForms.Guna2GroupBox();
            this.AllPatientsButton = new System.Windows.Forms.Label();
            this.pbAddButton = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblAddNewPatient = new System.Windows.Forms.Label();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            this.gbPatients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddButton)).BeginInit();
            this.guna2GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(227)))), ((int)(((byte)(169)))));
            this.guna2Panel3.Controls.Add(this.label1);
            this.guna2Panel3.Controls.Add(this.guna2PictureBox1);
            this.guna2Panel3.Controls.Add(this.guna2PictureBox2);
            this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel3.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(800, 70);
            this.guna2Panel3.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 33);
            this.label1.TabIndex = 7;
            this.label1.Text = "My Clinic";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(227)))), ((int)(((byte)(169)))));
            this.guna2PictureBox1.Image = global::Simple_Clinic.Properties.Resources.Exit;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(761, 6);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(27, 24);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 5;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.Image = global::Simple_Clinic.Properties.Resources.settings_cog;
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(12, 12);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(34, 29);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox2.TabIndex = 6;
            this.guna2PictureBox2.TabStop = false;
            // 
            // gbPatients
            // 
            this.gbPatients.Controls.Add(this.AllPatientsButton);
            this.gbPatients.Controls.Add(this.pbAddButton);
            this.gbPatients.Controls.Add(this.lblAddNewPatient);
            this.gbPatients.Font = new System.Drawing.Font("PT Bold Arch", 13F, System.Drawing.FontStyle.Bold);
            this.gbPatients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbPatients.Location = new System.Drawing.Point(24, 99);
            this.gbPatients.Name = "gbPatients";
            this.gbPatients.Size = new System.Drawing.Size(300, 200);
            this.gbPatients.TabIndex = 7;
            this.gbPatients.Text = "Patients";
            // 
            // AllPatientsButton
            // 
            this.AllPatientsButton.AutoSize = true;
            this.AllPatientsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AllPatientsButton.Location = new System.Drawing.Point(17, 126);
            this.AllPatientsButton.Name = "AllPatientsButton";
            this.AllPatientsButton.Size = new System.Drawing.Size(173, 32);
            this.AllPatientsButton.TabIndex = 11;
            this.AllPatientsButton.Text = "Total Patients";
            this.AllPatientsButton.Click += new System.EventHandler(this.AllPatientsButton_Click);
            // 
            // pbAddButton
            // 
            this.pbAddButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbAddButton.FillColor = System.Drawing.Color.Transparent;
            this.pbAddButton.Image = global::Simple_Clinic.Properties.Resources.plus;
            this.pbAddButton.ImageRotate = 0F;
            this.pbAddButton.Location = new System.Drawing.Point(250, 62);
            this.pbAddButton.Name = "pbAddButton";
            this.pbAddButton.Size = new System.Drawing.Size(38, 32);
            this.pbAddButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAddButton.TabIndex = 9;
            this.pbAddButton.TabStop = false;
            this.pbAddButton.Click += new System.EventHandler(this.pbAddButton_Click);
            // 
            // lblAddNewPatient
            // 
            this.lblAddNewPatient.AutoSize = true;
            this.lblAddNewPatient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAddNewPatient.Location = new System.Drawing.Point(17, 62);
            this.lblAddNewPatient.Name = "lblAddNewPatient";
            this.lblAddNewPatient.Size = new System.Drawing.Size(204, 32);
            this.lblAddNewPatient.TabIndex = 10;
            this.lblAddNewPatient.Text = "Add New Patient";
            this.lblAddNewPatient.Click += new System.EventHandler(this.lblAddNewPatient_Click);
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.Controls.Add(this.guna2PictureBox3);
            this.guna2GroupBox1.Controls.Add(this.label5);
            this.guna2GroupBox1.Controls.Add(this.label4);
            this.guna2GroupBox1.Font = new System.Drawing.Font("PT Bold Arch", 13F, System.Drawing.FontStyle.Bold);
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2GroupBox1.Location = new System.Drawing.Point(474, 99);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(300, 200);
            this.guna2GroupBox1.TabIndex = 8;
            this.guna2GroupBox1.Text = "Appointments";
            // 
            // guna2PictureBox3
            // 
            this.guna2PictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2PictureBox3.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox3.Image = global::Simple_Clinic.Properties.Resources.plus;
            this.guna2PictureBox3.ImageRotate = 0F;
            this.guna2PictureBox3.Location = new System.Drawing.Point(250, 5);
            this.guna2PictureBox3.Name = "guna2PictureBox3";
            this.guna2PictureBox3.Size = new System.Drawing.Size(38, 32);
            this.guna2PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox3.TabIndex = 13;
            this.guna2PictureBox3.TabStop = false;
            this.guna2PictureBox3.Click += new System.EventHandler(this.guna2PictureBox3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Location = new System.Drawing.Point(29, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 32);
            this.label5.TabIndex = 12;
            this.label5.Text = "Today";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Location = new System.Drawing.Point(29, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "All Appointment";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.guna2GroupBox1);
            this.Controls.Add(this.gbPatients);
            this.Controls.Add(this.guna2Panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            this.gbPatients.ResumeLayout(false);
            this.gbPatients.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddButton)).EndInit();
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2GroupBox gbPatients;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private Guna.UI2.WinForms.Guna2PictureBox pbAddButton;
        private System.Windows.Forms.Label AllPatientsButton;
        private System.Windows.Forms.Label lblAddNewPatient;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
    }
}

