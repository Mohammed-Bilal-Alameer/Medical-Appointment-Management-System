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
    public class clsMedicalRecordData
    {
        public static int? AddNewMedicalRecord( string VisitDescription, string Diagnosis, string AdditionalNotes)
        {
            int?  MedicalRecordID = null;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {



                    using (SqlCommand command = new SqlCommand("SP_AddMedicalRecord", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@VisitDescription", VisitDescription);
                        command.Parameters.AddWithValue("@Diagnosis", Diagnosis);
                        command.Parameters.AddWithValue("@AdditionalNotes", AdditionalNotes);

                        SqlParameter OutputParameter = new SqlParameter("MedicalRecordID", SqlDbType.Int)
                        {

                            Direction = ParameterDirection.Output

                        };
                        command.Parameters.Add(OutputParameter);

                        connection.Open();
                        command.ExecuteNonQuery();
                        MedicalRecordID = (int)OutputParameter.Value;


                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddNewMedicalRecord" + ex.Message);
            }


            return MedicalRecordID;
        }


        public static bool UpdateMedicalRecordInfo(int MedicalRecordID, string VisitDescription, string Diagnosis, string AdditionalNotes)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

      

                    using (SqlCommand command = new SqlCommand("SP_UpdateMedicalRecord", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);
                        command.Parameters.AddWithValue("@VisitDescription", VisitDescription);
                        command.Parameters.AddWithValue("@Diagnosis", Diagnosis);
                        command.Parameters.AddWithValue("@AdditionalNotes", AdditionalNotes);






                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateMedicalRecordInfo: " + ex.Message);

            }



            return (rowsAffected > 0);




        }

        public static bool FindMedicalRecordByID(int MedicalRecordID, ref string VisitDescription, ref string Diagnosis, ref string AdditionalNotes)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {


                    SqlCommand command = new SqlCommand("SP_FindMedicalRecordByID", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);


                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;

                            VisitDescription = (string)reader["VisitDescription"];
                            Diagnosis = (string)reader["Diagnosis"];                         
                            AdditionalNotes = (string)reader["AdditionalNotes"];



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
                Debug.Write("FindMedicalRecordByID" + ex.ToString());
            }




            return isFound;
        }
       

        public static bool IsMedicalRecordExistByID(int MedicalRecordID)
        {

            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                bool IsMedicalRecord = false;
                try
                {

                    //string Query = "select found=1 from Doctors where Doctors.Username @Username";
                    using (SqlCommand Command = new SqlCommand("SP_IsMedicalRecordExistsByMedicalRecordID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);

                        SqlParameter returnParameter = new SqlParameter
                        {
                            Direction = ParameterDirection.ReturnValue
                        };

                        Command.Parameters.Add(returnParameter);
                        Connection.Open();

                        Command.ExecuteScalar();
                        int Resulte = (int)returnParameter.Value;
                        IsMedicalRecord = (Resulte == 1);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("IsMedicalRecordExistByID" + ex.Message);
                }



                return IsMedicalRecord;



            }



        }


        public static bool DeleteMedicalRecord(int MedicalRecordID)
        {
            int rowsAffected = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    

               
                    using (SqlCommand command = new SqlCommand("SP_DeleteMedicalRecord", connection))
                    {
                        ;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);


                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteMedicalRecord" + ex.Message.ToString());
            }


            return (rowsAffected > 0);


        }


    }
}
