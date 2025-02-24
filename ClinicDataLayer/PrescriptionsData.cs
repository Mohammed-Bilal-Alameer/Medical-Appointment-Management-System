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
    public class PrescriptionsData
    {
        public static int? AddNewPrescription(int MedicalRecordID, string MedicationName, string Dosage, string Frequency, DateTime StartDate, DateTime EndDate, string SpecialInstructions)
        {
            int? PrescriptionID = null;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {



                    using (SqlCommand command = new SqlCommand("SP_AddNewPrescription", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);
                        command.Parameters.AddWithValue("@MedicationName", MedicationName);
                        command.Parameters.AddWithValue("@Dosage", Dosage);
                        command.Parameters.AddWithValue("@Frequency", Frequency);
                        command.Parameters.AddWithValue("@StartDate", StartDate);
                        command.Parameters.AddWithValue("@EndDate", EndDate);
                        command.Parameters.AddWithValue("@SpecialInstructions", SpecialInstructions);
                        SqlParameter OutputParameter = new SqlParameter("PrescriptionID", SqlDbType.Int)
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
                Debug.WriteLine("AddNewPrescription" + ex.Message);
            }


            return PrescriptionID;
        }


        public static bool UpdatePrescriptionInfo(int PrescriptionID, int MedicalRecordID, string MedicationName, string Dosage, string Frequency, DateTime StartDate, DateTime EndDate, string SpecialInstructions)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {



                    using (SqlCommand command = new SqlCommand("SP_UpdatePrescription", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

                        command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);
                        command.Parameters.AddWithValue("@MedicationName", MedicationName);
                        command.Parameters.AddWithValue("@Dosage", Dosage);
                        command.Parameters.AddWithValue("@Frequency", Frequency);
                        command.Parameters.AddWithValue("@StartDate", StartDate);
                        command.Parameters.AddWithValue("@EndDate", EndDate);
                        command.Parameters.AddWithValue("@SpecialInstructions", SpecialInstructions);






                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdatePrescriptionInfo: " + ex.Message);

            }



            return (rowsAffected > 0);




        }



        public static bool FindPrescriptionByMedicalRecordID(ref int PrescriptionID, int MedicalRecordID,ref string MedicationName,ref string Dosage,ref string Frequency,ref DateTime StartDate,ref DateTime EndDate,ref string SpecialInstructions)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {


                    SqlCommand command = new SqlCommand("SP_FindPrescriptionByMedicalRecordID", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);


                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;

                            PrescriptionID = (int)reader["PrescriptionID"];
                            MedicationName = (string)reader["MedicationName"];
                            Dosage = (string)reader["Dosage"];
                            Frequency = (string)reader["Frequency"];
                            StartDate = (DateTime)reader["StartDate"];
                            EndDate = (DateTime)reader["EndDate"];
                            SpecialInstructions = (string)reader["SpecialInstructions"];


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
                Debug.Write("FindPrescriptionByMedicalRecordID" + ex.ToString());
            }




            return isFound;
        }



        public static bool IsPrescriptionExistByID(int MedicalRecordID)
        {

            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                bool IsPrescriptionExsit = false;
                try
                {

                    //string Query = "select found=1 from Doctors where Doctors.Username @Username";
                    using (SqlCommand Command = new SqlCommand("SP_IsPrescriptionExist", Connection))
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
                        IsPrescriptionExsit = (Resulte == 1);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("IsPrescriptionExistByID" + ex.Message);
                }



                return IsPrescriptionExsit;



            }



        }



        public static bool DeleteMedicalRecord(int PrescriptionID)
        {
            int rowsAffected = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {



                    using (SqlCommand command = new SqlCommand("SP_DeletePrescription", connection))
                    {
                        ;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);


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
