using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicDataLayer
{
    public  class clsAppointmentsData
    {
        public static int? AddNewAppointment(int? PatientID, int? DoctorID, Nullable<DateTime> AppointmentDateTime,DateTime? StartTime,DateTime? EndTime, decimal AppointmentStatus, int? MedicalRecordID, int? PaymentID)
        {
            int? AppointmentID = null;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {



                    using (SqlCommand command = new SqlCommand("SP_AddNewAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PatientID", PatientID);
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);
                        command.Parameters.AddWithValue("@AppointmentDateTime", AppointmentDateTime);
                        command.Parameters.AddWithValue("@StartTime", StartTime);
                        command.Parameters.AddWithValue("@EndTime", EndTime);

                        command.Parameters.AddWithValue("@AppointmentStatus", AppointmentStatus);
                        command.Parameters.AddWithValue("@MedicalRecordID", (object)MedicalRecordID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentID", (object)PaymentID ?? DBNull.Value);



                        SqlParameter outputIdParam = new SqlParameter("@AppointmentID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);


                        connection.Open();

                        command.ExecuteNonQuery();

                        AppointmentID = (int)outputIdParam.Value;

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddNewAppointment: " + ex.Message);
            }


            return AppointmentID;
        }

        public static bool UpdateAppointment(int AppointmentID, int? PatientID, int? DoctorID, Nullable<DateTime> AppointmentDateTime, DateTime? StartTime, DateTime? EndTime, decimal AppointmentStatus, int? MedicalRecordID, int? PaymentID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                   

                    using (SqlCommand command = new SqlCommand("SP_UpdateAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                        command.Parameters.AddWithValue("@PatientID", PatientID);
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);
                        command.Parameters.AddWithValue("@AppointmentDateTime", AppointmentDateTime);
                        command.Parameters.AddWithValue("@StartTime", StartTime);
                        command.Parameters.AddWithValue("@EndTime", EndTime);
                        command.Parameters.AddWithValue("@AppointmentStatus", AppointmentStatus );
                        command.Parameters.AddWithValue("@MedicalRecordID", (object)MedicalRecordID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentID", (object)PaymentID ?? DBNull.Value);

                        //if (DateOfBirth == null)
                        //    command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value);
                        //else
                        //    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);



                        //if (string.IsNullOrEmpty(Email))
                        //    command.Parameters.AddWithValue("@Email", DBNull.Value);
                        //else
                        //    command.Parameters.AddWithValue("@Email", Email);

                        //if (string.IsNullOrEmpty(Address))
                        //    command.Parameters.AddWithValue("@Address", DBNull.Value);
                        //else
                        //    command.Parameters.AddWithValue("@Address", Address);



                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateAppointment: " + ex.Message);

            }



            return (rowsAffected > 0);




        }

        public static bool ChangeAppointmentStatus(int AppointmentID, decimal AppointmentStatus)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                
                    using (SqlCommand command = new SqlCommand("SP_UpdateAppointmentStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                     
                        command.Parameters.AddWithValue("@AppointmentStatus", AppointmentStatus);
                




                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ChangeAppointmentStatus: " + ex.Message);

            }



            return (rowsAffected > 0);




        }

        public static bool DeleteAppointment(int AppointmentID)
        {
            int rowsAffected = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    ;

                    //string query = @"Delete People 
                    //                    where PersonID = @PersonID";

                    using (SqlCommand command = new SqlCommand("SP_DeleteAppointment", connection))
                    {
                        ;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);


                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteAppointment" + ex.Message.ToString());
            }


            return (rowsAffected > 0);


        }

        public static DataTable GetAllAppointmentsForToday()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllAppointmentsForToday", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;



                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)

                            {
                                dt.Load(reader);
                            }

                            reader.Close();
                        }
                    }
                }
            }






            catch (Exception ex)
            {
                Debug.Write("GetAllAppointmentsForToday" + ex.ToString());
            }

            return dt;

        }

        public static DataTable GetAllAppointments()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllAppointments", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;



                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)

                            {
                                dt.Load(reader);
                            }

                            reader.Close();
                        }
                    }
                }
            }






            catch (Exception ex)
            {
                Debug.Write("GetAllAppointments" + ex.ToString());
            }

            return dt;

        }


        public static bool IsThePatientHasAnAppointment(int PatientID)
        {

            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                bool IsThePatientHasAnAppointment = false;
                try
                {

                    //string Query = "select found=1 from Doctors where Doctors.Username @Username";
                    using (SqlCommand Command = new SqlCommand("SP_IsThePatientHasAnAppointment", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@PatientID", PatientID);

                        SqlParameter returnParameter = new SqlParameter
                        {
                            Direction = ParameterDirection.ReturnValue
                        };

                        Command.Parameters.Add(returnParameter);
                        Connection.Open();

                        Command.ExecuteScalar();
                        int Resulte = (int)returnParameter.Value;
                        IsThePatientHasAnAppointment = (Resulte == 1);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("IsThePatientHasAnAppointment" + ex.Message);
                }



                return IsThePatientHasAnAppointment;



            }



        }



        public static bool FindAppointmentByID(int AppointmentID, ref int? PatientID, ref int? DoctorID,ref Nullable<DateTime> AppointmentDateTime,ref DateTime? StartTime,ref DateTime? EndTime,ref int AppointmentStatus,ref int? MedicalRecordID,ref int? PaymentID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {


                    SqlCommand command = new SqlCommand("SP_FindAppointmentByID", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);


                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;

                            PatientID = (int)reader["PatientID"];
                            DoctorID = (int)reader["DoctorID"];
                            AppointmentDateTime = (DateTime)reader["AppointmentDateTime"];

                            if (reader["StartTime"] == DBNull.Value)
                            {
                                StartTime = null;
                            }
                            else
                                StartTime = (DateTime)reader["StartTime"];

                            if (reader["EndTime"] == DBNull.Value)
                            {
                                EndTime = null;
                            }
                            else
                                EndTime = (DateTime)reader["EndTime"];


                            AppointmentStatus =  (int)reader["AppointmentStatus"];

                    




                            if (reader["MedicalRecordID"] == DBNull.Value)
                            {
                                MedicalRecordID = null;
                            }
                            else
                                MedicalRecordID = (int)reader["MedicalRecordID"];

                            if (reader["PaymentID"] == DBNull.Value)
                            {
                                PaymentID = null;
                            }
                            else
                                PaymentID = (int)reader["PaymentID"];




                        }
                        else
                        {
                            // The record was not found
                            isFound = false;
                        }

                        reader.Close();


                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("FindAppointmentByID" + ex.ToString());
            }




            return isFound;
        }


    }
}
