using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ClinicDataLayer
{
    public class clsPatientsData
    {

        public static int? AddNewPatient(int? PersonID, string Allergies, string Notes)
        {
            int? PatientID = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {




                    using (SqlCommand command = new SqlCommand("SP_AddNewPatient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@Allergies", (object)Allergies ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);



                        SqlParameter OutputParameter = new SqlParameter("PatientID", SqlDbType.Int)
                        {

                            Direction = ParameterDirection.Output

                        };
                        command.Parameters.Add(OutputParameter);

                        connection.Open();
                        command.ExecuteNonQuery();
                        PatientID = (int)OutputParameter.Value;


                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddNewPatient" + ex.Message);
            }


            return PatientID;
        }

        public static bool FindPatientByID(int PatientID, ref int PersonID, ref string Allergies, ref string Notes)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {


                    SqlCommand command = new SqlCommand("SP_FindPatientByID", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PatientID", PatientID);


                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;

                            PersonID = (int)reader["PersonID"];
                            if (reader["Allergies"] == DBNull.Value)
                            {
                                Allergies = null;
                            }
                            else
                                Allergies = (string)reader["Allergies"];

                            if (reader["Notes"] == DBNull.Value)
                            {
                                Notes = null;
                            }
                            else
                                Notes = (string)reader["Notes"];




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
                Debug.Write("FindPatientByID" + ex.ToString());
            }




            return isFound;
        }

        public static bool UpdatePatient(int PatientID,int? PersonID, string Allergies, string Notes)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

     

                    using (SqlCommand command = new SqlCommand("SP_UpdatePatientInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PatientID", PatientID);

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@Allergies", (object)Allergies ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);


                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdatePerson: " + ex.Message);

            }



            return (rowsAffected > 0);




        }

        //delete patient with after delete and delete person
        public static bool DeletePatient(int PatientID)
        {
            int rowsAffected = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    ;

                    //string query = @"Delete People 
                    //                    where PersonID = @PersonID";

                    using (SqlCommand command = new SqlCommand("SP_DeletePatientWithPerson", connection))
                    {
                        ;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PatientID", PatientID);


                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeletePatient" + ex.Message.ToString());
            }
        

            return (rowsAffected > 0);


        }

        public static DataTable GetAllPatients()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

                { 
                using (SqlCommand command = new SqlCommand("SP_PatientsList", connection))
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
                Debug.Write("GetAllPatients" + ex.ToString());
            }

            return dt;

        }

    }
}
